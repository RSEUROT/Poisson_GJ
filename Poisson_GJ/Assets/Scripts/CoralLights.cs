using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;


public class CoralLights : MonoBehaviour
{
    private GameObject playerFish = null;
    private Transform playerFishTransform = null;
    public float maxIntensity = 7f;
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
            if (playerMovementComp != null)
            {
                playerFish = rootGameObject;
            }
        }
        playerFishTransform = playerFish.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float lightIntensity = 0;
        lightIntensity = (maxIntensity * 2f) /Vector2.Distance(gameObject.transform.position, playerFishTransform.position);
        gameObject.GetComponent<Light2D>().intensity = Mathf.Clamp(lightIntensity, 0f, maxIntensity); 
    }
}
