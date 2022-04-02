using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour
{

    public float bounce = 20f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-collision.GetContact(0).normal * bounce);
            print("Bounce : " + collision.GetContact(0).normal * bounce);
        }
    }
}
