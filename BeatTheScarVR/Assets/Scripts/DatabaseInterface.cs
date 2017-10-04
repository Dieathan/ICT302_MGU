
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

/**
 * 
 */
public class DatabaseInterface
{


    List<Game> m_gameList;
    List<GameInstance> m_incompleteGames;
    List<string> m_restrictions;
    bool m_canFreePlay;

    public DatabaseInterface()
    {

    }



    /**
     * @param String url
     */
    private void addRecordingURL(string url) {
        // TODO implement here
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