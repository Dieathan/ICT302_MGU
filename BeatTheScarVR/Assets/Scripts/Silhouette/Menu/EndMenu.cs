using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour {
    public SihouetteGameManagement sgm;

    // Use this for initialization
    void Start()
    {
        transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RequestOpenMenu()
    {
        if (!transform.gameObject.activeInHierarchy)
        {
            transform.gameObject.SetActive(true);
        }
    }

    public void RequestCloseMenu()
    {
        if (transform.gameObject.activeInHierarchy)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void RequestBackToArcade()
    {
        sgm.QuitGame();
    }

    public void RequestRestart()
    {
        sgm.CheckRestartGameScene();
    }

    
}
