using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    Scene scene;
    string sceneName;
    public GameObject pfUiManager;
    public string sceneNext;
    public string scenePrevious;

    private void Start()
    {
        if (UiManager.instance == null)
        {
            GameObject goUiManager = Instantiate(pfUiManager);
            Buttons buttons = goUiManager.GetComponentInChildren<Buttons>();
            buttons.HideStartMenu();
        }
    }

    public void GoToTheNextScene()
    {
        // no AD
        GameManager.instance.bAd = false;

        SceneManager.LoadSceneAsync(sceneNext);

    }

    public void ShowEndingCredit() 
    {
        UiManager.instance.goEndingCredit.SetActive(true);
    }

}
