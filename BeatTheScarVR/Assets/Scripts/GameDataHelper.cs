
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/**
 * 
 */
public class GameDataHelper
{

    /**
     * 
     */
    public GameDataHelper()
    {

    }

    /**
     * 
     */
    private List<DatabaseInterface.GameInstance> m_programGameList;

    /**
     * 
     */
    private string gameID;

    /**
     * 
     */
    private bool m_kinectRecord;

    /**
     * @param int score 
     * @param int time
     */
    public static bool AddMetricsToDatabase(int score, int time)
    {
        bool success = false;

        return success;
    }

    /**
     * @param bool isRecording
     */
    public static void KinectRecord(bool isRecording)
    {
        // TODO implement here
    }

}