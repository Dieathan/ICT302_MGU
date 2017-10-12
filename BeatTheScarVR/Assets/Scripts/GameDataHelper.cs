
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/**
 * 
 */
public static class GameDataHelper
{

    /**
     * 
     */
    static GameDataHelper()
    {
        m_kinectRecord = false;
        m_kvr = new KinectVideoRecording();
    }
    /**
     * 
     */
    private static bool m_kinectRecord;

    private static string m_url;

    private static string m_pid;

    private static GameInstance m_currentGame;

    private static KinectVideoRecording m_kvr;

    /**
     * @param int score 
     * @param int time
     */
    public static bool AddMetricsToDatabase(GameInstance game, int score, int time)
    {
        DatabaseInterface dbInterface = new DatabaseInterface(0);
        bool success = false;

        if(m_kinectRecord)
        {
            stopRecord();
        }

        dbInterface.addPatientData(game, score, time, m_url);

        return success;
    }

    public static void createGameInstance(string gameTitle, string difficulty, int time)
    {
        DatabaseInterface db = new DatabaseInterface();
        m_currentGame = db.addGameInstance(gameTitle, difficulty, time);
    }

    /**
     * @param bool isRecording
     */
    public static void record()
    {
        if(m_kinectRecord == false)
        {
            m_kvr.record();
        }

        m_kinectRecord = true;
    }

    private static void stopRecord()
    {
        m_url = m_kvr.close(m_pid, m_currentGame.m_gameID);
        m_kinectRecord = false;
    }

    public static bool getRecord()
    {
        return m_kinectRecord;
    }

    public static void setPatient(string pid)
    {
        m_pid = pid;
    }

    public struct Game
    {
        public int m_id;
        public string m_coordinates;
        public string m_title;
        public string m_description;
    }

    public struct GameInstance
    {
        public int m_gameInstanceID;
        public int m_gameID;
        public string m_difficulty;
        public int m_duration;
        public bool m_completed;
    }

}