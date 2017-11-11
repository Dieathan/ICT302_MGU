
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    /**
    * @class GameDataHelper
    * @brief Contains all the functionality between the Kinect and the database.
    *
    * @author Geoff Hanson / MGU
    * @version 1
    * @date 10/11/17
    *
    */
public static class GameDataHelper
{
    private static bool m_kinectRecord; // Identifies whether Kinect recording is true or false
    private static string m_url; // URL of video as string
    private static string m_pid; // Ppatient ID as string
    private static GameInstance m_currentGame; // The current game instance

    /**
    * @brief Default Constructor
     * Initialises recording to false and video url to an empty string.
     * 
    * @param
    * @return
    * @pre
    * @post
    */
    static GameDataHelper()
    {
        m_kinectRecord = false;
        m_url = "";
    }

    /**
    * @brief Add Metrics to Database
     * Adds the patient and corresponding patient game data to the database.
     * 
    * @param int score
     * @param int time
    * @return static bool
    * @pre
    * @post
    */
    public static bool AddMetricsToDatabase(int score, int time)
    {
        DatabaseInterface dbInterface = new DatabaseInterface(0);
        bool success = false;

        if(m_kinectRecord)
        {
            stopRecord();
        }

        dbInterface.addPatientData(m_currentGame, score, time, m_url);

        return success;
    }

    /**
    * @brief Adds Game Instance
     * Adds the current game instance to the database with the parameters.
     * 
    * @param int gameTitle
     * @param int difficulty
     * @param int time
    * @return void
    * @pre
    * @post
    */
    public static void addGameInstance(int gameTitle, int difficulty, int time)
    {
        DatabaseInterface db = new DatabaseInterface(0);
        m_currentGame = db.addGameInstance(gameTitle, difficulty, time);
    }

    /**
    * @brief Sets Recording to True
     * Sets m_kinectRecord to true.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public static void record()
    {
        m_kinectRecord = true;
    }

    /**
    * @brief Sets Recording to False
     * Sets m_kinectRecord to false.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    private static void stopRecord()
    {
        m_kinectRecord = false;
    }

    /**
    * @brief Returns Kinct Record
    *
    * @param
    * @return static bool
    * @pre
    * @post
    */
    public static bool getRecord()
    {
        return m_kinectRecord;
    }

    /**
    * @brief Returns Current Game Instance
    *
    * @param
    * @return static GameInstance
    * @pre
    * @post
    */
    public static GameInstance getCurrentGame()
    {
        return m_currentGame;
    }

    /**
    * @brief Sets Current Game Instance
     *  Takes the parameter GameInstance gi and assigns m_currentGame to it.
     *  
    * @param GameInstance gi
    * @return
    * @pre
    * @post
    */
    public static void setCurrentGame(GameInstance gi)
    {
        m_currentGame = gi;
    }

    /**
    * @brief Sets Patient ID
     * Takes the string parameter pid and assigns m_pid to it.
     * 
    * @param string pid
    * @return
    * @pre
    * @post
    */
    public static void setPatient(string pid)
    {
        m_pid = pid;
    }

    /**
    * @brief Returns Patient ID
    *
    * @param
    * @return static string
    * @pre
    * @post
    */
    public static string getPatient() 
    { 
        return m_pid; 
    }

    /**
    * @brief Game Struct
     * This stuct contains all the information about the game including ID, (XYZ) coordinates within unity,
     * title and description. Initialises all associated variables.
    *
    * @param
    * @return
    * @pre
    * @post
    */
    public struct Game
    {
        public int m_id;
        public int m_coordx;
        public int m_coordy;
        public int m_coordz;
        public string m_title;
        public string m_description;
    }

    /**
    * @brief Game Instance Struct
     * This stuct contains all the information about the game instance including the instance ID, game ID,
     * difficulty, duration and if it has been completed. Initialises all associated variables.
    *
    * @param
    * @return
    * @pre
    * @post
    */
    public struct GameInstance
    {
        public int m_gameInstanceID;
        public int m_gameID;
        public int m_difficulty;
        public int m_duration;
        public bool m_completed;
    }
}