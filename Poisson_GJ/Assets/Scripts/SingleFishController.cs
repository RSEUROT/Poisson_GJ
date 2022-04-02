using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFishController : MonoBehaviour
{
    public bool StayAroundStartPosition = true;
    public float MaxNextPositionDistance = 5.0f;
    public float MinNextPositionDistance = 1.0f;
    public float DistanceThresholdForTargetDetection = 1.0f;
    public float MaxTimeBeforeChangeTarget = 10.0f;
    public bool UseAvoidObstacle = false;
    public bool ReOrientFishToMoveDirection = false;
    public float acceleration = 1.0f;
    public float moveSpeed = 5f;
    public float maxMoveSpeed = 5f;

    private Rigidbody2D FishRigidbody = null;
    private Vector3 StartPosition = Vector3.zero;
    private Vector3 NextPosition = Vector3.zero;
    private float CurrentTimeBeforeChangeTarget = 0.0f;


    void Start()
    {
        StartPosition = transform.position;
        ComputeNextPosition();
    }

    void Update()
    {
        float distanceFromArrival = Vector3.Distance(NextPosition, transform.position);
        CurrentTimeBeforeChangeTarget += Time.deltaTime;
        if (distanceFromArrival <= DistanceThresholdForTargetDetection || CurrentTimeBeforeChangeTarget >= MaxTimeBeforeChangeTarget)
        {
            ComputeNextPosition();
            CurrentTimeBeforeChangeTarget = 0.0f;
        }

        MoveToTarget();
    }

    private void ComputeNextPosition()
    {
        Vector3 direction = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0.0f);
        direction.Normalize();

        Vector3 basePos = StayAroundStartPosition ? StartPosition : transform.position;
        NextPosition = basePos + (direction * Random.Range(MinNextPositionDistance, MaxNextPositionDistance));
    }

    private void MoveToTarget()
    {
        Vector3 newDirection = NextPosition - transform.position;
        newDirection.Normalize();
        if (UseAvoidObstacle)
        {
            // TO DO => detect colliders
        }

        FishRigidbody.AddForce(acceleration * moveSpeed * newDirection, ForceMode2D.Force);

        if(FishRigidbody.velocity.magnitude > maxMoveSpeed)
        {
            Vector2 newVelocity = FishRigidbody.velocity.normalized;
            newVelocity *= maxMoveSpeed;
            FishRigidbody.velocity = newVelocity;
        }
    }
}
