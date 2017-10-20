using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPointManagement : MonoBehaviour {
    public BodyView bodyViewObj = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (bodyViewObj.GetBodies() != null)
        {
            CheckBodyCollision();
        }
    }

    private void CheckBodyCollision()
    {
        Dictionary<ulong, GameObject> bodiesObj = bodyViewObj.GetBodies();
        foreach (KeyValuePair<ulong, GameObject> bodyObj in bodiesObj)
        {
            //GameObject targetBody = item.Value;
            //GameObject targetBodyHead = targetBody.transform.Find("HandRight").gameObject;
            //if (targetBodyHead != null)
            //{
            //    targetBodyHead.GetComponent<Renderer>().material.color = Color.yellow;
            //    Vector3 forward = targetBodyHead.transform.TransformDirection(Vector3.forward);
            //    forward = Quaternion.Euler(.0f, 90.0f, .0f) * forward;
            //    Debug.DrawRay(targetBodyHead.transform.position, forward * 10.0f, Color.green);
            //}
            //else
            //{
            //    Debug.Log("Cannot find target body head");
            //}
            foreach (Transform child in bodyObj.Value.transform)
            {
                Debug.Log(child.ToString());
            }
        }
    }
}
