
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

    private List<int> m_restrictions;

	public bool canFreePlay()
    {
        bool check = false;

		if(m_dbInterface.programComplete() && m_dbInterface.canFreePlay())
        {
            check = true;
        }

        return check;
	}
	
	public bool isRestricted(int gameID)
    {
        bool check = false;

        for(int i = 0; i < m_restrictions.Count; i++)
        {
            if(gameID == m_restrictions.ElementAt(i))
            {
                check = true;
            }
        }

        return check;
	}
	
}