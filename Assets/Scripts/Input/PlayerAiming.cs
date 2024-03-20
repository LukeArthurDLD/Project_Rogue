using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public Camera cam;

    public LayerMask groundMask;
    public float turnSpeed = 450;

    public Transform body;
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Aim();
    }
    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition); // ray from camera
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
            return (success: true, position: hitInfo.point); // return true and mouse position as a vector3
        else
            return (success: false, position: Vector3.zero); // return false
    }
    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if(success)
        {
            // calculate direction
            var direction = position - transform.position;

            //ingore height
            direction.y = 0;

            // look in direction
            body.forward = direction;
        }
    }
}
