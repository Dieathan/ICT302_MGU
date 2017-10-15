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
        selectedAracde = "";
        isOpenMenu = false;
    }

	// Update is called once per frame
	void Update () {
        CheckEnterGameScene();
        CheckOpenMenu();
    }

    public void SelectArcade(string arcadeName)
    {
        selectedAracde = arcadeName;
    }

    private void CheckEnterGameScene()
    {
        if (selectedAracde != "")
        {
            Debug.Log("CheckEnterGameScene() - " + selectedAracde);
            if (selectedAracde == "Shooter Arcade")
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

    private string selectedAracde;

    private bool isOpenMenu;
}
