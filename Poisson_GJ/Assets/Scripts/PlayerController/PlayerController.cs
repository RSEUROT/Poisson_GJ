using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int numberOfCollectedFish = 0;
    public GameObject fishObject;
    public float spawnerTimer = 0f;
    public float minDistanceForSpawn = 50f;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        // iterate root objects and do something
        for (int i = 0; i < rootObjects.Count; ++i)
        {
            GameObject rootGameObject = rootObjects[i];
            Spawner spawnerComponent = rootGameObject.GetComponent<Spawner>();
            if (spawnerComponent != null)
            {
                spawnerComponent.SpawnObject(fishObject);
                spawnerComponent.SetPlayer(gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
