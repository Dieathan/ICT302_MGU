using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMenu : MonoBehaviour {
    public SihouetteGameManagement sgm;

    // Use this for initialization
    void Start()
    {
        transform.gameObject.SetActive(true);
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
            sgm.RequestPauseGame(false);
        }
    }
}
