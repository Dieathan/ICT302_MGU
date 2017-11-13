using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /**
    * @class ProgramMenu
    * @brief Contains all the functionality of the Program Menu. Inherits from
    * Unity's MonoBehaviour class.
    *
    * @author Benjamin Mak / MGU
    * @version 1
    * @date 10/11/17
    *
    */
public class ProgramMenu : MonoBehaviour
{
    public Transform player; // Holds player coordinates
    public Transform cameraCentre; // Holds camera coordinates
    public GameObject gms; // Game management script object
    public GameObject prog; // Program game object
    private int difficulty; // Used to identify the difficulty level
    private int duration; // Used to identify the time duration 

    public float distance = 15.0f; // Used to store the distance the menu is from the camera

    /**
    * @brief Overloaded Start Function
     * Initialises the game object attribute set active to false and sets the program
     * text.
     * 
    * @param
    * @return
    * @pre
    * @post
    */
    void Start()
    {
        transform.gameObject.SetActive(false);
        SetProgramText();
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
    * @brief Call to Close Menu
     * If game object is active, reset local position of game object and set
     * active to false.
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
    * @brief Call to Enter Game
     * Sets enter game to true.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void EnterGame()
    {
        gms.GetComponent<GameManagementScript>().SetEnterGame();
    }

    /**
    * @brief Sets Program Text
     * Sets the details of this instance of the program.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void SetProgramText()
    {
        prog.GetComponent<Text>().text = "Program\n" + gms.GetComponent<GameManagementScript>().gameInstanceDetails();
    }
}
