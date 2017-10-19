
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/**
 * 
 */
public class ArcadeLogic : MonoBehaviour
{
    private DatabaseInterface m_dbInterface;
    private Security m_security;
    private List<GameDataHelper.Game> m_gameList;
    private bool m_progComplete = false;

    // Use this for initialization
    void Start()
    {
        m_dbInterface = new DatabaseInterface();
        m_security = new Security(m_dbInterface);
        m_progComplete = m_dbInterface.programComplete();

        if(!m_progComplete)
        {
            GameDataHelper.setCurrentGame(m_dbInterface.getCurrentGame());
        }
        
        m_gameList = m_dbInterface.getGameList();

        GameDataHelper.addGameInstance(m_gameList.ElementAt(0).m_id, 1, 30);
        GameDataHelper.AddMetricsToDatabase(20, 20);
        Debug.Log(m_dbInterface.programComplete());
        m_dbInterface.close();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * 
     */
    public ArcadeLogic()
    {

    }

    public string gameInstanceDetails(int gameID)
    {
        return m_dbInterface.getGameTitle(gameID) + "    " + GameDataHelper.getCurrentGame().m_difficulty + "    " + GameDataHelper.getCurrentGame().m_duration;
    }

    public void gameArcadePressed()
    {
        //GameDataHelper.addGameInstance(m_gameID, m_difficulty, m_duration);
    }

    public int getGameID(int x, int y, int z)
    {
        for(int i = 0; i < m_gameList.Count; i++)
        {
            if(m_gameList.ElementAt(i).m_coordx == x && m_gameList.ElementAt(i).m_coordy == y && m_gameList.ElementAt(i).m_coordz == z)
            {
                return m_gameList.ElementAt(i).m_id;
            }
        }

        return 0;
    }
}