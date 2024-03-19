using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;

    public float height = 10f;
    public float distance = 20f;
    public float angle = 45f;
    public float smoothSpeed = 0.5f;

    private Vector3 refVelocity;

    void Start()
    {
        HandleCamera();
    }

    
    void Update()
    {
        HandleCamera();
    }

    protected virtual void HandleCamera()
    {
        if(!target)
        {
            return;
        }

        Vector3 worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);
        UnityEngine.Debug.DrawLine(target.position, worldPosition, Color.red);

        Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;
        UnityEngine.Debug.DrawLine(target.position, rotatedVector, Color.green);

        Vector3 flatTargetPosition = target.position;
        flatTargetPosition.y = 0f;

        Vector3 finalPosition = flatTargetPosition + rotatedVector;
        UnityEngine.Debug.DrawLine(target.position, finalPosition, Color.blue);

        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, smoothSpeed);
        transform.LookAt(flatTargetPosition + targetOffset);
    }

}
