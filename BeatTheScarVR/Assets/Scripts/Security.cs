
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /**
    * @class Security
    * @brief Contains all
    *
    * @author Geoff Hanson / MGU
    * @version 1
    * @date 10/11/17
    *
    */
public class Security 
{
    private DatabaseInterface m_dbInterface; // Database Interface object
    private List<int> m_restrictions; // List of int containing restrictions

    /**
    * @brief Parameter Constructor
     * Initialises the database security taking the parameter DatabaseInterface di and assigning it
     * to this m_dbInterface and then collects the restrictions set.
     * 
    * @param DatabaseInterface di
    * @return
    * @pre
    * @post
    */
    public Security(DatabaseInterface di)
    {
        m_dbInterface = di;
        m_restrictions = m_dbInterface.getRestrictions();

    }

    /**
    * @brief Checks Can Free Play Status
     * Returns true if program is complete and can free play are both true, else
     * returns false.
     * 
    * @param 
    * @return bool
    * @pre
    * @post
    */
	public bool canFreePlay()
    {
        bool check = false;

		if(m_dbInterface.programComplete() && m_dbInterface.canFreePlay())
        {
            check = true;
        }

        return check;
	}

    /**
    * @brief Checks Is Restricted Status
     * Loops through the list of restrictions checking the parameter given int gameID
     * against each restriction element. If IDs match, returns true, else no match
     * returns false.
     * 
    * @param
    * @return bool
    * @pre
    * @post
    */
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