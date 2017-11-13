using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


    /**
    * @class GameManagementScript
    * @brief Contains all the functionality that is used within the scripts for Unity.
     * Inherits from the Unity's MonoBehaviour class.
    *
    * @author Geoff Hanson / MGU
    * @version 1
    * @date 10/11/17
    *
    */
public class GameManagementScript : MonoBehaviour{
    public static GameManagementScript instance = null; // Game Management Script object set to null
    public ArcadeGameMenu agm; // Arcade Game Menu object
    public ProgramMenu pm; // Program Menu object
    public GameMenu gameMenu; // Game Menu object
    public OVRRecenterManagerScript ovrRecenterManager; // OVR Recenter Management Script object

    /**
    * @brief Overloaded Awake Function
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        m_dbInterface = new DatabaseInterface();
        m_security = new Security(m_dbInterface);
    }

    /**
    * @brief Overloaded Start Function
     * Initialises the Game Management class. Sets the selectedArcade to null, enterGame to false,
     * isOpenMenu to false. If the current program is complete, will set the current game from the game
     * list.
    *
    * @param
    * @return void
    * @pre
    * @post
    */
    void Start () {
        selectedArcade = "";
        enterGame = false;
        isOpenMenu = false;

        
        m_progComplete = m_dbInterface.programComplete();

        if (!m_progComplete)
        {
            GameDataHelper.setCurrentGame(m_dbInterface.getCurrentGame());
        }

        m_gameList = m_dbInterface.getGameList();
    }

    /**
    * @brief Overloaded Update Function
     * Update is called once per frame and runs checks functions for the game including checking
     * the CheckEnterGameScene, CheckOptionMenu and CheckOpenArcadeGameMenu.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
	void Update () {
        CheckEnterGameScene();
        CheckOpenMenu();
        CheckOpenArcadeGameMenu();
    }

    /**
    * @brief Sets Selected Arcade
     * Assigns the selectedArcade to the string parameter arcadeName.
     * 
    * @param string arcadeName
    * @return void
    * @pre
    * @post
    */
    public void SelectArcade(string arcadeName)
    {
        selectedArcade = arcadeName;
    }

    /**
    * @brief Sets the Game ID
     * Assigns the selectedGameID to the parameter int gameID.
    * @param int gameID
    * @return void
    * @pre
    * @post
    */
    public void SetGameID(int gameID)
    {
        selectedGameID = gameID;
    }

    /**
    * @brief Recenters the OVR Camera
     * Calls to RequestRecenter.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void OVRCamRecenter()
    {
        ovrRecenterManager.RequestRecenter();
    }

    /**
    * @brief Quits the Game
    *
    * @param
    * @return void
    * @pre
    * @post
    */
    public void QuitGame()
    {
        Application.Quit();
    }

    /**
    * @brief Sets the bool if Menu is Open
     * Assigns isOpenMenu to the parameter bool val.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void SetIsOpenMenu(bool val)
    {
        isOpenMenu = val;
    }

    /**
    * @brief Returns bool of menu status
     * 
    * @param
    * @return bool
    * @pre
    * @post
    */
    public bool GetIsOpenMenu()
    {
        return isOpenMenu;
    }

    /**
    * @brief Checks status of Arcade Game Menu
     * Requests to open the menu if the selectedArade != "".
     * @todo I assume this needs to be depending on what game you are looking at not just the
     * shooter arcade.
    * @param
    * @return void
    * @pre
    * @post
    */
    private void CheckOpenArcadeGameMenu()
    {
       // if (m_security.canFreePlay())
        //{
            if (selectedArcade != "")
            {
                if (selectedArcade == "Shooter Arcade")
                    agm.RequestOpenMenu();
            }
       // }
    }

    /**
    * @brief Checks whether to Enter Game or not
     * If enterGame is true, a game instance is added to the GameDataHelper with the game
     * difficulty and duration. The SceneManager then loads the game scene.
    * @param
    * @return void
    * @pre
    * @post
    */
    private void CheckEnterGameScene()
    {
        if(enterGame)
        {
            GameDataHelper.addGameInstance(1, difficulty, duration);
            SceneManager.LoadScene("Game");
        }          
    }

