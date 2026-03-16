using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.UIElements;

public class GameEnding : MonoBehaviour
{

    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public UIDocument uiDocument;   

    public AudioSource winAudio;
    public AudioSource loseAudio;
    bool hasAudioPlayed = false;


    bool isPlayerAtExit;
    bool isPlayerCaught;
    float timer;

    private VisualElement endScreen;
    private VisualElement caughtScreen;

    void Start()
    {
        endScreen = uiDocument.rootVisualElement.Q<VisualElement>("EndScreen");
        caughtScreen = uiDocument.rootVisualElement.Q<VisualElement>("CaughtScreen");
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }


    private void Update()
    {

        if(isPlayerAtExit)
        {
            EndLevel(endScreen, winAudio, false);
        }
        else if(isPlayerCaught)
        {
            EndLevel(caughtScreen, loseAudio, true);
        }
    }

    void EndLevel(VisualElement screen, AudioSource audioSource, bool doRestart)
    {

        if(!hasAudioPlayed)
        {
            audioSource.Play();
            Debug.Log("Playing audio: " + audioSource.clip.name);
            hasAudioPlayed = true;
        }

        timer += Time.deltaTime;
        screen.style.opacity = timer / fadeDuration;

        if(timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
                Time.timeScale = 0f;
            }
        }
    }
}
