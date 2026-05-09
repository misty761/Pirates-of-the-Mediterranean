using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public GameObject goEndingCredit;
    public GameObject goMenuOptions;
    public GameObject goSkipButton;
    public GameObject goNewAndContinue;
    public GameObject goTitle;

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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goEndingCredit.SetActive(false);
        goMenuOptions.SetActive(false);
        

        string scene = SceneManager.GetActiveScene().name;
        if (scene == "Start")
        {
            goNewAndContinue.SetActive(true);
            goSkipButton.SetActive(false);

        }
        else
        {
            goNewAndContinue.SetActive(false);
            goSkipButton.SetActive(true);
            goTitle.SetActive(false);
        }
    }

    public void HideSkipButton()
    {
        goSkipButton.SetActive(false);
    }

    public void ShowSkipButton()
    {
        goSkipButton.SetActive(true);
    }
}
