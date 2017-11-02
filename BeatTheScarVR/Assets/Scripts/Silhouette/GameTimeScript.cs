using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeScript : MonoBehaviour {
    public SihouetteGameManagement sgm;

    float time = 60.0f;
    bool isCountDown;

    // Use this for initialization
    void Start () {
        isCountDown = false;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<TextMesh>().text = "Time: " + ((int)time).ToString();
        if (isCountDown)
        {
            time -= Time.deltaTime;
            if(time <= .0f)
            {
                time = .0f;
                isCountDown = false;
            }
        }
    }

    public float GetTime() { return time; }
    
    public void SetTime(float val) { time = val; }

    public void StartCountDown() { isCountDown = true; }

    public bool IsFinish() { return (time <= .0f); }
}
