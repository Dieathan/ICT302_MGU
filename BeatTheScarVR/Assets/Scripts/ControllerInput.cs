using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerInput : MonoBehaviour {

    public Transform rHand, lHand;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        if ((Physics.Raycast(rHand.transform.position, rHand.transform.forward, out hit)) || (Physics.Raycast(lHand.transform.position, lHand.transform.forward, out hit)))
        {
            if (hit.collider.gameObject.CompareTag("Game"))
            {
                SceneManager.LoadScene(1);
            }
        }
	}
}
