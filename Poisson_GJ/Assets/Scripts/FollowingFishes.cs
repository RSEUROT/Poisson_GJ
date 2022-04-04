using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingFishes : MonoBehaviour
{
    GameObject gameObjectToFollow;
    public float timeToCatchUp = 40f;
    public float distance = 25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetGameObjectToFollow(GameObject inGameObjectToFollow)
    {
        gameObjectToFollow = inGameObjectToFollow;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObjectToFollow != null)
        {
            if (Vector3.Distance(gameObject.transform.position, gameObjectToFollow.transform.position) >= distance)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObjectToFollow.transform.position, Time.deltaTime / timeToCatchUp);
                print("bob");
            }
        }
    }
}
