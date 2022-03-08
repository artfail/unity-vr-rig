using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class VRInit : MonoBehaviour
{
    private List<InputDevice> inputDevices = new List<InputDevice>();
    private List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        InputDevices.GetDevices(inputDevices);
        if (inputDevices[0].manufacturer == "Oculus")
        {
            Unity.XR.Oculus.Performance.TrySetDisplayRefreshRate(90);
            //Unity.XR.Oculus.Performance.TrySetDisplayRefreshRate(120);
        }
#endif
    }

    IEnumerator Start()
    {
        yield return null;
        SubsystemManager.GetInstances(subsystems);
        foreach (var subsystem in subsystems)
        {
            subsystem.TrySetTrackingOriginMode(TrackingOriginModeFlags.Floor);
        }
    }
}
