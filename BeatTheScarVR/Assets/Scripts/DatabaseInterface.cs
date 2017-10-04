
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

    private List<Game> m_gameList;
    private List<GameInstance> m_programGames;
    private List<string> m_restrictions;
    private string m_userID;

    private SqliteConnection con;

    public DatabaseInterface()
    {
        con = new SqliteConnection("URI=file:..\\KinesisArcade.sqlite");
        con.Open();
        m_gameList = new List<Game>();
        m_programGames = new List<GameInstance>();
        m_restrictions = new List<string>();
        setUser();
        buildGameList();
        setProgramList();
    }

    private void setUser()
    { 
        StreamReader sr = File.OpenText("..\\currentuser.txt");
        m_userID = sr.ReadLine();
    }

    /**
     * @param String url
     */
    private void addPatientData(GameInstance game, string userid, int score, int timePlayed, string videourl)
    {
        SqliteCommand cmd1 = new SqliteCommand();
        cmd1.CommandText = "INSERT INTO PATIENTDATA(GameID, UserID, Score, TimePlayed, VideoLocation) VALUES(" + game.m_gameID + ", '" + userid + "', " + score + ", " + timePlayed + ", '" + videourl + "')";
        cmd1.ExecuteNonQuery();

        SqliteCommand cmd2 = new SqliteCommand();
        cmd2.CommandText = "UPDATE GAMEINSTANCE SET Completed = 1 WHERE GameInstanceID = " + game.m_gameInstanceID;
    }

    /**
     * 
     */
     private void buildGameList()
    {
        Game game;

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
            game = new Game();
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
    private List<Game> getGameList()
    {
        return m_gameList;
    }

    /**
     * 
     */
    private bool programComplete()
    {
        bool check = true;

        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT ProgramID FROM PATIENT WHERE UserID = " + m_userID;
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
        GameInstance game;

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
            game = new GameInstance();
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
    private List<GameInstance> getProgramGameList()
    {
        return m_programGames;
    }

    /**
     * 
     */
    private List<string> getRestrictions()
    {
        return m_restrictions;
    }

	public struct Game
    {
        public int m_id;
        public string m_coordinates;
        public string m_title;
		public string m_description;
    }

    public struct GameInstance
    {
        public int m_gameInstanceID;
        public int m_gameID;
        public string m_difficulty;
        public int m_duration;
        public bool m_completed;
    }
	
}