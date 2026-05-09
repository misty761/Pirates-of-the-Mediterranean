using UnityEngine;
using UnityEngine.SceneManagement;

public class Choices : MonoBehaviour
{
    public GameObject goChoices;

    Scene scene;
    string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goChoices.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowChoices()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        // pause the game
        GameManager.instance.isPaused = true;

        // show choice
        goChoices.SetActive(true);
    }

    public void Choice1()
    {
        // Q : Just step into my hat.
        // A : Step into the hat
        if (sceneName == "Scene6")
        {
            // Ending1(Dreamlike ending)
            SceneManager.LoadSceneAsync("End1-1");

            // continue
            GameManager.instance.isPaused = false;
        }
        // Q : Join to bear's project
        // A : accepting bear's proposal
        else if (sceneName == "Scene8")
        {
            // Ending2
            SceneManager.LoadSceneAsync("End2-1");

            // continue
            GameManager.instance.isPaused = false;
        }
        // Q : Fox asks to be a influencer
        // A : No
        else if (sceneName == "Scene10")
        {
            // Ending2
            SceneManager.LoadSceneAsync("End3-1");

            // continue
            GameManager.instance.isPaused = false;
        }

        // hide choices menu
        goChoices.SetActive(false);
    }

    public void Choice2()
    {
        // Q : Just step into my hat.
        // A : Politely decline and roll on
        if (sceneName == "Scene6")
        {
            // load Scene7
            SceneManager.LoadSceneAsync("Scene7");

            // continue
            GameManager.instance.isPaused = false;
        }
        // Q : Join to bear's project
        // A : denying bear's proposal
        else if (sceneName == "Scene8")
        {
            // Load Scene9
            SceneManager.LoadSceneAsync("Scene9");

            // continue
            GameManager.instance.isPaused = false;
        }
        // Q : Fox asks to be a influencer
        // A : Go back home of grand parents
        else if (sceneName == "Scene10")
        {
            // Ending2
            SceneManager.LoadSceneAsync("End4-1");

            // continue
            GameManager.instance.isPaused = false;
        }

        // hide choices menu
        goChoices.SetActive(false);
    }

    public void Choice3()
    {
        // Q : Fox asks to be a influencer
        // A : Yes
        if (sceneName == "Scene10")
        {
            // Ending2
            SceneManager.LoadSceneAsync("End5-1");

            // continue
            GameManager.instance.isPaused = false;
        }

        // hide choices menu
        goChoices.SetActive(false);
    }
}