    /**
    * @brief Sets Enter Game Status
     * Assigns the value of true to the enterGame variable.
    * @param
    * @return void
    * @pre
    * @post
    */
    public void SetEnterGame()
    {
        enterGame = true;
    }

    /**
    * @brief Requests to Open Menu
    *
    * @param
    * @return void
    * @pre
    * @post
    */
    public void OpenProgramMenu()
    {
        pm.RequestOpenMenu();
    }

    /**
    * @brief Checks if the Menu is Open
     * Checks if the user controller input 'Start Button' has been pressed and opens the menu
     * if not open already. Sets the isOpenMenu variable to true if open and false when closed.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    private void CheckOpenMenu()
    {
        
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (!isOpenMenu)
            {
                gameMenu.RequestOpenMenu();
                isOpenMenu = true;
            }
            else
            {
                gameMenu.RequestCloseMenu();
                isOpenMenu = false;
            }
        }
    }

    /**
    * @brief Sets the Difficulty of the Game
     * Assigns the parameter given diff to difficulty variable.
     * 
    * @param int diff
    * @return void
    * @pre
    * @post
    */
    public void SetDifficulty(int diff)
    {
        difficulty = diff;
    }

    /**
    * @brief Sets the Duration of the Game
     * Assigns the parameter given dura to duration variable.
     * 
    * @param int dura
    * @return void
    * @pre
    * @post
    */
    public void SetDuration(int dura)
    {
        duration = dura;
    }

    /**
    * @brief Sets the Game Instance Details
     * Assigns gameTitle and difficulty to empty strings. Checks the current game id and assigns the 
     * corresponding title to it to the m_gameID value. Gets the current game difficulty int and assigns
     * it a string "easy, medium or hard" (1, 2, 3) through a switch statement. Returns a string containing
     * all details about the game instance.
     * 
    * @param
    * @return string
    * @pre
    * @post
    */
    public string gameInstanceDetails()
    {
        string gameTitle = "";
        string difficulty = "";

        if(GameDataHelper.getCurrentGame().m_gameID == 1)
        {
            gameTitle = "Silhouette Wall";
        }
        else if(GameDataHelper.getCurrentGame().m_gameID == 2)
        {
            gameTitle = "Whack-A-Mole";
        }

        switch(GameDataHelper.getCurrentGame().m_difficulty)
        {
            case 1:
                difficulty = "Easy";
                break;
            case 2:
                difficulty = "Medium";
                break;
            case 3:
                difficulty = "Hard";
                break;              
        }

            

        return "Game: " + gameTitle + "\n" + 
               "Difficulty: " + difficulty + "\n" + 
               "Duration: " + GameDataHelper.getCurrentGame().m_duration + " seconds";
    }

    /**
    * @brief Returns Game ID
     * Loops through the game list and checks if the int X, Y, Z coordinates provided through the parameters
     * match any of the games. If so return the game ID of that game, else continue loop until no more games.
     * 
    * @param int x
     * @param int y
     * @param int z
    * @return int
    * @pre
    * @post
    */
    public int getGameID(int x, int y, int z)
    {
        for (int i = 0; i < m_gameList.Count; i++)
        {
            if (m_gameList.ElementAt(i).m_coordx == x && m_gameList.ElementAt(i).m_coordy == y && m_gameList.ElementAt(i).m_coordz == z)
            {
                return m_gameList.ElementAt(i).m_id;
            }
        }

        return 0;
    }

    private string selectedArcade; // String of selected arcade
    private bool isOpenMenu; // Bool for menu status, open/closed
    private bool enterGame; // Bool for enter game status, yes/no
    private int difficulty; // Used for level of difficulty, easy(1)/medium(2)/hard(3)
    private int duration; // Used to store duration of game
    private int selectedGameID; // Used to store selected game Id

    private DatabaseInterface m_dbInterface; // Database Interface object
    private Security m_security; // Securtiy object
    private List<GameDataHelper.Game> m_gameList; // List of games
    private bool m_progComplete; // Bool for program status, complete/incomplete
}
