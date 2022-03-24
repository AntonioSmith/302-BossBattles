using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootRaycast : MonoBehaviour
{
    public float raycastLength = 2;
    private float distanceBetweenGroundAndIK = 0;
    private Quaternion startingRot;
    
    void Start()
    {
        startingRot = transform.rotation;
        distanceBetweenGroundAndIK = transform.localPosition.y;
    }

    void Update()
    {
        Vector3 origin = transform.position + Vector3.up * raycastLength/2;
        Vector3 direction = Vector3.down;
        float maxDistance = 1;

        // Draw ray in scene
        Debug.DrawRay(origin, direction * raycastLength, Color.blue);

        // Check for collision with ray:
        if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, maxDistance))
        {
            // Finds Ground Position
            transform.position = hitInfo.point + Vector3.up * distanceBetweenGroundAndIK;

            // Convert Starting Rotation into World-Space
            Quaternion worldNeutral = transform.parent.rotation * startingRot; 

            // Finds Ground Rotation
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal) * worldNeutral;
        }
    }
}
