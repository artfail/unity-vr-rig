using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class PlayerMove : MonoBehaviour
{
    int speed = 5;
    public XRNode handRole = XRNode.LeftHand;
    Rigidbody _rigidbody;
    Transform camTrans;
    void Start()
    {
        camTrans = Camera.main.transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        InputDevices.GetDeviceAtXRNode(handRole).TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 direction);
        Vector3 moveDir = camTrans.forward * direction.y + camTrans.right * direction.x;
        moveDir = moveDir.normalized * speed;
        moveDir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = moveDir;
    }
}
