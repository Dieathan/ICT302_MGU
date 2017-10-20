using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManagement : MonoBehaviour {
    public float wallMoveSpd = .05f;
    public SihouetteGameManagement sgm;
    public BodyPointManagement bpm;

    public bool isMove = true;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        WallMovementUpdate();
	}

    public void CollideToWall()
    {
        sgm.Score(false);
        RequestNewWall();
    }

    private void RequestNewWall()
    {
        // reset & request new wall
        transform.position =
            new Vector3(
                30.0f,
                transform.position.y,
                transform.position.z + 93.0f
            );
        sgm.RequestNewWall(transform.name);
        isMove = false;
    }

    private void WallMovementUpdate()
    {
        if (transform.position.x < -60.0f)
        {
            // successfully through the wall
            sgm.Score(true);

            RequestNewWall();
        }

        if (isMove)
        {
            transform.position =
                new Vector3(
                    (transform.position.x) - wallMoveSpd,
                    transform.position.y,
                    transform.position.z
                );
        }
    }
}
