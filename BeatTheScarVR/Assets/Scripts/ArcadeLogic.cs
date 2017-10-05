
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
    private DatabaseInterface m_databaseInterface;
    private Security m_security;
    private ProgramHelper m_programHelper;
    private List<DatabaseInterface.Game> m_gameList;

    // Use this for initialization
    void Start()
    {
        m_databaseInterface = new DatabaseInterface();
        m_security = new Security();
        m_programHelper = new ProgramHelper(m_databaseInterface.getProgramGameList());
        m_gameList = m_databaseInterface.getGameList();
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