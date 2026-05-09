using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]
    public bool isPaused;
    [HideInInspector]
    public bool bAd;
    

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
        isPaused = false;
        bAd = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        isPaused = true;
    }
}
