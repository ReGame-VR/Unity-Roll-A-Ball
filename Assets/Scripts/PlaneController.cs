using UnityEngine;
using Unity3DRudder;

public class PlaneController : MonoBehaviour
{
    public Vector3 SpeedTranslation = new Vector3(10, 10, 10);
    public float SpeedRotation = 50f;
    public int IndexRudder = 0;
    public bool UseCurve = false;
    public ns3DRudder.ModeAxis ModeAxis = ns3DRudder.ModeAxis.NormalizedValueNonSymmetricalPitch;

    public AnimationCurve YawCurve = new AnimationCurve(new Keyframe(-1, -1), new Keyframe(-0.25f, 0), new Keyframe(0, 0), new Keyframe(0.25f, 0), new Keyframe(1, 1));
    public AnimationCurve PitchCurve = new AnimationCurve(new Keyframe(-1, -1), new Keyframe(-0.25f, 0), new Keyframe(0, 0), new Keyframe(0.25f, 0), new Keyframe(1, 1));
    public AnimationCurve RollCurve = new AnimationCurve(new Keyframe(-1, -1), new Keyframe(-0.25f, 0), new Keyframe(0, 0), new Keyframe(0.25f, 0), new Keyframe(1, 1));
    public AnimationCurve UpDownCurve = new AnimationCurve(new Keyframe(-1, -1), new Keyframe(-0.25f, 0), new Keyframe(0, 0), new Keyframe(0.25f, 0), new Keyframe(1, 1));

    protected Vector3 translation;
    protected Quaternion rotation;

    protected Transform cube;
    protected Rudder rudder;
    protected ns3DRudder.Axis axis;
    protected ns3DRudder.CurveArray curves;

    private CurveRudder Yaw;
    private CurveRudder Pitch;
    private CurveRudder Roll;
    private CurveRudder UpDown;

    // Use this for initialization
    void Start()
    {
        cube = transform;
        translation = Vector3.zero;
        rotation = transform.rotation;

        // Mode Curve
        curves = new ns3DRudder.CurveArray();
        if (UseCurve)
        {
            ModeAxis = ns3DRudder.ModeAxis.ValueWithCurveNonSymmetricalPitch;
            // SET ANIMATION CURVE
            Roll = new CurveRudder(RollCurve);
            Pitch = new CurveRudder(PitchCurve);
            UpDown = new CurveRudder(UpDownCurve);
            Yaw = new CurveRudder(YawCurve);
            // SET NEW RUDDER CURVES 
            curves.SetCurve(ns3DRudder.CurveType.CurveXAxis, Roll);
            curves.SetCurve(ns3DRudder.CurveType.CurveYAxis, Pitch);
            curves.SetCurve(ns3DRudder.CurveType.CurveZAxis, UpDown);
            curves.SetCurve(ns3DRudder.CurveType.CurveZRotation, Yaw);
        }
    }

    void OnApplicationQuit()
    {
#if !UNITY_EDITOR
        s3DRudderManager.Instance.ShutDown();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        // Get info
        GetAxis();

        // Update position
        // Translate with the new direction in self space
        //cube.rotation = rotation;
    }

    void GetAxis()
    {
        // Get state of Controller with port number : 0
        rudder = s3DRudderManager.Instance.GetRudder(IndexRudder);
        if (UseCurve)
            axis = rudder.GetAxisWithCurve(ModeAxis, curves);
        else
            axis = rudder.GetAxis(ModeAxis);

        // Get the direction of Controller and multiply by deltatime and speed
        /*
        if (CanMove)
        {
            if (Move3D)
                translation = Vector3.Scale(rudder.GetAxis3D(axis), SpeedTranslation * Time.deltaTime);
            else
            {
                translation.x = axis.GetXAxis() * SpeedTranslation.x * Time.deltaTime;
                translation.z = axis.GetYAxis() * SpeedTranslation.z * Time.deltaTime;
            }
        }
        if (CanRotate)
            rotation *= Quaternion.AngleAxis(axis.GetZRotation() * SpeedRotation * Time.deltaTime, Vector3.up);
        */

        cube.eulerAngles = new Vector3(axis.GetPhysicalPitch() * 0.5f, axis.GetPhysicalYaw() * 0.5f, axis.GetPhysicalRoll() * -0.5f) * SpeedRotation;
    }
}
