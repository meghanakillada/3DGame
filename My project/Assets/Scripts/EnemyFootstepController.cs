using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFootstepController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float stepInterval = 0.7f;

    private float stepTimer;

    void Update()
    {
        if (agent == null || AudioManager.Instance == null)
            return;

        bool moving = agent.velocity.magnitude > 0.2f;

        if (moving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                AudioManager.Instance.PlayFootstepEnemy();
                stepTimer = stepInterval;
            }
        }
    }
}