using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManagement : MonoBehaviour {
    public float wallMoveSpd;
    public SihouetteGameManagement sgm;
    public BodyPointManagement bpm;

	// Use this for initialization
	void Start () {
	}
    
    // Update is called once per frame
    void Update() {
        WallMovementUpdate();
        WallCheck();
        
    }

    public void RequestHideWall()
    {
        transform.localPosition = new Vector3(.0f, .0f, -90.0f);
        wallMoveSpd = .0f;
    }

    public void RequestShowWall(float spd)
    {
        transform.localPosition = new Vector3(.0f, .0f, 0.0f);
        wallMoveSpd = spd;
    }

    public void SetWallSpeed(float spd)
    {
        wallMoveSpd = spd;
    }

    private void WallMovementUpdate()
    {
        // Come close to the player
        transform.localPosition = new Vector3(
                (transform.localPosition.x) - wallMoveSpd,
                transform.localPosition.y,
                transform.localPosition.z
            );
    }

    private void WallCheck()
    {
        // Wall end
        if (transform.localPosition.x < wallEndDistance)
        {
            RequestHideWall();
            // fail with wall hit
            if (bpm.IsBodyCollided())
            {
                sgm.ScoreCheck(false);
            }

            // successfully through the wall
            else
            {
                sgm.ScoreCheck(true);
            }
            bpm.ResetBodyCollided();
        }
    }

    private float wallEndDistance = -425.0f;
}
