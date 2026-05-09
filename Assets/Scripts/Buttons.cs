using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    string savedScene;
    public GameObject goConfirmNewGame;
    public GameObject goContinueAndNew;
    public GameObject goConfirmBackToPreviousScene;
    public GameObject goConfirmGoToTheNextScene;
    public GameObject goTitle;
    public GameObject goConfirmCloseApp;
    public GameObject goOptionsMenu;
    public GameObject goSkipButton;
    public Text textVolumeSFX;
    public Text textVolumeMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // hide Confirm back to previous scene
        if (goConfirmBackToPreviousScene != null)
            goConfirmBackToPreviousScene.SetActive(false);

        // get saved scene
        savedScene = PlayerPrefs.GetString("SavedScene", "NoSavedScene");
        //Debug.Log(savedScene);

        // hide confirm new game menu
        if (goConfirmNewGame != null)
            goConfirmNewGame.SetActive(false);

        // hide confirm close app
        if (goConfirmCloseApp != null)
            goConfirmCloseApp.SetActive(false);
    }

    public void NewGameButton()
    {
        // Sound
        SoundManager.instance.ClickButton();

        // no saved scene
        if (savedScene == "NoSavedScene")
        {
            NewGame();
        }
        // saved scene
        else
        {
            // hide contine and new buttons
            //goContinueAndNew.SetActive(false);

            // show confirm menu
            goConfirmNewGame.SetActive(true);
        }
    }

    public void YesNewGame()
    {
        // Sound
        SoundManager.instance.ClickButton();

        NewGame();
    }

    public void CancelNewGame()
    {
        // Sound
        SoundManager.instance.ClickButton();

        // hide confirm new game button
        goConfirmNewGame.SetActive(false);

        // show contine and new buttons
        //goContinueAndNew.SetActive(true);
    }

    void NewGame()
    {
        // no AD
        GameManager.instance.bAd = false;

        HideStartMenu();

        // hide option menu
        goOptionsMenu.SetActive(false);

        // load scene1
        SceneManager.LoadSceneAsync("Scene1");

        // show Skip button
        goSkipButton.SetActive(true);
    }

    public void ContinueGame()
    {
        // no AD
        GameManager.instance.bAd = false;

        // hide start menu
        HideStartMenu();

        // no saved scene
        if (savedScene == "NoSavedScene")
        {
            NewGame();
        }
        // saved scene
        else
        {
            // load scene1
            SceneManager.LoadSceneAsync(savedScene);

            // show Skip button
            goSkipButton.SetActive(true);
        }

        // Play BGM
        StartCoroutine(PlayBGM());
    }

    IEnumerator PlayBGM()
    {
        MusicManager.instance.music.Stop();
        //yield return null;
        yield return new WaitForSeconds(1f);
        StartScene _startScene = FindAnyObjectByType<StartScene>();
        if (!MusicManager.instance.music.isPlaying)
        {
            _startScene.SetMusic();
        }   
    }

    public void HideStartMenu()
    {
        // hide new game menu
        goConfirmNewGame.SetActive(false);

        // hide title
        goTitle.SetActive(false);

        // hide new & continue button
        goContinueAndNew.SetActive(false);

        // hide ending credit
        UiManager.instance.goEndingCredit.SetActive(false);
    }

    public void BackToPreviousScene()
    {
        // get current scene
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;

        // if current scene is Start
        if (sceneName == "Start")
        {
            // show close app menu
            //goConfirmCloseApp.SetActive(true); 
        }
        // if current scene is not Start
        else
            // show confirm menu
            goConfirmBackToPreviousScene.SetActive(true);
    }

    public void CloseAppButton()
    {
        // show confirm menu
        goConfirmCloseApp.SetActive(true);
    }

    public void CancelCloseApp()
    {
        // hide confirm menu
        goConfirmCloseApp.SetActive(false);
    }


    public void YesCloseApp()
    {
        // exit game
        Application.Quit();
    }

    public void YesBackToPreviousScene()
    {
        // show AD
        GameManager.instance.bAd = true;

        // current scene
        Scene scene = SceneManager.GetActiveScene();
        string currentScene = scene.name;

        if (currentScene == "Scene1")
        {
            GotoStartScene();
        }
        else
        {
            // load previous scene
            NextScene _nextScene = FindAnyObjectByType<NextScene>();
            if (_nextScene != null)
            {
                string _previousScene = _nextScene.scenePrevious;
                SceneManager.LoadSceneAsync(_previousScene);
            }
        }

        // Deactive confirm menu
        goConfirmBackToPreviousScene.SetActive(false);

        // if current scene is not Scene1
        if (currentScene != "Scene1")
        {
            // hiding start menu
            HideStartMenu();
        }

        // hide options menu
        goOptionsMenu.SetActive(false);

        // go back to Start scene
        if (currentScene == "Scene1")
        {
            // Hide skip button
            goSkipButton.SetActive(false);

            // play BGM
            MusicManager.instance.music.clip = MusicManager.instance.musicIntro;
            MusicManager.instance.SetMusic();
        }
        // except start scene
        else
        {
            // play BGM
            StartCoroutine(PlayBGM());
        }
    }

    void GotoStartScene()
    {
        // go to the start scene
        SceneManager.LoadSceneAsync("Start");

        // show title
        goTitle.SetActive(true);

        // set active new & continue button
        goContinueAndNew.SetActive(true);
    }

    public void CancelBackToPreviousScene()
    {
        goConfirmBackToPreviousScene.SetActive(false);
    }

    public void OptionsButton()
    {
        SoundManager.instance.ClickButton();

        goOptionsMenu.SetActive(true);

        SetUiSfxVolume();

        SetUiMusicVolume();
    }

    public void OptionsClose()
    {
        SoundManager.instance.ClickButton();

        goOptionsMenu.SetActive(false);
    }

    public void SfxIncrease()
    {
        SoundManager.instance.VolumeIncrease();

        SetUiSfxVolume();
    }

    public void SfxDecrease()
    {
        SoundManager.instance.VolumeDecrease();

        SetUiSfxVolume();
    }

    public void SfxOnOff()
    {
        SoundManager.instance.VolumeOnOff();

        SetUiSfxVolume(); 
    }

    void SetUiSfxVolume()
    {
        // set UI
        float volume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        double _v = Math.Round(volume, 3);
        int v = (int)(_v * 100);
        if (v > 100) v = 100;
        else if (v < 0) v = 0;
        textVolumeSFX.text = v.ToString();
        //Debug.Log(volume);
        //Debug.Log(v);
    }

    void SetUiMusicVolume()
    {
        // set UI
        float volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        double _v = Math.Round(volume, 3);
        int v = (int)(_v * 100);
        if (v > 100) v = 100;
        else if (v < 0) v = 0;
        textVolumeMusic.text = v.ToString();
        //Debug.Log(volume);
        //Debug.Log(v);
    }

    public void MusicIncrease()
    {
        MusicManager.instance.VolumeIncrease();

        SetUiMusicVolume();
    }

    public void MusicDecrease() 
    {
        MusicManager.instance.VolumeDecrease();

        SetUiMusicVolume();
    }

    public void MusicOnOff() 
    {
        MusicManager.instance.VolumeOnOff();

        SetUiMusicVolume();
    }

    public void ButtonGoToTheNextScene()
    {
        SoundManager.instance.ClickButton();

        goConfirmGoToTheNextScene.SetActive(true);
    }

    public void CancelGoToTheNextScene()
    {
        SoundManager.instance.ClickButton();

        goConfirmGoToTheNextScene.SetActive(false);
    }

    // skip
    public void YesGoToTheNextScene()
    {
        // show AD
        GameManager.instance.bAd = true;
        //MyAd.instance.ShowInterstitialAd();


        NextScene _nextScene = FindAnyObjectByType<NextScene>();
        _nextScene.GoToTheNextScene();

        // hide confirm menu
        goConfirmGoToTheNextScene.SetActive(false);

        // hide title menu
        HideStartMenu();
    }
}