using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManagement : MonoBehaviour {
    public float wallMoveSpd = .05f;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        WallMovementUpdate();
	}

    private void WallMovementUpdate()
    {
        if (transform.position.x < -60.0f)
        {
            transform.position =
                new Vector3(
                    .0f,
                    transform.position.y,
                    transform.position.z
                );

        }
        transform.position =
            new Vector3(
                (transform.position.x) - wallMoveSpd,
                transform.position.y,
                transform.position.z
            );
    }
}
