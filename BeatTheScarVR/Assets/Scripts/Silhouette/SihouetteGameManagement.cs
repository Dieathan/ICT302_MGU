using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SihouetteGameManagement : MonoBehaviour {
    public BodyPointManagement bpm;
    public GameScoreScript gameScore;
    public GameTimeScript gameTime;

    public bool isRandomWall;
    public Transform[] walls;
    
    // Use this for initialization
    void Awake()
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

        gameTime.SetTime(GameDataHelper.getCurrentGame().m_duration);
        //gameTime.SetTime(10.0f);
    }

    void OnDelete()
    {
        
    }

    void Start()
    {
        gameTime.StartCountDown();
        nextWallIndex = 0;
        if (walls.Length <= 1) isRandomWall = false;
        // hide all walls first
        foreach (Transform wall in walls)
        {
            wall.GetComponent<WallManagement>().RequestHideWall();
        }
        RequestNewWall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestNewWall()
    {
        if (isRandomWall)
        {
            int i = (int)Random.Range(.0f, (float)(walls.Length-.1f));
            walls[i].GetComponent<WallManagement>().RequestShowWall(wallSpeed);
        }
        else
        {
            nextWallIndex += 1;
            // array out of bound
            if (nextWallIndex >= walls.Length) nextWallIndex = 0;
            walls[nextWallIndex].GetComponent<WallManagement>().RequestShowWall(wallSpeed);
        }
    }

    public void ScoreCheck(bool check)
    {
        if (check == true)
        {
            gameScore.AddScore(100);
            gameScore.PlaySuccessSound();
            RequestNewWall();
        }
        else
        {
            gameScore.PlayFailSound();
            RequestNewWall();
        }
    }

    public void GameFinish()
    {
        
        // parsing data to db
        //GameDataHelper.AddMetricsToDatabase(gameScore.GetScore(),
        //    (int)(gameTime.GetTime()));
    }

    private void setEasy()
    {
        // Set wall speed to a slow speed
        wallSpeed = 0.2f;
    }

    private void setMedium()
    {
        // Set wall speed to a moderate speed
        wallSpeed = 0.35f;
    }

    private void setHard()
    {
        // Set wall speed to a fast speed
        wallSpeed = 0.5f;
    }

    private GameDataHelper.GameInstance game;
    private float wallSpeed;
    private Transform currentWall;
    private int amountOfWalls;
    private int nextWallIndex;
}
