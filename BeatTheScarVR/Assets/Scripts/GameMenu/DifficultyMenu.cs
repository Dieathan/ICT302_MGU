using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /**
    * @class DifficultyMenu
    * @brief Contains all the functionality of the Difficulty Menu. Inherits from
    * Unity's MonoBehaviour class.
    *
    * @author Benjamin Mak / MGU
    * @version 1
    * @date 10/11/17
    *
    */
public class DifficultyMenu : MonoBehaviour
{
    public Transform player; // Holds player coordinates
    public Transform cameraCentre; // Holds camera coordinates
    public GameObject gms; // Game management script object
    public GameObject Easy; // Easy game object
    public GameObject Medium; // Medium game object
    public GameObject Hard; // Hard game object

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
    * @brief Sets the Difficulty to Easy
     * Changes the button color to yellow when selected.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void SetEasy()
    {
        gms.GetComponent<GameManagementScript>().SetDifficulty(1);
        SetWhite();
        Easy.GetComponent<Button>().image.color = Color.yellow;
    }

    /**
    * @brief Sets the Difficulty to Medium
     * Changes the button color to yellow when selected.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void SetMedium()
    {
        gms.GetComponent<GameManagementScript>().SetDifficulty(2);
        SetWhite();
        Medium.GetComponent<Button>().image.color = Color.yellow;
    }

    /**
    * @brief Sets the Difficulty to Hard
     * Changes the button color to yellow when selected.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void SetHard()
    {
        gms.GetComponent<GameManagementScript>().SetDifficulty(3);
        SetWhite();
        Hard.GetComponent<Button>().image.color = Color.yellow;
    }

    /**
    * @brief Resets Color of Difficulty Buttons
     * Changes all the difficulty button colors to white.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    private void SetWhite()
    {
        Easy.GetComponent<Button>().image.color = Color.white;
        Medium.GetComponent<Button>().image.color = Color.white;
        Hard.GetComponent<Button>().image.color = Color.white;

    }
}
