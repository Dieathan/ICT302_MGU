using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurationMenu : MonoBehaviour {

    public Transform player;
    public Transform cameraCentre;
    public GameObject Thirty;
    public GameObject Sixty;
    public GameObject Ninety;
    public GameObject OneTwenty;
    private GameManagementScript gms;

    public float distance = 15.0f;

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

    public void SetThirty()
    {
        gms.SetDuration(30);
        SetWhite();
        Thirty.GetComponent<Button>().image.color = Color.yellow;
    }

    public void SetSixty()
    {
        gms.SetDuration(60);
        SetWhite();
        Sixty.GetComponent<Button>().image.color = Color.yellow;
    }

    public void SetNinety()
    {
        gms.SetDuration(90);
        SetWhite();
        Ninety.GetComponent<Button>().image.color = Color.yellow;
    }

    public void SetOneTwenty()
    {
        gms.SetDuration(120);
        SetWhite();
        OneTwenty.GetComponent<Button>().image.color = Color.yellow;
    }

    private void SetWhite()
    {
        Thirty.GetComponent<Button>().image.color = Color.white;
        Sixty.GetComponent<Button>().image.color = Color.white;
        Ninety.GetComponent<Button>().image.color = Color.white;
        OneTwenty.GetComponent<Button>().image.color = Color.white;

    }
}
