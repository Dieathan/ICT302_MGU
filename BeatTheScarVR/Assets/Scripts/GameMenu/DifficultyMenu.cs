using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyMenu : MonoBehaviour
{

    public Transform player;
    public Transform cameraCentre;
    public GameObject Easy;
    public GameObject Medium;
    public GameObject Hard;
    public GameObject gms;

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

    public void SetEasy()
    {
        gms.GetComponent<GameManagementScript>().SetDifficulty(1);
        SetWhite();
        Easy.GetComponent<Button>().image.color = Color.yellow;
    }

    public void SetMedium()
    {
        gms.GetComponent<GameManagementScript>().SetDifficulty(2);
        SetWhite();
        Medium.GetComponent<Button>().image.color = Color.yellow;
    }

    public void SetHard()
    {
        gms.GetComponent<GameManagementScript>().SetDifficulty(3);
        SetWhite();
        Hard.GetComponent<Button>().image.color = Color.yellow;
    }

    private void SetWhite()
    {
        Easy.GetComponent<Button>().image.color = Color.white;
        Medium.GetComponent<Button>().image.color = Color.white;
        Hard.GetComponent<Button>().image.color = Color.white;

    }
}
