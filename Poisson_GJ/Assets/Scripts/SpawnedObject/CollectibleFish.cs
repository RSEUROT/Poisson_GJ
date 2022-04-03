using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleFish : MonoBehaviour
{
    Spawner parentSpawner;

    public void SetParentSpawner(Spawner inParentSpawner)
    {
        parentSpawner = inParentSpawner;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playermovementComponent;
        if(collision.gameObject.TryGetComponent<PlayerMovement>(out playermovementComponent))
        {
            parentSpawner.OnObjectDestroyed();
            Destroy(gameObject);
        }

    }

}
