using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootRaycast : MonoBehaviour
{
    public Transform footLeft;
    public Transform footRight;

    public float raycastLength = 2;

    /// <summary>
    /// Local-space position of wher the IK spawned
    /// </summary>
    private Vector3 startingPosition;

    /// <summary>
    /// Local-space rotation of where the IK spawned
    /// </summary>
    private Quaternion startingRot;

    /// <summary>
    /// World-Space rotation for the foot to be aligned with the ground
    /// </summary>
    private Quaternion groundRotation;

    /// <summary>
    /// World-Space position of the ground above/below the foot IK.
    /// </summary>
    private Vector3 groundPosition;

    /// <summary>
    /// The local-space position to ease towards. Allows us to animation the position
    /// </summary>
    private Vector3 targetPosition;

    void Start()
    {
        startingRot = transform.rotation;
        startingPosition = transform.localPosition;
    }

    void Update()
    {
        //FindGround();

        transform.localPosition = AnimMath.Ease(transform.localPosition, targetPosition, .05f); // Ease towards target
    }

    public void SetLocalPosition(Vector3 p)
    {
        targetPosition = p;
    }

    public void SetPositionHome()
    {
        targetPosition = startingPosition;
    }

    public void SetPositionOffset(Vector3 p)
    {
        targetPosition = startingPosition + p;
    }

    private void FindGround()
    {
        Vector3 origin = transform.position + Vector3.up * raycastLength / 2;
        Vector3 direction = Vector3.down;
        float maxDistance = 2;

        // Draw ray in scene
        Debug.DrawRay(origin, direction * maxDistance, Color.blue);

        // Check for collision with ray:
        if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, maxDistance))
        {
            // Finds Ground Position
            groundPosition = hitInfo.point + Vector3.up * startingPosition.y;

            // Convert Starting Rotation into World-Space
            Quaternion worldNeutral = transform.parent.rotation * startingRot;

            // Finds Ground Rotation
            groundRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal) * worldNeutral;
        }
    }
}
