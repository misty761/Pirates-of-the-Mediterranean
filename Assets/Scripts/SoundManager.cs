using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip audioAlert;
    public AudioClip audioClick;
    public AudioClip audioCoin;
    public AudioClip audioDenied;
    public AudioClip audioDoor;
    public AudioClip audioFanfare;
    public AudioClip audioGameOver;
    public AudioClip audioJump;
    public AudioClip audioLifeUp;
    public AudioClip audioNegative;
    public AudioClip audioPistol;
    public AudioClip audioPunch;
    public AudioClip audioScore;
    public AudioClip audioSpawnItem;
    public AudioClip audioThud;
    public float volume;
    public float volumeStep = 0.1f;
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
            //Debug.Log("destroy : " + gameObject.name);
        }
    }

    private void Start()
    {
        //PlayerPrefs.SetFloat("SoundVolume", 0.5f);
        volume = PlayerPrefs.GetFloat("SoundVolume", 0.1f);
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (volume > 0f)
        {
            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, volume);
        }
        //Debug.Log("sound volume : " + volume);
    }

    public void PlaySound(AudioClip audioClip, float v)
    {
        if (volume > 0f)
        {
            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, v * volume);
        }
        //Debug.Log("sound volume : " + volume);
    }

    public void PlaySound(AudioClip audioClip, Vector3 pos, float v)
    {
        if (volume > 0f)
        {
            AudioSource.PlayClipAtPoint(audioClip, pos, v * volume);
        }
        //Debug.Log("sound volume : " + volume);
    }

    public void ClickButton()
    {
        PlaySound(audioClick, 1f * volume);
        //Debug.Log("sound volume : " + volume);
    }

    public void ChatSound()
    {
        ClickButton();
    }

    public void VolumeOnOff()
    {
        if (volume > 0f)
        {
            volume = 0f;
            goMute.SetActive(true);
        }
        else
        {
            volume = 0.1f;
            goMute.SetActive(false);
        }

        PlayerPrefs.SetFloat("SoundVolume", volume);

        PlaySound(audioClick);
    }

    public void VolumeIncrease()
    {
        PlaySound(audioClick);

        volume += volumeStep;

        if (volume > 1f) volume = 1f;

        goMute.SetActive(false) ;

        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    public void VolumeDecrease()
    {
        PlaySound(audioClick);

        volume -= volumeStep;

        if (volume <= 0f)
        {
            volume = 0f;
            goMute.SetActive(true);
        } 

        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    public void SoundPunch()
    {
        PlaySound(audioPunch, 1f * volume);
    }

    public void SoundDoor()
    {
        PlaySound(audioDoor, 1f * volume);
    }

    public void SoundPistol()
    {
        PlaySound(audioPistol, 1f * volume);
    }
}
