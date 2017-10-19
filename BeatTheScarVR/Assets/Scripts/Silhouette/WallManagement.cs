using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManagement : MonoBehaviour {
    public GameObject wall = null;
    public float wallMoveSpd = .05f;
    public BodyView bodyViewObj = null;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (wall != null)
        {
            if (wall.transform.position.x < -60.0f)
            {
                wall.transform.position =
                   new Vector3(
                       .0f,
                       wall.transform.position.y,
                       wall.transform.position.z
                   );

            }
            wall.transform.position =
                new Vector3(
                    (wall.transform.position.x) - wallMoveSpd,
                    wall.transform.position.y,
                    wall.transform.position.z
                );
        }

        if (bodyViewObj.GetBodies() != null) {
            Dictionary<ulong, GameObject> bodiesObj = bodyViewObj.GetBodies();
            foreach (KeyValuePair<ulong, GameObject> item in bodiesObj)
            {
                GameObject targetBody = item.Value;
                GameObject targetBodyHead = targetBody.transform.Find("Head").gameObject;
                if(targetBodyHead != null)
                {
                    
                }
                else
                {
                    Debug.Log("Cannot find target body head");
                }
            }
        }
	}
}
