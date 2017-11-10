using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SihouetteGameManagement : MonoBehaviour {
    public BodyPointManagement bpm;
    public GameScoreScript gameScore;
    public GameTimeScript gameTime;
    public PauseMenu pauseMenu;
    public EndMenu endMenu;
    

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
                setHard();
                break;
        }

        gameTime.SetTime(GameDataHelper.getCurrentGame().m_duration);
        //gameTime.SetTime(5.0f);
    }

    void OnDelete()
    {
        
    }

    void Start()
    {
        gameTime.StartCountDown();
        isOpenMenu = false;
        nextWallIndex = 0;
        if (walls.Length <= 1) isRandomWall = false;
        // hide all walls first
        foreach (Transform wall in walls)
        {
            wall.GetComponent<WallManagement>().RequestHideWall();
        }
        RequestNewWall();
        RequestPauseGame(true);
    }

    // Update is called once per frame
    void Update()
    {
        CheckOpenMenu();
    }

    public void RequestNewWall()
    {
        if (isRandomWall)
        {
            int i = (int)Random.Range(.0f, (float)(walls.Length-.1f));
            currentWallIndex = i;
            walls[i].GetComponent<WallManagement>().RequestShowWall(wallSpeed);
        }
        else
        {
            nextWallIndex += 1;
            // array out of bound
            if (nextWallIndex >= walls.Length) nextWallIndex = 0;
            currentWallIndex = nextWallIndex;
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
        endMenu.RequestOpenMenu();
        PauseGame(true);
    }

    public void SetIsOpenMenu(bool val)
    {
        isOpenMenu = val;
    }

    public bool GetIsOpenMenu()
    {
        return isOpenMenu;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Arcade");
    }

    public void CheckRestartGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    private void CheckOpenMenu()
    {

        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (!isOpenMenu)
            {
                pauseMenu.RequestOpenMenu();
                isOpenMenu = true;             
            }
            else
            {
                pauseMenu.RequestCloseMenu();
                isOpenMenu = false;
            }
            PauseGame(isOpenMenu);
        }
    }

    private void PauseGame(bool pause)
    {
        if(pause)
        {
            walls[currentWallIndex].GetComponent<WallManagement>().SetWallSpeed(0.0f);
            gameTime.StopCountDown();
        }
        else
        {
            walls[currentWallIndex].GetComponent<WallManagement>().SetWallSpeed(oldWallSpeed);
            gameTime.StartCountDown();
        }
    }

    public void RequestPauseGame(bool pause)
    {
        PauseGame(pause);
    }

    private void setEasy()
    {
        // Set wall speed to a slow speed
        wallSpeed = 0.2f;
        oldWallSpeed = 0.2f;
    }

    private void setMedium()
    {
        // Set wall speed to a moderate speed
        wallSpeed = 0.35f;
        oldWallSpeed = 0.35f;
    }

    private void setHard()
    {
        // Set wall speed to a fast speed
        wallSpeed = 0.5f;
        oldWallSpeed = 0.5f;
    }

    private GameDataHelper.GameInstance game;
    private float wallSpeed;
    private float oldWallSpeed;
    private Transform currentWall;
    private int amountOfWalls;
    private int nextWallIndex;
    private bool isOpenMenu;
    private int currentWallIndex;
}
