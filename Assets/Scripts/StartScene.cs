using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public GameObject goBackground;
    public GameObject goVideo;
    public GameObject goChatGUI;
    public GameObject goNextScene;
    public GameObject pfAD;
    public GameObject pfGameManager;
    public GameObject pfSoundManager;
    public GameObject pfUiManager;
    public GameObject pfMusicManager;
    public AudioClip bgm;

    private void Awake()
    {
        if (MusicManager.instance == null)
        {
            Instantiate(pfMusicManager);
        }

        if (SoundManager.instance == null)
        {
            Instantiate(pfSoundManager);
        }

        if (MyAd.instance == null)
        {
            Instantiate(pfAD);
        }

        if (GameManager.instance == null)
        {
            Instantiate(pfGameManager);
        }

        if (UiManager.instance == null)
        {
            Instantiate(pfUiManager);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // show AD
        if (GameManager.instance.bAd)
        {
            MyAd.instance.ShowInterstitialAd();

            GameManager.instance.bAd = false;
        }

        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        //Debug.Log(sceneName);
        PlayerPrefs.SetString("SavedScene", sceneName);

        // showing or hiding skip button
        if (sceneName.StartsWith("End") ||
            sceneName.StartsWith("Wheel") ||
            sceneName.StartsWith("Start"))
        {
            UiManager.instance.HideSkipButton();
        }
        else
        {
            UiManager.instance.ShowSkipButton();
        }
    }

    public void SetMusic()
    {
        if (MusicManager.instance != null) 
        {
            if (bgm != null)
            {
                MusicManager.instance.music.clip = bgm;
                MusicManager.instance.SetMusic();
            }        
        }    
    }

    public void PlayVideo()
    {
        goChatGUI.SetActive(false);
        goBackground.SetActive(false);
        goVideo.SetActive(true);
        goNextScene.SetActive(true);
    }
}
