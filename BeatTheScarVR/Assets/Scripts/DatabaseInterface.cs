
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

    List<Game> m_gameList;
    List<GameInstance> m_incompleteGames;
    List<string> m_restrictions;
    bool m_canFreePlay;
    string m_userID;

    SqliteConnection con;

    public DatabaseInterface()
    {
        con = new SqliteConnection("URI=file:..\\KinesisArcade.sqlite");
        con.Open();
        getUser();
    }

    private void getUser()
    { 
        StreamReader sr = File.OpenText("..\\currentuser.txt");
        m_userID = sr.ReadLine();
        Debug.Log(m_userID);
    }

    /**
     * @param String url
     */
    private void addRecordingURL(string url, GameInstance game)
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.CommandText = "SELECT  ";
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
    private void buildGameList()
    {
        // TODO implement here
    }

    /**
     * 
     */
    private void completeProgram()
    {
        // TODO implement here
    }

    /**
     * 
     */
    private List<GameInstance> getIncompleteGameList()
    {
        return m_incompleteGames;
    }

    /**
     * 
     */
    private bool checkCanFreePlay()
    {
        return m_canFreePlay;
    }

    /**
     * 
     */
    private List<string> getRestrictions()
    {
        // TODO implement here
        return m_restrictions;
    }

    /**
     * @param List[GameInstances] games
     */
    private void processGamesList(List<GameInstance> games)
    {
        // TODO implement here
    }

	public struct Game
    {
		public string m_title;
		public int m_id;
		public string m_description;
    }

    public struct GameInstance
    {
        int gameTitle;
        string m_difficulty;
        string m_duration;
        bool m_isComplete;
    }
	
}