using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionMovement : MonoBehaviour
{
    enum Mode
    {
        Idle,
        Walk
    }

    public float speed = 2;

    private CharacterController pawn;

    private Mode mode = Mode.Idle;

    private Vector3 input;

    private Camera cam;

    void Start()
    {
        pawn = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = Vector3.Cross(Vector3.up, camForward);

        input = camForward * v + camRight * h;
        if (input.sqrMagnitude > 1) input.Normalize();

        // Set movement mode based on movement input
        float threshold = .1f;
        mode = (input.sqrMagnitude > threshold * threshold) ? Mode.Walk : Mode.Idle; // if moving set mode to walk, otherwise set mode to idle

        pawn.SimpleMove(input * speed);
    }
}
