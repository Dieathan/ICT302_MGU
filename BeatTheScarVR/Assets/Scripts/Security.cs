
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Security {

    /**
     * 
     */
    public Security(DatabaseInterface di)
    {
        m_dbInterface = di;
        m_restrictions = m_dbInterface.getRestrictions();

    }

    private DatabaseInterface m_dbInterface;

    private List<string> m_restrictions;

	private bool canFreePlay()
    {
        bool check = false;

		if(m_dbInterface.programComplete() && m_dbInterface.canFreePlay())
        {
            check = true;
        }

        return check;
	}
	
	private bool isRestricted(string gameTitle)
    {
        bool check = false;

        for(int i = 0; i < m_restrictions.Count; i++)
        {
            if(gameTitle.CompareTo(m_restrictions.ElementAt(i)) == 1)
            {
                check = true;
            }
        }

        return check;
	}
	
}