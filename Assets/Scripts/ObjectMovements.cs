using UnityEngine;
using System.Collections;
using System.Threading;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

/*
public class ObjectMovements : MonoBehaviour {


    public GameObject myo = null;
    ThalmicMyo thalmicMyo = null;
    public float speed = 1;
    public float timeWaiting = 5000000.0f;
    private Quaternion _antiYaw = Quaternion.identity;

    // A reference angle representing how the armband is rotated about the wearer's arm, i.e. roll.
    // Set by making the fingers spread pose or pressing "r".
    private float _referenceRoll = 0.0f;

    private Pose _lastPose = Pose.Unknown;


    private bool t1Paused = false;
    private bool t2Paused = false;
    private Thread t1;
    private Thread t2;
    // Use this for initialization
    void Start () {
        thalmicMyo = myo.GetComponent<ThalmicMyo>();
        t1 = new Thread(_process1);
        t2 = new Thread(_process2);
	}

    // Update is called once per frame
    void Update() {
        if (thalmicMyo.pose == Pose.Fist || thalmicMyo.pose == Pose.FingersSpread) {
            if (!t1.IsAlive)
            {
                t2.Start();
            } else {
                t1Paused = !t1Paused;
            }
        } else {
            if (!t2.IsAlive) {
                t2.Start();
            } else{
                t2Paused = !t2Paused;
            }
        }

    }
    // Compute the angle of rotation clockwise about the forward axis relative to the provided zero roll direction.
    // As the armband is rotated about the forward axis this value will change, regardless of which way the
    // forward vector of the Myo is pointing. The returned value will be between -180 and 180 degrees.
    float rollFromZero (Vector3 zeroRoll, Vector3 forward, Vector3 up) {

        float cosine = Vector3.Dot(up, zeroRoll);


        Vector3 cp = Vector3.Cross(up, zeroRoll);
        float directionCosine = Vector3.Dot(forward, cp);
        float sign = directionCosine < 0.0f ? 1.0f : -1.0f;

        // Return the angle of roll (in degrees) from the cosine and the sign.
        return sign * Mathf.Rad2Deg * Mathf.Acos(cosine);
    }
    
    // Compute a vector that points perpendicular to the forward direction,
    // minimizing angular distance from world up (positive Y axis).
    // This represents the direction of no rotation about its forward axis.
    Vector3 computeZeroRollVector(Vector3 forward) {
        Vector3 antigravity = Vector3.up;
        Vector3 m = Vector3.Cross(myo.transform.forward, antigravity);
        Vector3 roll = Vector3.Cross(m, myo.transform.forward);

        return roll.normalized;
    }

    // Adjust the provided angle to be within a -180 to 180.
    float normalizeAngle (float angle) {
        if (angle > 180.0f) {
            return angle - 360.0f;
        }
        if (angle < -180.0f) {
            return angle + 360.0f;
        }

        return angle;
    }

    private void _process1()
    {
        if (thalmicMyo.pose == Pose.Fist) {
            transform.RotateAround(Vector3.zero, Vector3.up, speed * Time.deltaTime);
        }
        if (thalmicMyo.pose == Pose.FingersSpread) {
            transform.RotateAround(Vector3.zero, Vector3.down, speed * Time.deltaTime);
        }
        for (int i = 0; i < timeWaiting; i++) {
            while (t1Paused) { }
        }

    }


    private void _process2()
    {
        Vector3 zeroRoll = computeZeroRollVector(myo.transform.forward);
        float roll = rollFromZero(zeroRoll, myo.transform.forward, myo.transform.up);


        float relativeRoll = normalizeAngle(roll - _referenceRoll);


        Quaternion antiRoll = Quaternion.AngleAxis(relativeRoll, myo.transform.forward);


        transform.rotation = _antiYaw * antiRoll * Quaternion.LookRotation(myo.transform.forward);


        if (thalmicMyo.xDirection == Thalmic.Myo.XDirection.TowardWrist)
        {

            transform.rotation = new Quaternion(transform.localRotation.x,
                                                -transform.localRotation.y,
                                                transform.localRotation.z,
                                                -transform.localRotation.w);
           
        }

        for (int i = 0; i < timeWaiting; i++)
            while (t2Paused) { }
    }
}

//If above doesn't work, delete everything under import statements and copy this block:
*/

    public class ObjectMovements : MonoBehaviour {

        public GameObject myo = null;
   
        public float speed = 1;

        private Quaternion _antiYaw = Quaternion.identity;

        // A reference angle representing how the armband is rotated about the wearer's arm, i.e. roll.
        // Set by making the fingers spread pose or pressing "r".
        private float _referenceRoll = 0.0f;

        private Pose _lastPose = Pose.Unknown;

        // Use this for initialization
        void Start () {}

        // Update is called once per frame
        void Update()
        {
            ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
        
            Vector3 zeroRoll = computeZeroRollVector(myo.transform.forward);
            float roll = rollFromZero(zeroRoll, myo.transform.forward, myo.transform.up);


            float relativeRoll = normalizeAngle(roll - _referenceRoll);


            Quaternion antiRoll = Quaternion.AngleAxis(relativeRoll, myo.transform.forward);


            transform.rotation = _antiYaw * antiRoll * Quaternion.LookRotation(myo.transform.forward);


            if (thalmicMyo.xDirection == Thalmic.Myo.XDirection.TowardWrist)
            {

                transform.rotation = new Quaternion(transform.localRotation.x,
                                                    -transform.localRotation.y,
                                                    transform.localRotation.z,
                                                    -transform.localRotation.w);       
            }
        }

        // Compute the angle of rotation clockwise about the forward axis relative to the provided zero roll direction.
        // As the armband is rotated about the forward axis this value will change, regardless of which way the
        // forward vector of the Myo is pointing. The returned value will be between -180 and 180 degrees.
        float rollFromZero (Vector3 zeroRoll, Vector3 forward, Vector3 up) {

            float cosine = Vector3.Dot(up, zeroRoll);


            Vector3 cp = Vector3.Cross(up, zeroRoll);
            float directionCosine = Vector3.Dot(forward, cp);
            float sign = directionCosine < 0.0f ? 1.0f : -1.0f;

            // Return the angle of roll (in degrees) from the cosine and the sign.
            return sign * Mathf.Rad2Deg * Mathf.Acos(cosine);
        }
    
        // Compute a vector that points perpendicular to the forward direction,
        // minimizing angular distance from world up (positive Y axis).
        // This represents the direction of no rotation about its forward axis.
        Vector3 computeZeroRollVector(Vector3 forward) {
                Vector3 antigravity = Vector3.up;
                Vector3 m = Vector3.Cross(myo.transform.forward, antigravity);
                Vector3 roll = Vector3.Cross(m, myo.transform.forward);

                return roll.normalized;
        }

        // Adjust the provided angle to be within a -180 to 180.
        float normalizeAngle (float angle) {
                if (angle > 180.0f)
                {
                    return angle - 360.0f;
                }
                if (angle < -180.0f)
                {
                    return angle + 360.0f;
                }
                return angle;
        }
    }

  
