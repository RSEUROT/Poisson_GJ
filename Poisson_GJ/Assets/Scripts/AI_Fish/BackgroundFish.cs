using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFish : MonoBehaviour
{
    public bool positiveXDirection = true;
    public float verticalIntensity = 1.0f;
    public float verticalTimerDelayer = 1.0f;
    public float speed = 1.0f;
    public float maxPositionXBeforeRespawn = 60.0f;

    private float startPosY = 0.0f;
    private float Timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        startPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (positiveXDirection && transform.position.x >= maxPositionXBeforeRespawn)
        {
            transform.position = new Vector3(-maxPositionXBeforeRespawn, startPosY, 0.0f);
            Timer = 0.0f;
        }
        else if (positiveXDirection == false && transform.position.x <= -maxPositionXBeforeRespawn)
        {
            transform.position = new Vector3(maxPositionXBeforeRespawn, startPosY, 0.0f);
            Timer = 0.0f;
        }

        Timer += Time.deltaTime * verticalTimerDelayer;
        float posX = transform.position.x + (speed * Time.deltaTime * (positiveXDirection ? 1.0f : -1.0f));
        float posY = startPosY + Mathf.Sin(Timer) * verticalIntensity;
        transform.position = new Vector3(posX, posY, 0.0f);
    }
}
