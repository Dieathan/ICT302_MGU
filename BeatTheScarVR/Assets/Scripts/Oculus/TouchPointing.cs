using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPointing : MonoBehaviour
{
    public Transform hand;

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
            RaycastHand();
    }

    private void RaycastHand()
    {
        RaycastHit hit;

        if (Physics.Raycast(hand.position, hand.forward, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Arcade"))
            {
                GameManagementScript.instance.SelectArcade(hit.collider.gameObject.name);
                GameManagementScript.instance.SetGameID(GameManagementScript.instance.getGameID((int)hit.collider.gameObject.transform.position.x, (int)hit.collider.gameObject.transform.position.y, (int)hit.collider.gameObject.transform.position.z));
            }
            else if(hit.collider.gameObject.CompareTag("Counter"))
            {
                GameManagementScript.instance.OpenProgramMenu();
            }
        }
    }
}