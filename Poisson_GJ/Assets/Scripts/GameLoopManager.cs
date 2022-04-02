using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : MonoBehaviour
{
    private static GameLoopManager instance = null;
    public static GameLoopManager Instance
    {
        get
        {
            return instance;
        }
    }

    // TIMER
    [SerializeField]
    private int MaxTime = 60;
    [SerializeField]
    private AnimationCurve LightByTimeCurve = new AnimationCurve();

    private float currentTime = 0.0f;

    // FISH
    private int FishCount = 0;


    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= (float)MaxTime)
        {
            // launch end
        }
    }

    public float GetLightIntensityValue()
    {
        if (currentTime >= (float)MaxTime)
            return 0.0f;

        float curveValue = LightByTimeCurve.Evaluate(currentTime / (float)MaxTime);
        return curveValue;
    }
    public void AddFish()
    {
        FishCount++;
    }
    public int GetFishCount()
    {
        return FishCount;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
