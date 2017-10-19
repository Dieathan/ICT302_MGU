
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/**
 * 
 */
public class GameLogic : MonoBehaviour
{

    GameDataHelper.GameInstance game;
    int playerScore = 0;
    int time = 0;
    double wallSpeed = 0;
    // UnityCoordX wallCoordX
    // UnityCoordY wallCoordY
    // UnityCoordZ wallCoordZ
    AudioClip success;
    AudioClip fail;
    AudioSource audioSource;

    // Use this for initialization
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

    void OnDelete()
    {
        GameDataHelper.AddMetricsToDatabase(playerScore, time);
    }

    // Update is called once per frame
    void Update()
    {
        // When walls appear
        //     if (wallCoordX == currentWallCoordX)
        //         generateWall();
    }

    void setEasy()
    {
        // Set wall speed to a slow speed
        wallSpeed = 0.10;
    }

    void setMedium()
    {
        // Set wall speed to a moderate speed
        wallSpeed = 0.25;    
    }

    void setHard()
    {
        // Set wall speed to a fast speed
        wallSpeed = 0.50;    
    }

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

    /**
     * 
     */
    public GameLogic()
    {

    }


}