using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagementScript : MonoBehaviour{
    public static GameManagementScript instance = null;
    public ArcadeGameMenu agm;
    public ProgramMenu pm;
    public GameMenu gameMenu;
    public OVRRecenterManagerScript ovrRecenterManager;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        m_dbInterface = new DatabaseInterface();
        m_security = new Security(m_dbInterface);
    }

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

	// Update is called once per frame
	void Update () {
        CheckEnterGameScene();
        CheckOpenMenu();
        CheckOpenArcadeGameMenu();
    }

    public void SelectArcade(string arcadeName)
    {
        selectedArcade = arcadeName;
    }

    public void SetGameID(int gameID)
    {
        selectedGameID = gameID;
    }

    public void OVRCamRecenter()
    {
        ovrRecenterManager.RequestRecenter();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetIsOpenMenu(bool val)
    {
        isOpenMenu = val;
    }

    public bool GetIsOpenMenu()
    {
        return isOpenMenu;
    }

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

    private void CheckEnterGameScene()
    {
        if(enterGame)
        {
            GameDataHelper.addGameInstance(1, difficulty, duration);
            SceneManager.LoadScene("Game");
        }          
    }

    public void SetEnterGame()
    {
        enterGame = true;
    }

    public void OpenProgramMenu()
    {
        pm.RequestOpenMenu();
    }

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

    public void SetDifficulty(int diff)
    {
        difficulty = diff;
    }

    public void SetDuration(int dura)
    {
        duration = dura;
    }

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

    private string selectedArcade;

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
