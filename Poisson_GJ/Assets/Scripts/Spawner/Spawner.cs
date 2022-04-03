using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float timer = 0f;
    List<GameObject> objectToSpawn;
    GameObject player;
    bool hasObject = false;
    public void SpawnObject(List<GameObject> inGameObject)
    {
        objectToSpawn = inGameObject;
        int rand = Random.Range((int)0, (int)objectToSpawn.Count - 1);
        GameObject instantiatedObject = Instantiate(inGameObject[rand], gameObject.transform);
        instantiatedObject.GetComponent<CollectibleFish>().SetParentSpawner(this);
        hasObject = true;
    }

    public void SetPlayer(GameObject inPlayer)
    {
        player = inPlayer;
    }

    public void OnObjectDestroyed()
    {
        timer = player.GetComponent<PlayerController>().spawnerTimer; ;
        hasObject = false;
        player.GetComponent<PlayerController>().numberOfCollectedFish++;
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject.GetComponent<SpriteRenderer>());
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 0f;
            if (!hasObject && Vector3.Distance(gameObject.transform.position, player.transform.position) > player.GetComponent<PlayerController>().minDistanceForSpawn)
            {
                SpawnObject(objectToSpawn);
            }
        }
    }
}
