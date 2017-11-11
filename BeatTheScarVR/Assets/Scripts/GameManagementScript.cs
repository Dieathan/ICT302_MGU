using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


    /**
    * @class GameManagementScript
    * @brief Contains all the functionality that is used within the scripts for Unity.
    *
    * @author Geoff Hanson / MGU
    * @version 1
    * @date 10/11/17
    *
    */
public class GameManagementScript : MonoBehaviour{
    public static GameManagementScript instance = null;
    public ArcadeGameMenu agm;
    public ProgramMenu pm;
    public GameMenu gameMenu;
    public OVRRecenterManagerScript ovrRecenterManager;

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
     * Initialises the Game Management variables. Sets the selectedArcade to null, enterGame to false,
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
     * Update is called once per frame and runs check functions for the game including checking
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
    * @brief Reques
    *
    * @param
    * @return
    * @pre
    * @post
    */
    public void OpenProgramMenu()
    {
        pm.RequestOpenMenu();
    }

    /**
    * @brief
    *
    * @param
    * @return
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
    * @brief
    *
    * @param
    * @return
    * @pre
    * @post
    */
    public void SetDifficulty(int diff)
    {
        difficulty = diff;
    }

    /**
    * @brief
    *
    * @param
    * @return
    * @pre
    * @post
    */
    public void SetDuration(int dura)
    {
        duration = dura;
    }

    /**
    * @brief
    *
    * @param
    * @return
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
    * @brief
    *
    * @param
    * @return
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
    private bool isOpenMenu;
    private bool enterGame;
    private int difficulty;
    private int duration;
    private int selectedGameID;

    private DatabaseInterface m_dbInterface;
    private Security m_security;
    private List<GameDataHelper.Game> m_gameList;
    private bool m_progComplete;
}
