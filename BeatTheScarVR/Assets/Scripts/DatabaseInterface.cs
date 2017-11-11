
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using UnityEngine;
using System.Threading;

    /**
    * @class DatabaseInterface
    * @brief Contains all the functionality between the database and the game data.
    *
    * @author Geoff Hanson / MGU
    * @version 1
    * @date 10/11/17
    *
    */
public class DatabaseInterface
{
    private List<GameDataHelper.Game> m_gameList;
    private List<GameDataHelper.GameInstance> m_programGames;
    private List<int> m_restrictions;
    private string m_userID;
    private int m_currentProgram;
    private GameDataHelper.GameInstance m_currentGame;

    private OdbcConnection con;

    /**
    * @brief Default Constructor
     * Constructor that initialises the connection to the database and initialises the m_gameList, m_programGames, m_restrictions.
     * Sets up the Database Interface by calling required functions.
    *
    * @param
    * @return
    * @pre
    * @post
    */
    public DatabaseInterface()
    {
        con = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=..\\KinesisArcade.mdb");
        con.Open();
        m_gameList = new List<GameDataHelper.Game>();
        m_programGames = new List<GameDataHelper.GameInstance>();
        m_restrictions = new List<int>();
        setUser();
        setProgramID();
        buildGameList();
        setProgramList();
        setRestrictions();
    }

    /**
    * @brief Parameter Constructor
     * Constructor that initialises the connection to the database and calls SetUser() function.
    *
    * @param int x
    * @return
    * @pre
    * @post
    */
    public DatabaseInterface(int x)
    {
        con = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=..\\KinesisArcade.mdb");
        con.Open();
        setUser();
    }

    /**
    * @brief Sets the User ID
     * The Login Server stores the userID information in a text file 'currentuser.txt' so the Database Interface
     * reads the text file and stores it in the m_userID then sets the patient to m_userID.
    *
    * @param
    * @return void
    * @pre
    * @post
    */
    private void setUser()
    { 
        StreamReader sr = File.OpenText("..\\currentuser.txt");
        m_userID = sr.ReadLine();
        GameDataHelper.setPatient(m_userID);      
    }

    /**
    * @brief Sets the Program ID
     * Opens a connection to the database and queries "SELECT ProgramID FROM PATIENT WHERE UserID = '" + m_userID + "'".
     * The reader will then read the results from the query and set the current program to program 1.
     * @todo meant to read program list of patient and assign m_currentProgram to the id of the program selected.
    *
    * @param
    * @return void
    * @pre
    * @post
    */
    private void setProgramID()
    {
        OdbcCommand cmd = new OdbcCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT ProgramID FROM PATIENT WHERE UserID = '" + m_userID + "'";
        OdbcDataReader read = cmd.ExecuteReader();
        read.Read();
        //m_currentProgram = read.GetInt32(0);
        m_currentProgram = 1;
        read.Close();
    }

    /**
    * @brief Adds Patient Data to Database
     * Opens a connection to the database adds the parameters given into the patient data then updates the
     * game instance of the patient setting the completed value to 1 (true).
    *
    * @param GameDataHelper.GameInstance game
     * @param int score
     * @param int timePlayed
     * @string videourl
    * @return void
    * @pre
    * @post
    */
    public void addPatientData(GameDataHelper.GameInstance game, int score, int timePlayed, string videourl)
    {
        OdbcCommand cmd = new OdbcCommand();
        cmd.Connection = con;
        cmd.CommandText = "INSERT INTO PATIENTDATA(GameID, UserID, Score, TimePlayed, VideoLocation) VALUES(" + game.m_gameID + ", '" + m_userID + "', " + score + ", " + timePlayed + ", '" + videourl + "')";
        cmd.ExecuteNonQuery();

        OdbcCommand cmd2 = new OdbcCommand();
        cmd2.Connection = con;
        cmd2.CommandText = "UPDATE GAMEINSTANCE SET Completed = 1 WHERE GameInstanceID = " + game.m_gameInstanceID;
        cmd2.ExecuteNonQuery();
    }

