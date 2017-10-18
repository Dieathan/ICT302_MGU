
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
    private ProgramHelper m_programHelper;
    private List<GameDataHelper.Game> m_gameList;

    // Use this for initialization
    void Start()
    {
        m_dbInterface = new DatabaseInterface();
        m_security = new Security(m_dbInterface);
        m_programHelper = new ProgramHelper(m_dbInterface.getProgramGameList());
        m_gameList = m_dbInterface.getGameList();

        GameDataHelper.addGameInstance(m_gameList.ElementAt(0).m_id, 1, 30);
        GameDataHelper.AddMetricsToDatabase(20, 20);
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


}