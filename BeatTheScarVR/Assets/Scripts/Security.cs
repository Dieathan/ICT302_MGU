
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
    public Security() {
    }

    /**
     * 
     */
    private string restrictionID;

    /**
     * 
     */
    private string userID;

    /**
     * 
     */
    private string gameID;

    /**
     * 
     */
    private bool isFound;

    /**
     * 
     */
    private bool isCompleted;

    /**
     * 
     */
    private bool isRestricted;

    /**
     * 
     */
    private bool isFreePlay;

	private bool canFreePlay(){
		if (isFreePlay == true)
			return true;
		else
			return false;
	}
	
	private bool isRestricted(string gameTitle){
		if (gameTitle.isRestricted == true)
			return true;
		else
			return false;
	}
	
}