    /**
    * @brief Builds the Game List
     * Opens a connection to the database and queries "SELECT * FROM GAME". Creates a GameDataHelper.Game object called game 
     * and whilst the query results contains rows, assigns the corresponding column values to that of the game object then
     * adds it to the game list.
    *
    * @param
    * @return void
    * @pre
    * @post
    */
    private void buildGameList()
    {
        GameDataHelper.Game game;

        OdbcCommand cmd = new OdbcCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM GAME";
        OdbcDataReader read = cmd.ExecuteReader();

        while(read.Read())
        {
            game = new GameDataHelper.Game();
            game.m_id = read.GetInt32(0);
            game.m_coordx = read.GetInt32(1);
            game.m_coordy = read.GetInt32(2);
            game.m_coordz = read.GetInt32(3);
            game.m_title = read.GetString(4);
            game.m_description = read.GetString(5);
            m_gameList.Add(game);          
        }
    }

    /**
    * @brief Returns Game List
    *
    * @param
    * @return List<GameDataHelper.Game> m_gameList
    * @pre
    * @post
    */
    public List<GameDataHelper.Game> getGameList()
    {
        return m_gameList;
    }

    /**
    * @brief Check if Program is Complete
     * Opens a connection to the database and queries "SELECT Completed FROM GAMEINSTANCE WHERE ProgramID = " + m_currentProgram.
     * Whilst the query result has rows, it checks if the current rows string value == "False". If true, the program has been
     * completed else return false.
    *
    * @param
    * @return bool
    * @pre
    * @post
    */
    public bool programComplete()
    {
        bool check = true;

        OdbcCommand cmd = new OdbcCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT Completed FROM GAMEINSTANCE WHERE ProgramID = " + m_currentProgram;
        OdbcDataReader read = cmd.ExecuteReader();

        while(read.Read())
        {
            if (read.GetString(0) == "False")
            {
                check = false;
            }
        };

        return check;
    }

    /**
    * @brief Sets Current Game
     * Loops through the program games and checks if the program game has been completed. If true,
     * continues to check the rest of the games in the program games else sets the current game to
     * that of the game that has not been completed and breaks to keep game set at that incomplete
     * game.
    *
    * @param
    * @return void
    * @pre
    * @post
    */
    public void setCurrentGame()
    {
        for (int i = 0; i < m_programGames.Count; i++)
        {
            if (!m_programGames.ElementAt(i).m_completed)
            {
                m_currentGame = m_programGames.ElementAt(i);
                GameDataHelper.setCurrentGame(m_programGames.ElementAt(i));
                break;
            }
        }
    }

    /**
    * @brief Returns Current Game Instance
    *
    * @param
    * @return GameDataHelper.GameInstance m_currentGame
    * @pre
    * @post
    */
    public GameDataHelper.GameInstance getCurrentGame()
    {
        return m_currentGame;
    }

    /**
    * @brief Sets Program List
     * Opens up a connection to the database and queries "SELECT * FROM GAMEINSTANCE WHERE ProgramID = " + m_currentProgram.
     * Reads through each of the database columns and assigns the values to the corresponding current instance of the program
     * then sets it to the current game.
    *
    * @param
    * @return void
    * @pre
    * @post
    */
    private void setProgramList()
    {
        GameDataHelper.GameInstance game;

        OdbcCommand cmd = new OdbcCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM GAMEINSTANCE WHERE ProgramID = " + m_currentProgram;
        OdbcDataReader read = cmd.ExecuteReader();

        while(read.Read())
        {
            game = new GameDataHelper.GameInstance();
            game.m_gameInstanceID = read.GetInt32(0);
            game.m_gameID = read.GetInt32(2);
            game.m_difficulty = read.GetInt32(3);
            game.m_duration = read.GetInt32(4);

            if(read.GetString(5) == "True")
            {
                game.m_completed = true;
            }
            else
            {
                game.m_completed = false;
            }
            m_programGames.Add(game);
        }

        setCurrentGame();
    }

    /**
    * @brief Returns Program Game List
    *
    * @param
    * @return List<GameDataHelper.GameInstance> m_programGames
    * @pre
    * @post
    */
    public List<GameDataHelper.GameInstance> getProgramGameList()
    {
        return m_programGames;
    }

