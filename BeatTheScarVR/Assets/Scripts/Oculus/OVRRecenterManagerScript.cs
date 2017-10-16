using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OVRRecenterManagerScript : MonoBehaviour {
    public static OVRRecenterManagerScript instance = null;

    public float countDownTime = 3.0f;
    public GameObject recenterButtonObj = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        current_time = .0f;
        isRequested = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isRequested)
        {
            current_time += Time.deltaTime;

            if (recenterButtonObj != null)
            {
                recenterButtonObj.GetComponent<Text>().text = ((int)(4.0f - current_time)).ToString();
            }

            if (current_time > countDownTime)
            {
                OVRManager.display.RecenterPose();
                current_time = .0f;
                isRequested = false;
                // reset button text
                recenterButtonObj.GetComponent<Text>().text = "Recenter";
                // close menu after recenter
                GameMenu.instance.RequestCloseMenu();
                GameManagementScript.instance.SetIsOpenMenu(false);
            }
        }
	}

    public void RequestRecenter()
    {
        isRequested = true;
        
    }

    private float current_time;
    private bool isRequested;
}
