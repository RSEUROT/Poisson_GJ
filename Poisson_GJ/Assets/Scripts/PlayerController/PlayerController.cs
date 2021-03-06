using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int numberOfCollectedFish = 0;
    public List<GameObject> fishObject;
    public float spawnerTimer = 0f;
    public float minDistanceForSpawn = 50f;
    private Vector3 lastPosition;
    private float speed;
    public GameObject followFishObject;
    GameObject LastFish;

    [SerializeField] public AudioSource PafSound;
    [SerializeField] private AudioSource OuchSound;

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
        float dist = Vector3.Distance(lastPosition, gameObject.transform.position);
        speed = dist / Time.deltaTime;
        lastPosition = gameObject.transform.position;
    }
    public void AddFishes()
    {
        GameObject instantiatedObject = Instantiate(followFishObject, gameObject.transform.position, Quaternion.identity);
        instantiatedObject.GetComponent<FollowingFishes>().SetGameObjectToFollow(LastFish != null ? LastFish : gameObject);
        LastFish = instantiatedObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PafSound.Play();
        if (speed > 1f && Random.Range(0,10) == 3)
        {
            OuchSound.Play();
        }
    }
}
