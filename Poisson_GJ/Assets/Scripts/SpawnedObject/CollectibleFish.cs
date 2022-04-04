using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CollectibleFish : MonoBehaviour
{
    Spawner parentSpawner;
    GameLoopManager gameLoop;
    GameObject fishPrefab;
    bool isEndScreen = false;

    [SerializeField] public AudioSource[] yaySounds;

    public void SetParentSpawner(Spawner inParentSpawner, GameObject inInFishPrefab)
    {
        parentSpawner = inParentSpawner;
        fishPrefab = inInFishPrefab;
    }

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
            GameLoopManager gameLoopManagerComponent = rootGameObject.GetComponent<GameLoopManager>();
            if (gameLoopManagerComponent != null)
            {
                gameLoop = gameLoopManagerComponent;
            }
        }
        gameObject.GetComponent<Light2D>().enabled = isEndScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLight(bool light)
    {
        isEndScreen = light;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerControllerComponent;
        if(collision.gameObject.TryGetComponent<PlayerController>(out playerControllerComponent))
        {
            if(gameLoop != null)
                gameLoop.AddFish(fishPrefab);
            parentSpawner.OnObjectDestroyed();
            playerControllerComponent.AddFishes();
            yaySounds[Random.Range(0, yaySounds.Length)].Play();
            Destroy(gameObject);
        }

    }

}
