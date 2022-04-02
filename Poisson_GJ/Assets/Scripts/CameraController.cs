using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private GameObject playerFish = null;
    private Transform playerFishTransform = null;


    void Start()
    {
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);
        
        // iterate root objects and do something
        for (int i = 0; i < rootObjects.Count; ++i)
        {
            GameObject rootGameObject = rootObjects[i];
            PlayerMovement playerMovementComp = rootGameObject.GetComponent<PlayerMovement>();
            if(playerMovementComp != null)
            {
                playerFish = rootGameObject;
            }
        }
        playerFishTransform = playerFish.transform;
    }

    void Update()
    {
        transform.position = new Vector3(playerFishTransform.position.x, playerFishTransform.position.y, transform.position.z);
    }
}
