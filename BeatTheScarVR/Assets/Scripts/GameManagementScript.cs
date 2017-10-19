using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagementScript : MonoBehaviour{
    public static GameManagementScript instance = null;
    public ArcadeGameMenu agm;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start () {
        selectedArcade = "";
        enterGame = false;
        isOpenMenu = false;
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

    public void OVRCamRecenter()
    {
        //OVRRecenterManagerScript.instance.RequestRecenter();
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
        if (selectedArcade != "")
        {
            Debug.Log("CheckEnterGameScene() - " + selectedArcade);
            if (selectedArcade == "Shooter Arcade")
                agm.RequestOpenMenu();
        }
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

    private void CheckOpenMenu()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (!isOpenMenu)
            {
                //GameMenu.instance.RequestOpenMenu();
                isOpenMenu = true;
            }
            else
            {
                //GameMenu.instance.RequestCloseMenu();
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

    private string selectedArcade;

    private bool isOpenMenu;

    private bool enterGame;

    private int difficulty;

    private int duration;
}
