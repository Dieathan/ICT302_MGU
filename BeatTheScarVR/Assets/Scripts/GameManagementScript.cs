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
    }

	// Update is called once per frame
	void Update () {
        if(selectedAracde != "")
        {
            Debug.Log(selectedAracde);
            if (selectedAracde == "Shooter Arcade")
                SceneManager.LoadScene("Game");
        }
        
    }

    public void SelectArcade(string arcadeName)
    {
        selectedAracde = arcadeName;
    }

    private string selectedAracde;
}
