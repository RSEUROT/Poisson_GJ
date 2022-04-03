using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFishController : MonoBehaviour
{
    public bool UseObstacleAvoidance = false;

    private float RaycastDistance = 2.0f;
    private float RaycastAngleOffset = 5.0f;

    void Start()
    {
        StartInternal();
    }

    protected virtual void StartInternal()
    {
    }

    void Update()
    {
        UpdateInternal();
    }
    protected virtual void UpdateInternal()
    {
    }

    protected Vector2 GetObstacleAvoidanceSafeDirection(Vector2 baseDirection, Vector2 basePosition)
    {
        Vector2 safeDirection = Vector2.zero;

        LayerMask mask = LayerMask.GetMask("Default");
        RaycastHit2D forwardHit = Physics2D.Raycast(transform.position, baseDirection, RaycastDistance, mask);
        if(forwardHit.transform != null)
        {
            Vector2 dir = basePosition - new Vector2(forwardHit.transform.position.x, forwardHit.transform.position.y);
            dir.Normalize();
            safeDirection += dir * 3.0f;
        }

        int angleCount = 1;
        const int maxAngleCount = 18;
        while(angleCount < maxAngleCount)
        {
            Vector2 positiveDirection = new Vector2(baseDirection.x, baseDirection.y);
            Vector2 negativeDirection = new Vector2(baseDirection.x, baseDirection.y);
            float angleDeg = angleCount * RaycastAngleOffset;
            float angle = angleDeg * Mathf.Deg2Rad;
            Vector2 addingDirection = Vector2.zero;
            addingDirection.x = Mathf.Cos(angle);
            addingDirection.y =  Mathf.Sin(angle);

            positiveDirection += addingDirection;
            negativeDirection -= addingDirection;
            positiveDirection.Normalize();
            negativeDirection.Normalize();

            RaycastHit2D positiveAngleHit = Physics2D.Raycast(transform.position, positiveDirection, RaycastDistance, mask);
            RaycastHit2D negativeAngleHit = Physics2D.Raycast(transform.position, negativeDirection, RaycastDistance, mask);

            if (positiveAngleHit.transform != null)
            {
                Vector2 dir = basePosition - new Vector2(positiveAngleHit.transform.position.x, positiveAngleHit.transform.position.y);
                dir.Normalize();
                safeDirection += dir;
            }
            if (negativeAngleHit.transform != null)
            {
                Vector2 dir = basePosition - new Vector2(negativeAngleHit.transform.position.x, negativeAngleHit.transform.position.y);
                dir.Normalize();
                safeDirection += dir;
            }

            ++angleCount;
        }

        safeDirection.Normalize();

        return safeDirection;
    }
}
