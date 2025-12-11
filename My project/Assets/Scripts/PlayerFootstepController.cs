using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepController : MonoBehaviour
{
    [Header("Refs")]
    public CharacterController controller;   // Drag your CharacterController here

    [Header("Footstep Settings")]
    public float stepInterval = 0.45f;       // Time between steps when walking
    public float sprintMultiplier = 0.6f;    // Steps faster when sprinting
    public float speedThreshold = 0.3f;      // Minimum speed considered "moving"
    public float inputThreshold = 0.1f;      // Minimum input considered "intentional movement"

    private float stepTimer;

    void Update()
    {
        if (controller == null || AudioManager.Instance == null)
            return;

        // 1) Check if player is trying to move (WASD)
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        bool hasMoveInput = Mathf.Abs(inputX) > inputThreshold || Mathf.Abs(inputZ) > inputThreshold;

        // 2) Check actual movement speed from CharacterController
        float speed = controller.velocity.magnitude;
        bool isMoving = controller.isGrounded && hasMoveInput && speed > speedThreshold;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                AudioManager.Instance.PlayFootstepPlayer();

                bool isSprinting = Input.GetKey(KeyCode.LeftShift);
                stepTimer = isSprinting ? stepInterval * sprintMultiplier : stepInterval;
            }
        }
        else
        {
            // Reset so the first step happens quickly after starting to move
            stepTimer = 0f;
        }
    }
}