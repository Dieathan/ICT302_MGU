using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {
    public static GameMenu instance = null;

    public Transform player;
    public Transform cameraCentre;

    public float distance = 15.0f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
	void Start () {
        transform.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void RequestOpenMenu()
    {
        if (!transform.gameObject.activeInHierarchy)
        {
            MenuDisplayAdjustment();
            transform.gameObject.SetActive(true);
        }
    }

    public void RequestCloseMenu()
    {
        if (transform.gameObject.activeInHierarchy)
        {
            transform.localPosition.Set(.0f, .0f, .0f);
            transform.localRotation.Set(.0f, .0f, .0f, .0f);
            transform.gameObject.SetActive(false);
        }
    }

    private void MenuDisplayAdjustment()
    {
        transform.localPosition = player.localPosition + cameraCentre.forward * distance;
        Vector3 reversePlayerRotation = new Vector3(-(player.localRotation.eulerAngles.x / 2),
            -(player.localRotation.eulerAngles.y / 2),
            -(player.localRotation.eulerAngles.z / 2));
        transform.localRotation = cameraCentre.localRotation * Quaternion.Euler(reversePlayerRotation);
    }
}
