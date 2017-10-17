using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using UnityEngine;

public class ProgramHelper
{
    public ProgramHelper(List<GameDataHelper.GameInstance> prog)
    {
        m_program = prog;
        setCurrentGame();
    }

    List<GameDataHelper.GameInstance> m_program;

    DatabaseInterface m_dbInterface;

    GameDataHelper.GameInstance m_currentGame;

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

    public GameDataHelper.GameInstance getNextGame()
    {
        return m_currentGame;
    }

    public string gameInstanceDetails(int index)
    {
        return m_dbInterface.getGameTitle(index) + "    " + m_program.ElementAt(index).m_difficulty + "    " + m_program.ElementAt(index).m_duration;
    }
}
