
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
    }
    /**
     * 
     */
    private static bool m_kinectRecord;

    private static string m_url;

    private static GameInstance m_currentGame;

    /**
     * @param int score 
     * @param int time
     */
    public static bool AddMetricsToDatabase(GameInstance game, int score, int time)
    {
        DatabaseInterface dbInterface = new DatabaseInterface();
        bool success = false;

        if(m_kinectRecord)
        {
            setRecord(false);
        }

        dbInterface.addPatientData(game, score, time, m_url);

        return success;
    }

    /**
     * @param bool isRecording
     */
    public static void setRecord(bool record)
    {
        m_kinectRecord = record;

        if(m_kinectRecord)
        {
            //turn on kinect recording
        }
        else
        {
            //turn off kinect recording
            m_url = "";
        }
    }

    public static bool getRecord()
    {
        return m_kinectRecord;
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