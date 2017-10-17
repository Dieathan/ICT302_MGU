using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagementScript : MonoBehaviour{
    public static GameManagementScript instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start () {
        selectedArcade = "";
        isOpenMenu = false;
    }

	// Update is called once per frame
	void Update () {
        CheckEnterGameScene();
        CheckOpenMenu();
    }

    public void SelectArcade(string arcadeName)
    {
        selectedArcade = arcadeName;
    }

    public void OVRCamRecenter()
    {
        OVRRecenterManagerScript.instance.RequestRecenter();
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

    private void CheckEnterGameScene()
    {
        if (selectedArcade != "")
        {
            Debug.Log("CheckEnterGameScene() - " + selectedArcade);
            if (selectedArcade == "Shooter Arcade")
                SceneManager.LoadScene("Game");
        }
    }

    private void CheckOpenMenu()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (!isOpenMenu)
            {
                GameMenu.instance.RequestOpenMenu();
                isOpenMenu = true;
            }
            else
            {
                GameMenu.instance.RequestCloseMenu();
                isOpenMenu = false;
            }
        }
    }

    private string selectedArcade;

    private bool isOpenMenu;
}
