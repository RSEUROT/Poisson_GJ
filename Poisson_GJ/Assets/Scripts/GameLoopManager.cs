using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.U2D;
using UnityEngine.Rendering.Universal;
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

    // black screen
    [SerializeField]
    private GameObject BlackScreen = null;
    [SerializeField]
    private float FadeMaxTime = 3.0f;
    private float FadeTime = 0.0f;

    [SerializeField]
    private GameObject CanvasUiEndGame = null;
    [SerializeField]
    private GameObject CanvasUiEndGame2 = null;
    [SerializeField]
    private TextMeshProUGUI ScoreText = null;

    [SerializeField]
    List<GameObject> LightsToSwitchOff = new List<GameObject>();
    List<float> LightBaseIntensity = new List<float>();
    [SerializeField]
    GameObject LightToSwitchOn = null;
    GameObject[] rocks;

    // FISH
    private List<GameObject> CatchedFishPrefabs = new List<GameObject>();


    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        for (int i = 0; i < LightsToSwitchOff.Count; ++i)
        {
            LightBaseIntensity.Add(LightsToSwitchOff[i].GetComponent<Light2D>().intensity);
        }


        rocks = GameObject.FindGameObjectsWithTag("Rock");

        if (rocks.Length == 0)
        {
            Debug.Log("No game objects are tagged with 'Enemy'");
        }
    }

    void Update()
    {
        if (currentTime < (float)MaxTime)
        {
            currentTime += Time.deltaTime;
            for (int i = 0; i < LightsToSwitchOff.Count; ++i)
            {
                LightsToSwitchOff[i].GetComponent<Light2D>().intensity = LightBaseIntensity[i] - ((currentTime / (float)MaxTime) * LightBaseIntensity[i]);
            }
        }
        if (currentTime >= (float)MaxTime)
        {
            for(int i = 0; i < LightsToSwitchOff.Count; ++i)
            {
                LightsToSwitchOff[i].SetActive(false);
            }

            if (FadeTime < FadeMaxTime)
            {
                FadeTime += Time.deltaTime;
                if (FadeTime < FadeMaxTime)
                {

                    BlackScreen.SetActive(true);
                    BlackScreen.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, FadeTime / FadeMaxTime);
                }
                else
                {
                    for (int i = 0; i < LightsToSwitchOff.Count; ++i)
                    {
                        LightsToSwitchOff[i].SetActive(false);
                    }

                    for (int i = 0; i < rocks.Length; ++i)
                    {
                        rocks[i].SetActive(false);
                    }

                    BlackScreen.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                    Camera.main.GetComponent<CameraController>().enabled = false;
                    Camera.main.transform.position = new Vector3(0.0f, 0.0f, Camera.main.transform.position.z);
                    SpawnAllFishes();
                    LightToSwitchOn.SetActive(true);
                    CanvasUiEndGame.SetActive(true);
                    CanvasUiEndGame2.SetActive(true);
                    ScoreText.text = CatchedFishPrefabs.Count.ToString();
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ReturnToMainMenu();
            }
        }
    }

    public float GetLightIntensityValue()
    {
        if (currentTime >= (float)MaxTime)
            return 0.0f;

        float curveValue = LightByTimeCurve.Evaluate(currentTime / (float)MaxTime);
        return curveValue;
    }
    public void AddFish(GameObject fishPrefab)
    {
        CatchedFishPrefabs.Add(fishPrefab);
    }
    public int GetFishCount()
    {
        return CatchedFishPrefabs.Count;
    }

    private void SpawnAllFishes()
    {
        for(int i = 0; i < CatchedFishPrefabs.Count; ++i)
        {
            GameObject fish = Instantiate<GameObject>(CatchedFishPrefabs[i], new Vector3(Random.Range(-20, 20), Random.Range(-20, 20) > 0.0f ? 20.0f : -20.0f, 0.0f), Quaternion.identity);
            fish.GetComponent<SpriteRenderer>().sortingOrder = 101;
            fish.GetComponent<CollectibleFish>().SetLight(true);
            fish.GetComponent<SingleFishController>().SetIsEndScreen(true);
            //fish.AddComponent<EndFishBehaviour>();
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
