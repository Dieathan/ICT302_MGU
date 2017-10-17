
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
 * 
 */
public class DatabaseInterface
{

    private List<GameDataHelper.Game> m_gameList;
    private List<GameDataHelper.GameInstance> m_programGames;
    private List<string> m_restrictions;
    private string m_userID;

    private OdbcConnection con;

    public DatabaseInterface()
    {
        con = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=C:..\\KinesisArcade.mdb");
        con.Open();
        m_gameList = new List<GameDataHelper.Game>();
        m_programGames = new List<GameDataHelper.GameInstance>();
        m_restrictions = new List<string>();
        setUser();
        buildGameList();
        setProgramList();
        setRestrictions();
    }

    public DatabaseInterface(int x)
    {

    }

    private void setUser()
    { 
        StreamReader sr = File.OpenText("..\\currentuser.txt");
        m_userID = sr.ReadLine();
        GameDataHelper.setPatient(m_userID);
    }

    /**
     * @param String url
     */
    public void addPatientData(GameDataHelper.GameInstance game, int score, int timePlayed, string videourl)
    {
        OdbcCommand cmd1 = new OdbcCommand();
        cmd1.CommandText = "INSERT INTO PATIENTDATA(GameID, UserID, Score, TimePlayed, VideoLocation) VALUES(" + game.m_gameID + ", '" + m_userID + "', " + score + ", " + timePlayed + ", '" + videourl + "')";
        cmd1.ExecuteNonQuery();

        OdbcCommand cmd2 = new OdbcCommand();
        cmd2.CommandText = "UPDATE GAMEINSTANCE SET Completed = 1 WHERE GameInstanceID = " + game.m_gameInstanceID;
        cmd2.ExecuteNonQuery();
    }

    /**
     * 
     */
     private void buildGameList()
    {
        GameDataHelper.Game game;

        OdbcCommand cmd = new OdbcCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT COUNT(*) FROM GAME";
        OdbcDataReader read = cmd.ExecuteReader();
        read.Read();
        int count = read.GetInt32(0);
        read.Close();

        cmd.CommandText = "SELECT * FROM GAME";
        read = cmd.ExecuteReader();

        for (int i = 0; i < count; i++)
        {
            read.Read();
            game = new GameDataHelper.Game();
            game.m_id = read.GetInt32(0);
            game.m_coordinates = read.GetString(1);
            game.m_title = read.GetString(2);
            game.m_description = read.GetString(3);
            game.m_coordinates = read.GetString(1);
            m_gameList.Add(game);
        }
    }

    /**
     * 
     */
    public List<GameDataHelper.Game> getGameList()
    {
        return m_gameList;
    }

    /**
     * 
     */
    public bool programComplete()
    {
        bool check = true;

        OdbcCommand cmd = new OdbcCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT ProgramID FROM PATIENT WHERE UserID = '" + m_userID + "'";
        OdbcDataReader read = cmd.ExecuteReader();
        int pid = read.GetInt32(0);

        cmd.CommandText = "SELECT COUNT(*) FROM GAMEINSTANCE WHERE ProgramID = " + pid;
        read = cmd.ExecuteReader();
        read.Read();
        int count = read.GetInt32(0);
        read.Close();

        cmd.CommandText = "SELECT Completed FROM GAMEINSTANCE WHERE ProgramID = " + pid;
        read = cmd.ExecuteReader();

        for (int i = 0; i < count; i++)
        {
            read.Read();

            if (!read.GetBoolean(0))
            {
                check = false;
            }
        };

        return check;
    }

    private void setProgramList()
    {
        GameDataHelper.GameInstance game;

        OdbcCommand cmd = new OdbcCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT COUNT(*) FROM GAMEINSTANCE";
        OdbcDataReader read = cmd.ExecuteReader();
        read.Read();
        int count = read.GetInt32(0);
        read.Close();

        cmd.CommandText = "SELECT * FROM GAMEINSTANCE";
        read = cmd.ExecuteReader();

        for (int i = 0; i < count; i++)
        {
            read.Read();
            game = new GameDataHelper.GameInstance();
            game.m_gameInstanceID = read.GetInt32(0);
            game.m_gameID = read.GetInt32(2);
            game.m_difficulty = read.GetString(3);
            game.m_duration = read.GetInt32(4);
            game.m_completed = read.GetBoolean(5);
            m_programGames.Add(game);
        }
    }

    /**
     * 
     */
    public List<GameDataHelper.GameInstance> getProgramGameList()
    {
        return m_programGames;
    }

    private void setRestrictions()
    {
        String game;

        OdbcCommand cmd = new OdbcCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT COUNT(*) FROM RESTRICTION";
        OdbcDataReader read = cmd.ExecuteReader();
        read.Read();
        int count = read.GetInt32(0);
        read.Close();

        cmd.CommandText = "SELECT * FROM RESTRICTION";
        read = cmd.ExecuteReader();

        for (int i = 0; i < count; i++)
        {
            read.Read();
            if(read.GetBoolean(3))
            {
                for(int j = 0; j < m_gameList.Count; j++)
                {
                    if(read.GetInt32(1) == m_gameList.ElementAt(j).m_id)
                    {
                        game = m_gameList.ElementAt(j).m_title;
                        m_restrictions.Add(game);
                    }
                }
                
            }
            
        }
    }

    /**
     * 
     */
    public List<string> getRestrictions()
    {
        return m_restrictions;
    }

    public bool canFreePlay()
    {
        bool check = false;

        OdbcCommand cmd = new OdbcCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT CanFreePlay FROM PATIENT WHERE UserID = '" + m_userID + "'";
        OdbcDataReader read = cmd.ExecuteReader();
        read.Read();
        check = read.GetBoolean(0);

        return check;
    }

	public GameDataHelper.GameInstance addGameInstance(string gameTitle, string difficulty, int time)
    {
        int gid = -1;
        int ginstid;
        int count = 0;
        OdbcCommand cmd = new OdbcCommand();

        for (int i = 0; i < m_gameList.Count; i++)
        {
            if(gameTitle.CompareTo(m_gameList.ElementAt(i).m_title) == 0)
            {
                gid = m_gameList.ElementAt(i).m_id;
            }
        }

        if (gid >= 0)
        {
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO GAMEINSTANCE(GameID, Difficulty, Duration) VALUES(" + gid + ", '" + difficulty + "', " + time + ")";
            cmd.ExecuteNonQuery();
        }

        cmd.CommandText = "SELECT COUNT(*) FROM GAMESINSTANCE";
        OdbcDataReader read = cmd.ExecuteReader();
        read.Read();
        int total = read.GetInt32(0);
        read.Close();

        cmd.CommandText = "SELECT GameInstance FROM GAMEINSTANCE";
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

        return gi;       
    }
	
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
}