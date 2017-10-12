using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPointingLaser : MonoBehaviour
{
    public Transform hand;
    public float rayWidthMultiplier = 0.02f;
    public float maxRayDistance = 500.0f;

    void Awake()
    {
        if(lineRenderer == null)
        {
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                lineRenderer.receiveShadows = false;
                lineRenderer.widthMultiplier = rayWidthMultiplier;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray laserPointer = new Ray(hand.position, hand.forward);
        if(lineRenderer != null)
        {
            lineRenderer.SetPosition(0, laserPointer.origin);
            lineRenderer.SetPosition(1, laserPointer.origin + laserPointer.direction * maxRayDistance);
        }
    }

    private LineRenderer lineRenderer = null;
}