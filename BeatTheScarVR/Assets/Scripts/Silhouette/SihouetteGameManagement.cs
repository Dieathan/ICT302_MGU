using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SihouetteGameManagement : MonoBehaviour {
    public BodyPointManagement bpm = null;

    GameDataHelper.GameInstance game;
    int playerScore = 0;
    int time = 0;
    float wallSpeed = 0;
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
        //GameDataHelper.record();
        switch (game.m_difficulty)
        {
            case 1:
                setEasy();
                break;
            case 2:
                setMedium();
                break;
            case 3:
                setHard();
                break;
            default:
                setMedium();
                break;
        }
        audioSource = GetComponent<AudioSource>();


        GameObject wall01 = GameObject.Find("Wall01");
        wall01.transform.position =
            new Vector3(
                    30.0f,
                    10.0f,
                    -100.0f
                );
        wall01.GetComponent<WallManagement>().wallMoveSpd = wallSpeed;

        GameObject wall02 = GameObject.Find("Wall02");
        wall02.GetComponent<WallManagement>().isMove = false;
        wall02.GetComponent<WallManagement>().wallMoveSpd = wallSpeed;
    }

    void OnDelete()
    {
        GameDataHelper.AddMetricsToDatabase(playerScore, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestNewWall(string wallName)
    {
        if (wallName == "Wall01")
        {
            GameObject wall02 = GameObject.Find("Wall02");
            wall02.transform.position =
                new Vector3(
                    30.0f,
                    10.0f,
                    -100.0f
                );
            wall02.GetComponent<WallManagement>().isMove = true;
            bpm.wm = wall02.GetComponent<WallManagement>();
        }
        else
        {
            GameObject wall01 = GameObject.Find("Wall01");
            wall01.transform.position =
                new Vector3(
                    30.0f,
                    10.0f,
                    -100.0f
                );
            wall01.GetComponent<WallManagement>().isMove = true;
            bpm.wm = wall01.GetComponent<WallManagement>();
        }
    }

    public void Score(bool check)
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
        Debug.Log(playerScore);
    }

    private void setEasy()
    {
        // Set wall speed to a slow speed
        wallSpeed = 0.10f;
    }

    private void setMedium()
    {
        // Set wall speed to a moderate speed
        wallSpeed = 0.20f;
    }

    private void setHard()
    {
        // Set wall speed to a fast speed
        wallSpeed = 0.30f;
    }

    

    
}
