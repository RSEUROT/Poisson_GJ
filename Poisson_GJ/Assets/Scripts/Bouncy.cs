using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour
{

    public float bounce = 20f;
    [SerializeField] private AudioSource boingSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Bounce");

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-collision.GetContact(0).normal * bounce);
            print("Bounce : " + collision.GetContact(0).normal * bounce);
            if(collision.gameObject.GetComponent<PlayerController>() != null)
                boingSound.Play();
        }
    }
}
