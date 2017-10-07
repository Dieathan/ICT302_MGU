
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine;

/**
 * 
 */
public class DatabaseInterface
{

    private List<GameDataHelper.Game> m_gameList;
    private List<GameDataHelper.GameInstance> m_programGames;
    private List<string> m_restrictions;
    private string m_userID;

    private SqliteConnection con;

    public DatabaseInterface()
    {
        con = new SqliteConnection("URI=file:..\\KinesisArcade.sqlite");
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
    }

    /**
     * @param String url
     */
    public void addPatientData(GameDataHelper.GameInstance game, int score, int timePlayed, string videourl)
    {
        SqliteCommand cmd1 = new SqliteCommand();
        cmd1.CommandText = "INSERT INTO PATIENTDATA(GameID, UserID, Score, TimePlayed, VideoLocation) VALUES(" + game.m_gameID + ", '" + m_userID + "', " + score + ", " + timePlayed + ", '" + videourl + "')";
        cmd1.ExecuteNonQuery();

        SqliteCommand cmd2 = new SqliteCommand();
        cmd2.CommandText = "UPDATE GAMEINSTANCE SET Completed = 1 WHERE GameInstanceID = " + game.m_gameInstanceID;
    }

    /**
     * 
     */
     private void buildGameList()
    {
        GameDataHelper.Game game;

        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT COUNT(*) FROM GAME";
        SqliteDataReader read = cmd.ExecuteReader();
        read.Read();
        int count = read.GetInt32(0);

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

        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT ProgramID FROM PATIENT WHERE UserID = '" + m_userID + "'";
        SqliteDataReader read = cmd.ExecuteReader();
        int pid = read.GetInt32(0);

        cmd.CommandText = "SELECT COUNT(*) FROM GAMEINSTANCE WHERE ProgramID = " + pid;
        read = cmd.ExecuteReader();
        read.Read();
        int count = read.GetInt32(0);

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

        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT COUNT(*) FROM GAMEINSTANCE";
        SqliteDataReader read = cmd.ExecuteReader();
        read.Read();
        int count = read.GetInt32(0);
        
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

        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT COUNT(*) FROM RESTRICTION";
        SqliteDataReader read = cmd.ExecuteReader();
        read.Read();
        int count = read.GetInt32(0);

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

        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT CanFreePlay FROM PATIENT WHERE UserID = '" + m_userID + "'";
        SqliteDataReader read = cmd.ExecuteReader();
        read.Read();
        check = read.GetBoolean(0);

        return check;
    }

	
	
}