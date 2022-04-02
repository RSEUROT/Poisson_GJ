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

    protected bool GetObstacleAvoidanceSafeDirection(ref Vector2 outDirection)
    {
        LayerMask mask = LayerMask.GetMask("Default");
        RaycastHit2D forwardHit = Physics2D.Raycast(transform.position, outDirection, RaycastDistance, mask);
        if (forwardHit.transform == null)
        {
            Vector3 endpos = transform.position + (new Vector3(outDirection.x, outDirection.y, 0.0f) * RaycastDistance);
            Debug.DrawLine(transform.position, endpos, Color.green, 0.01f);
            //Debug.Log("forwardHit.transform == null");
            return false;
        }
        else
        {
            Vector3 endpos = transform.position + (new Vector3(outDirection.x, outDirection.y, 0.0f) * RaycastDistance);
            Debug.DrawLine(transform.position, endpos, Color.red, 0.01f);
        }

        bool hasFound = false;
        int angleCount = 1;
        const int maxAngleCount = 36;
        while(hasFound == false && angleCount < maxAngleCount)
        {
            Vector2 positiveDirection = new Vector2(outDirection.x, outDirection.y);
            Vector2 negativeDirection = new Vector2(outDirection.x, outDirection.y);
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

            if(positiveAngleHit.transform == null)
            {
                Vector3 endpos = transform.position + (new Vector3(positiveDirection.x, positiveDirection.y, 0.0f) * RaycastDistance);
                Debug.DrawLine(transform.position, endpos, Color.green, 0.01f);
                //Debug.Log("Change direction from: " + outDirection + " to: " + positiveDirection);
                outDirection = positiveDirection;
                hasFound = true;
                break;
            }
            else
            {
                Vector3 endpos = transform.position + (new Vector3(positiveDirection.x, positiveDirection.y, 0.0f) * RaycastDistance);
                Debug.DrawLine(transform.position, endpos, Color.red, 0.01f);
            }
            if (negativeAngleHit.transform == null)
            {
                Vector3 endpos = transform.position + (new Vector3(negativeDirection.x, negativeDirection.y, 0.0f) * RaycastDistance);
                //Debug.Log("Change direction from: " + outDirection + " to: " + negativeDirection);
                Debug.DrawLine(transform.position, endpos, Color.green, 0.01f);
                outDirection = negativeDirection;
                hasFound = true;
                break;
            }
            else
            {
                Vector3 endpos = transform.position + (new Vector3(negativeDirection.x, negativeDirection.y, 0.0f) * RaycastDistance);
                Debug.DrawLine(transform.position, endpos, Color.red, 0.01f);
            }

            ++angleCount;
        }

        if(!hasFound)
            Debug.Log("hasFound == false");
        return true;
    }
}
