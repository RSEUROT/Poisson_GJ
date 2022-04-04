using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFishes : MonoBehaviour
{
    GameObject LastFish;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddFishes(GameObject inGameObject)
    {
        GameObject instantiatedObject = Instantiate(inGameObject, gameObject.transform);
        if(LastFish != null)
            instantiatedObject.GetComponent<FollowingFishes>().SetGameObjectToFollow(LastFish);
        LastFish = instantiatedObject;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
