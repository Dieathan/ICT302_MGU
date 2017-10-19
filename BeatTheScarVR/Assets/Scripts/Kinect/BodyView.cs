using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Windows.Kinect;

public class BodyView : MonoBehaviour {
    public GameObject parentObj = null;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int state = 0;

        if(BodyManager == null)
        {
            return;
        }

        _BodyManager = BodyManager.GetComponent<BodyManager>();
        if(_BodyManager == null)
        {
            return;
        }

        Body[] data = _BodyManager.GetData();
        if(data == null)
        {
            return;
        }

        // get the amount of bodies in the list of tracked bodies, then delete unknown bodies
        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                trackedIds.Add(body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        // First delete untracked bodies
        foreach (ulong trackingId in knownIds)
        {
            if (!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        // body oject
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                if (!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }

                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
    }

    public Dictionary<ulong, GameObject> GetBodies()
    {
        return _Bodies;
    }

    public Material BoneMaterial;
    public GameObject BodyManager;

    private Dictionary<JointType, JointType> _BoneMap = new Dictionary<JointType, JointType>()
    {
        { JointType.FootLeft, JointType.AnkleLeft },
        { JointType.AnkleLeft, JointType.KneeLeft },
        { JointType.KneeLeft, JointType.HipLeft },
        { JointType.HipLeft, JointType.SpineBase },

        { JointType.FootRight, JointType.AnkleRight },
        { JointType.AnkleRight, JointType.KneeRight },
        { JointType.KneeRight, JointType.HipRight },
        { JointType.HipRight, JointType.SpineBase },

        { JointType.HandTipLeft, JointType.HandLeft }, //Need this for HandSates
        { JointType.ThumbLeft, JointType.HandLeft },
        { JointType.HandLeft, JointType.WristLeft },
        { JointType.WristLeft, JointType.ElbowLeft },
        { JointType.ElbowLeft, JointType.ShoulderLeft },
        { JointType.ShoulderLeft, JointType.SpineShoulder },

        { JointType.HandTipRight, JointType.HandRight }, //Needthis for Hand State
        { JointType.ThumbRight, JointType.HandRight },
        { JointType.HandRight, JointType.WristRight },
        { JointType.WristRight, JointType.ElbowRight },
        { JointType.ElbowRight, JointType.ShoulderRight },
        { JointType.ShoulderRight, JointType.SpineShoulder },

        { JointType.SpineBase, JointType.SpineMid },
        { JointType.SpineMid, JointType.SpineShoulder },
        { JointType.SpineShoulder, JointType.Neck },
        { JointType.Neck, JointType.Head },
    };

    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        if (parentObj != null)
            body.transform.parent = parentObj.transform;
        //body.transform.localPosition = Vector3.zero;
        body.transform.localPosition = new Vector3(.0f, .0f, 30.0f);
        body.transform.localRotation = Quaternion.identity;


        for (JointType jt = JointType.SpineBase; jt <= JointType.ThumbRight; jt++)
        {
            GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            //lr.SetVertexCount(2);
            //lr.material = BoneMaterial;
            //lr.SetWidth(0.05f, 0.05f);

            jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
        }

        return body;
    }

    private void RefreshBodyObject(Body body, GameObject bodyObject)
    {
        for (JointType jt = JointType.SpineBase; jt <= JointType.ThumbRight; jt++)
        {
            Windows.Kinect.Joint sourceJoint = body.Joints[jt];
            Windows.Kinect.Joint? targetJoint = null;

            if (_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }

            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);
            
            //LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if(targetJoint.HasValue)
            {
                //lr.SetPosition(0, jointObj.position);
                //lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                //Vector3 tempTargetJoint = GetVector3FromJoint(targetJoint.Value);
                //tempTargetJoint += parentObj.transform.position;
                //lr.SetPosition(1, tempTargetJoint);
                //lr.SetColors(GetColorForState(sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }
            else
            {
                //lr.enabled = false;
            }
        }
    }

    private static Vector3 GetVector3FromJoint(Windows.Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }

    private static Color GetColorForState(TrackingState state)
    {
        switch (state)
        {
            case TrackingState.Tracked:
                return Color.green;

            case TrackingState.Inferred:
                return Color.red;

            default:
                return Color.black;
        }
    }

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodyManager _BodyManager;
}
