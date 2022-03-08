using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Grab : MonoBehaviour
{
    public LayerMask grabbableLayer;
    public Transform holdPoint;

    private float launchForce = 10;
    private float raycastDist = 50;

    private Transform hoverObject = null;
    private Transform heldObject = null;
    private Rigidbody heldRigidbody = null;

    public XRNode handRole = XRNode.LeftHand;
    bool triggerState = false;

    void Update()
    {
        InputDevices.GetDeviceAtXRNode(handRole).TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger);

        if (trigger && !triggerState) // on trigger down
        {
            if (heldObject == null)
            {
                CheckForPickUp();
            }
            else
            {
                LaunchObject();
            }
        }

        /*
        if (!hTrigger && triggerState) // on trigger up
        {

        }
        */
        triggerState = trigger;
    }

    void CheckForPickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDist, grabbableLayer))
        {
            StartCoroutine(PickUpObject(hit.transform));
        }
    }

    IEnumerator PickUpObject(Transform transform)
    {
        heldObject = transform;
        heldObject.gameObject.layer = 7; // ignore player layer - keeps held objects from hitting the players collider
        heldRigidbody = heldObject.GetComponent<Rigidbody>();
        heldRigidbody.isKinematic = true;

        float t = 0;
        while (t < .4f) //snap to position when close
        {
            heldRigidbody.position = Vector3.Lerp(heldRigidbody.position, holdPoint.position, t);
            t += Time.deltaTime;
            yield return null;
        }
        SnapToHand();
    }

    void SnapToHand()
    {
        heldObject.position = holdPoint.position;
        heldObject.parent = transform;
    }

    void LaunchObject()
    {
        StopAllCoroutines();
        SnapToHand();

        heldRigidbody.isKinematic = false;
        heldRigidbody.AddForce(transform.forward * launchForce, ForceMode.VelocityChange);
        heldObject.parent = null;
        StartCoroutine(LetGo());
    }

    IEnumerator LetGo()
    {
        yield return new WaitForSeconds(.1f);
        heldObject.gameObject.layer = 6; //grabbableLayer
        heldObject = null;
    }


    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDist, grabbableLayer))
        {
            if (hoverObject != hit.transform)
            {
                if (hoverObject != null)
                {
                    hoverObject.GetComponent<Renderer>().material.color = Color.white;
                }
                hoverObject = hit.transform;
                hoverObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        else
        {
            if (hoverObject != null)
            {
                hoverObject.GetComponent<Renderer>().material.color = Color.white;
                hoverObject = null;
            }
        }
    }
}