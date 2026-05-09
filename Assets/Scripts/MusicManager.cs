using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    float volume = 0.5f;
    float volumeStep = 0.1f;
    public AudioClip musicIntro;
    [HideInInspector]
    public AudioSource music;
    public GameObject goMute;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        music = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume = PlayerPrefs.GetFloat("MusicVolume", 0.1f);

        SetVolume();
    }

    void SetVolume()
    {
        if (volume <= 0f)
        {
            music.Pause();
            goMute.SetActive(true);
        }
        else
        {
            music.volume = volume;
            music.UnPause();
            if (!music.isPlaying)
            {
                music.Play();
            }
            if (goMute != null)
            {
                goMute.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeOnOff()
    {
        if (volume > 0f)
        {
            volume = 0f;
        }
        else
        {
            volume = 0.1f;
        }

        PlayerPrefs.SetFloat("MusicVolume", volume);

        SetVolume();
    }

    public void VolumeIncrease()
    {
        volume += volumeStep;

        if (volume > 1f) volume = 1f;

        PlayerPrefs.SetFloat("MusicVolume", volume);

        SetVolume();
    }

    public void VolumeDecrease()
    {
        volume -= volumeStep;

        if (volume < 0f) volume = 0f;

        PlayerPrefs.SetFloat("MusicVolume", volume);

        SetVolume();
    }

    public void SetMusic()
    {
        //StartScene startScene = FindAnyObjectByType<StartScene>();
        //music.clip = startScene.bgm;

        SetVolume();

        //if (volume > 0f) music.Play();
    }
}
