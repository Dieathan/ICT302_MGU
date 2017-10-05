using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using UnityEngine;

public class ProgramHelper
{
    public ProgramHelper(List<DatabaseInterface.GameInstance> prog)
    {
        m_program = prog;
        setCurrentGame();
    }

    List<DatabaseInterface.GameInstance> m_program;

    DatabaseInterface m_dbInterface;

    DatabaseInterface.GameInstance m_currentGame;

    private void setCurrentGame()
    {
        int j = 0;

        do
        {
            if (!m_program.ElementAt(j).m_completed)
            {
                m_currentGame = m_program.ElementAt(j);
            }
            j++;
        } while (m_program.ElementAt(j).m_completed);
    }

    public DatabaseInterface.GameInstance getNextGame()
    {
        return m_currentGame;
    }
}
