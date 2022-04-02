using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float turnSpeed = 3.5f;

    public Rigidbody2D body;

    Vector2 MovementInput;

    float acceleration;
    float rotationAngle;

    // Update is called once per frame
    void Update()
    {
        MovementInput.x = Input.GetAxisRaw("Horizontal");
        MovementInput.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        acceleration = MovementInput.y;
        body.AddForce(acceleration * moveSpeed * transform.up, ForceMode2D.Force);
        rotationAngle -= MovementInput.x * turnSpeed;
        body.MoveRotation(rotationAngle);
    }
}
