
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    /**
    * @class GameLogic
    * @brief Contains all game logic used by the game. Handles functionality of scoring
     * as well as changing game difficulty. Inherits from Unity's MonoBehaviour class.
    *
    * @author Geoff Hanson / Jonathan Sands / MGU
    * @version 1
    * @date 10/11/17
    *
    */
public class GameLogic : MonoBehaviour
{

    GameDataHelper.GameInstance game; // Game Instance object
    int playerScore = 0; // Used to store players score
    int time = 0; // Time incrementer
    double wallSpeed = 0; // Used to store speed of the wall for difficulty settings
    AudioClip success; // Audio clip for success in scoring
    AudioClip fail; // Audio clip for failure in scoring
    AudioSource audioSource; // Audio Source object

    /**
    * @brief Overloaded Start Function
     * Initialises the Game Logic class. Sets the game instance, begins the Kinect recording,
     * sets the game difficulty and initialises audio features.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    void Start()
    {
        game = GameDataHelper.getCurrentGame();
        GameDataHelper.record();
        switch (game.m_difficulty){
            case 1:
                setEasy();
                break;
            case 2:
                setMedium();
                break;
            case 3:
                setHard();
                break;
        }
        audioSource = GetComponent<AudioSource>();
    }

    /**
    * @brief Overloaded OnDelete Function
     * Adds the metrics of the game to database that is playerScore and time.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    void OnDelete()
    {
        GameDataHelper.AddMetricsToDatabase(playerScore, time);
    }

    /**
    * @brief Overloaded Update Function
     * Update is called once per frame. Currently no use in class.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    void Update()
    {

    }

    /**
    * @brief Sets the Difficulty to Easy
     * Changes the speed of the wall to 0.10 to make the game easy.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    void setEasy()
    {
        // Set wall speed to a slow speed
        wallSpeed = 0.10;
    }

    /**
    * @brief Sets the Difficulty to Medium
     * Changes the speed of the wall to 0.25 to make the game medium.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    void setMedium()
    {
        // Set wall speed to a moderate speed
        wallSpeed = 0.25;    
    }

    /**
    * @brief Sets the Difficulty to Hard
     * Changes the speed of the wall to 0.50 to make the game hard.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    void setHard()
    {
        // Set wall speed to a fast speed
        wallSpeed = 0.50;    
    }

    /**
    * @brief Scoring Function
     * Takes the parameter given bool check and if true, increments the score of the
     * player by 100 and play success audio cue, else play fail audio cue.
     * 
    * @param bool check
    * @return void
    * @pre
    * @post
    */
    void Score(bool check)
    {
        if (check == true)
        {
            playerScore = playerScore + 100;
            audioSource.PlayOneShot(success, 1.0f);
        }
        else
        {
            audioSource.PlayOneShot(fail, 1.0f);
        }
    }
}