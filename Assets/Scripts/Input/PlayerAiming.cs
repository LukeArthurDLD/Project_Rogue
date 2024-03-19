using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public Camera cam;

    public float turnSpeed = 450;
    Quaternion targetRotation;

    public Transform body;
    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        ControlMouse();
    }
    void ControlMouse()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cam.transform.position.y - body.transform.position.y));
        targetRotation = Quaternion.LookRotation(mousePosition - new Vector3(body.transform.position.x, 0, body.transform.position.z));
        body.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(body.transform.eulerAngles.y, targetRotation.eulerAngles.y, turnSpeed * Time.deltaTime);

    }
}
