using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandInput : MonoBehaviour
{
    public TextMesh outText;
    public XRNode handRole = XRNode.LeftHand;

    void Update()
    {

        InputDevices.GetDeviceAtXRNode(handRole).TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger);
        InputDevices.GetDeviceAtXRNode(handRole).TryGetFeatureValue(CommonUsages.gripButton, out bool grip);
        InputDevices.GetDeviceAtXRNode(handRole).TryGetFeatureValue(CommonUsages.primaryButton, out bool pButton);
        InputDevices.GetDeviceAtXRNode(handRole).TryGetFeatureValue(CommonUsages.secondaryButton, out bool sButton);
        InputDevices.GetDeviceAtXRNode(handRole).TryGetFeatureValue(CommonUsages.menuButton, out bool mButton);

        if (trigger)
        {
            outText.text = "trigger";
        }
        else if (grip)
        {
            outText.text = "grip";
        }
        else if (pButton)
        {
            outText.text = "primary Button";
        }
        else if (sButton)
        {
            outText.text = "secondary Button";
        }
        else if (mButton)
        {
            outText.text = "menu Button";
        }
        else
            outText.text = "none";

    }
}
