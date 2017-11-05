using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPointManagement : MonoBehaviour {
    public BodyView bodyViewObj = null;
    public LayerMask grabMask;

    // Use this for initialization
    void Start () {
        collideCount = 0;
        isBodyDetected = false;
        isBodyCollided = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (bodyViewObj.GetBodies() != null)
        {
            isBodyDetected = true;
            CheckBodyCollision();
        }
        else
        {
            isBodyDetected = false;
        }
    }

    public bool IsBodyCollided() { return isBodyCollided; }
    public void ResetBodyCollided() { isBodyCollided = false; collideCount = 0; }
    public bool IsBodyDetected() { return isBodyDetected; }

    private void CheckBodyCollision()
    {
        Dictionary<ulong, GameObject> bodiesObj = bodyViewObj.GetBodies();
        
        foreach (KeyValuePair<ulong, GameObject> bodyObj in bodiesObj)
        {
            foreach (Transform child in bodyObj.Value.transform)
            {
                Vector3 forward = child.TransformDirection(Vector3.forward);
                forward = Quaternion.Euler(.0f, 90.0f, .0f) * forward;
                Debug.DrawRay(child.position, forward * 10.0f, Color.green);

                // check if body part is going to be collide - ray
                RaycastHit hit;

                if (Physics.Raycast(child.position, forward, out hit))
                {
                    if (hit.collider.gameObject.CompareTag("SihouetteWall"))
                    {
                        child.GetComponent<Renderer>().material.color = Color.red;
                    }
                }
                else
                {
                    child.GetComponent<Renderer>().material.color = Color.blue;
                }


                // check if body part is colliding to the wall - touching
                RaycastHit[] hits;

                hits = Physics.SphereCastAll(child.position, .0f, forward, .0f, grabMask);

                if(hits.Length > 0)
                {
                    collideCount++;
                }
            }
        }

        if(collideCount > 0)
        {
            isBodyCollided = true;
        }
        else
        {
            isBodyCollided = false;
        }

    }


    private bool isBodyDetected;
    
    private bool isBodyCollided;
    private int collideCount;
}
