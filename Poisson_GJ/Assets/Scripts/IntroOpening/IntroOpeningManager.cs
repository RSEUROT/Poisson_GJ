using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroOpeningManager : MonoBehaviour
{
    public Image BlackPanel = null;
    public float MaxTimeBeforeLoadMenu = 5.0f;
    public float MaxTimeBeforeLaunchSound = 2.0f;
    public float MaxTimeBeforeFadeOut = 1.0f;
    public float MaxTimeBeforeFadeIn = 4.0f;
    private float FadeDuration = 1.0f;

    private float CurrentTime = 0.0f;
    private bool HasLaunchSound = false;

    private AudioSource soundSource = null;
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        if(CurrentTime >= MaxTimeBeforeFadeOut && CurrentTime < MaxTimeBeforeFadeOut + FadeDuration)
        {
            float alpha = ((MaxTimeBeforeFadeOut + FadeDuration) - CurrentTime) / FadeDuration;
            BlackPanel.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        else if (CurrentTime >= MaxTimeBeforeFadeIn && CurrentTime < MaxTimeBeforeFadeIn + FadeDuration)
        {
            float alpha = 1.0f - (((MaxTimeBeforeFadeIn + FadeDuration) - CurrentTime) / FadeDuration);
            BlackPanel.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        if (CurrentTime >= MaxTimeBeforeLaunchSound && HasLaunchSound == false)
        {
            HasLaunchSound = true;
            soundSource.Play();
        }
        if (CurrentTime >= MaxTimeBeforeLoadMenu)
        {
            BlackPanel.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            SceneManager.LoadScene("MenuScene");
        }
        Debug.Log(BlackPanel.color.a);
    }
}