    /**
    * @brief Sets Restrictions
     * Opens a connection to the database and queries to select all the values from
     * RESTRICTION column. Whilst the column has rows, the value of each string is
     * checked. If the string == "True", the game id is added to the restrictions 
     * list.
    *
    * @param
    * @return void
    * @pre
    * @post
    */
    private void setRestrictions()
    {
        int game;

        OdbcCommand cmd = new OdbcCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM RESTRICTION";
        OdbcDataReader read = cmd.ExecuteReader();

        while(read.Read())
        {
            if(read.GetString(3) == "True")
            {
                for(int j = 0; j < m_gameList.Count; j++)
                {
                    if(read.GetInt32(1) == m_gameList.ElementAt(j).m_id)
                    {
                        game = m_gameList.ElementAt(j).m_id;
                        m_restrictions.Add(game);
                    }
                }
                
            }
            
        }
    }

    /**
    * @brief Returns Restrictions List
    
    * @param
    * @return List<int> m_restrictions
    * @pre
    * @post
    */
    public List<int> getRestrictions()
    {
        return m_restrictions;
    }

    /**
    * @brief Checks if can Free Play
     * Opens up a connection to the database and queries it using the "SELECT CanFreePlay FROM PATIENT WHERE UserID = m_userID. 
     * If the query returns a string == "true" then free play is enabled, else false.
    *
    * @param
    * @return bool
    * @pre
    * @post
    */
    public bool canFreePlay()
    {
        bool check = false;

        OdbcCommand cmd = new OdbcCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT CanFreePlay FROM PATIENT WHERE UserID = '" + m_userID + "'";
        OdbcDataReader read = cmd.ExecuteReader();
        read.Read();

        if(read.GetString(0) == "True")
        {
            check = true;
        }

        return check;
    }

    /**
    * @brief Sets Current Game Instance
    *
    * @param int gid
     * @param int difficulty
     * @param int time
    * @return GameDataHelper.GameInstance gi
    * @pre
    * @post
    */
	public GameDataHelper.GameInstance addGameInstance(int gid, int difficulty, int time)
    {
        int ginstid;
        int count = 0;
        OdbcCommand cmd = new OdbcCommand();

        cmd.Connection = con;
        cmd.CommandText = "INSERT INTO GAMEINSTANCE(GameID, Difficulty, Duration) VALUES(" + gid + ", '" + difficulty + "', " + time + ")";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "SELECT COUNT(*) FROM GAMEINSTANCE";
        OdbcDataReader read = cmd.ExecuteReader();
        read.Read();
        int total = read.GetInt32(0);
        read.Close();

        cmd.CommandText = "SELECT GameInstanceID FROM GAMEINSTANCE";
        read = cmd.ExecuteReader();
        
        while(count < total - 1)
        {
            read.Read();
            count++;
        }

        read.Read();
        ginstid = read.GetInt32(0);

        GameDataHelper.GameInstance gi = new GameDataHelper.GameInstance();
        gi.m_gameInstanceID = ginstid;
        gi.m_gameID = gid;
        gi.m_difficulty = difficulty;
        gi.m_duration = time;

        GameDataHelper.setCurrentGame(gi);

        return gi;       
    }

    /**
    * @brief Returns Game Titles
    * Searches through the gamelist and checks if the gameID parameter is
     * within the list. Returns the title of the game if found else returns
     * "Title Not Found"
     * 
    *
    * @param int gameID
    * @return string
    * @pre
    * @post
    */
    public string getGameTitle(int gameID)
    {
        for(int i = 0; i < m_gameList.Count; i++)
        {
            if(gameID == m_gameList.ElementAt(i).m_id)
            {
                return m_gameList.ElementAt(i).m_title;
            }
        }

        return "Title Not Found";
    }

    /**
    * @brief Closes Database Connection
    * Closes the connection to the database
    *
    * @param
    * @return void
    * @pre
    * @post
    */
    public void close()
    {
        con.Close();
    }
}