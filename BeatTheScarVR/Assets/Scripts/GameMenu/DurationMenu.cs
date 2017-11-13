using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /**
    * @class DurationMenu
    * @brief Contains all the functionality of the Duration Menu. Inherits from
    * Unity's MonoBehaviour class.
    *
    * @author Benjamin Mak / MGU
    * @version 1
    * @date 10/11/17
    *
    */
public class DurationMenu : MonoBehaviour 
{
    public Transform player; // Holds player coordinates
    public Transform cameraCentre; // Holds camera coordinates
    public GameObject gms; // Game management script object
    public GameObject Thirty; // Thirty game object
    public GameObject Sixty; // Sixty game object
    public GameObject Ninety; // Ninety game object
    public GameObject OneTwenty; // One Twenty game object

    public float distance = 15.0f; // Used to store the distance the menu is from the camera

    /**
    * @brief Overloaded Start Function
     * Initialises the game object attribute set active to false.
     * 
    * @param
    * @return
    * @pre
    * @post
    */
    void Start()
    {
        transform.gameObject.SetActive(false);
    }

    /**
    * @brief Overloaded Update Function
     * Called once per frame. Currently no implementation.
     * 
    * @param
    * @return
    * @pre
    * @post
    */
    void Update()
    {

    }

    /**
    * @brief Call to Open the Menu
     * If game object is not active, then set active to true.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void RequestOpenMenu()
    {
        if (!transform.gameObject.activeInHierarchy)
        {
            //MenuDisplayAdjustment();
            transform.gameObject.SetActive(true);
        }
    }

    /**
    * @brief Call to Close Menu
     * If game object is active, reset local position of game object and set
     * active to false.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void RequestCloseMenu()
    {
        if (transform.gameObject.activeInHierarchy)
        {
            transform.localPosition.Set(.0f, .0f, .0f);
            transform.localRotation.Set(.0f, .0f, .0f, .0f);
            transform.gameObject.SetActive(false);
        }
    }

    /**
    * @brief Adjusts the Menu Display
     * Changes the menu to be displayed in front of the players position according to which
     * direction they are looking.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    private void MenuDisplayAdjustment()
    {
        transform.localPosition = player.localPosition + cameraCentre.forward * distance;
        Vector3 reversePlayerRotation = new Vector3(-(player.localRotation.eulerAngles.x / 2),
            -(player.localRotation.eulerAngles.y / 2),
            -(player.localRotation.eulerAngles.z / 2));
        transform.localRotation = cameraCentre.localRotation * Quaternion.Euler(reversePlayerRotation);
    }

    /**
    * @brief Sets the Duration to Thirty
     * Changes the button color to yellow when selected.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void SetThirty()
    {
        gms.GetComponent<GameManagementScript>().SetDuration(30);
        SetWhite();
        Thirty.GetComponent<Button>().image.color = Color.yellow;
    }

    /**
    * @brief Sets the Duration to Sixty
     * Changes the button color to yellow when selected.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void SetSixty()
    {
        gms.GetComponent<GameManagementScript>().SetDuration(60);
        SetWhite();
        Sixty.GetComponent<Button>().image.color = Color.yellow;
    }

    /**
    * @brief Sets the Duration to Ninety
     * Changes the button color to yellow when selected.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void SetNinety()
    {
        gms.GetComponent<GameManagementScript>().SetDuration(90);
        SetWhite();
        Ninety.GetComponent<Button>().image.color = Color.yellow;
    }

    /**
    * @brief Sets the Duration to One Twenty
     * Changes the button color to yellow when selected.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void SetOneTwenty()
    {
        gms.GetComponent<GameManagementScript>().SetDuration(120);
        SetWhite();
        OneTwenty.GetComponent<Button>().image.color = Color.yellow;
    }

    /**
    * @brief Resets Color of Duration Buttons
     * Changes all the duration button colors to white.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    private void SetWhite()
    {
        Thirty.GetComponent<Button>().image.color = Color.white;
        Sixty.GetComponent<Button>().image.color = Color.white;
        Ninety.GetComponent<Button>().image.color = Color.white;
        OneTwenty.GetComponent<Button>().image.color = Color.white;
    }
}
