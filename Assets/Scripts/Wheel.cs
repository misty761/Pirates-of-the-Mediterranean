using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    public GameObject ui;
    bool isTurning;
    bool bResult;
    public RectTransform rectTransformWheel;
    public GameObject goResult;
    public float rotationSpeed = 600f;
    public float rotationSpeedDecrease = 1f;
    public float rotationSpeedStop = 100f;
    float rotationSpeedInitial;
    int rotationAngle = 0;
    [HideInInspector]
    public int result;
    public float timeRotationSpeedMax = 1f;
    float timeRotation;
    public int[] probabilityResults;
    public string[] stringResults;
    public Text[] textProbabilities;
    public Text[] textResults;
    int choice;
    string nameScene;
    public GameObject goRerollAndContinue;
    public GameObject pfAD;
    public GameObject goConfirmReroll;

    private void Awake()
    {
        ui.SetActive(false);
        //print("hide wheel");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // AD
        if (MyAd.instance == null)
        {
            Instantiate(pfAD);
        }

        isTurning = false;
        goResult.SetActive(false);
        bResult = false;
        timeRotation = 0f;
        rotationSpeedInitial = rotationSpeed;
        // numbers of results
        int count = textProbabilities.Length;
        // Settting conditions of choices
        if (count == 3)
        {
            textProbabilities[0].text = "[>=" + (100 - probabilityResults[0]) + "]";
            textProbabilities[1].text = "[<" + (100 - probabilityResults[0]) + "]";
            textProbabilities[2].text = "[<" + (100 - probabilityResults[0] - probabilityResults[1]) + "]"; 
        }
        // Setting of the texts of choices
        for (int i = 0; i < count; i++)
        {
            textResults[i].text = stringResults[i];
        }

        choice = 0; // before rolling
        // Getting a name of the scene
        nameScene = SceneManager.GetActiveScene().name;
        // Hide re-roll and contine buttons
        goRerollAndContinue.SetActive(false);
        // Hide confirm reroll
        goConfirmReroll.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        // turning
        if (isTurning)
        {
            // calculate turning time
            timeRotation += Time.deltaTime;
            //print(timeRotation);
            // rotation speed --
            if (timeRotation > timeRotationSpeedMax )
            {
                rotationSpeed = rotationSpeed - rotationSpeedDecrease;
                if (rotationSpeed < rotationSpeedStop)
                {
                    rotationSpeed = 0;
                    GetResult();
                }
            }

            rotationAngle = (int)(rotationAngle + Time.deltaTime * rotationSpeed);
            if (rotationAngle > 360)
            {
                rotationAngle = 0;
            }
            //print(rotationAngle);
            rectTransformWheel.eulerAngles = new Vector3(0, 0, -rotationAngle);
        }
    }

    public void ShowWheel()
    {
        ui.SetActive(true);
        //print("show wheel");
    }

    public void ClickWheel()
    {
        // 결과가 없을 때
        if (!bResult)
        {
            // play sound
            SoundManager.instance.PlaySound(SoundManager.instance.audioClick);

            // load AD
            MyAd.instance.LoadInterstitialAd();

            // 휠이 멈춰 있다면
            if (!isTurning)
            {
                isTurning = true;
                rotationSpeed = rotationSpeedInitial;
            }
            // 휠이 돌고 있다면
            else
            {
                GetResult();
            }
        }
    }

    void GetResult()
    {
        // play click sound
        SoundManager.instance.ClickButton();
        // Stop turning
        isTurning = false;
        bResult = true;
        // result
        result = Random.Range(1, 101); // Valid in Unity
        //print(result);
        Text text = goResult.GetComponent<Text>();
        text.text = "" + result;
        goResult.SetActive(true);
        // 결과가 3개면
        if (textResults.Length == 3)
        {
            // 해당 결과 text 노란색으로
            if (result >= (100 - probabilityResults[0]))
            {
                textProbabilities[0].color = Color.yellow;
                textResults[0].color = Color.yellow;
                choice = 1;
            }
            else if (result > probabilityResults[2])
            {
                textProbabilities[1].color = Color.yellow;
                textResults[1].color = Color.yellow;
                choice = 2;
            }
            else
            {
                textProbabilities[2].color = Color.yellow;
                textResults[2].color = Color.yellow;
                choice = 3;
            }
        }
        // show re-roll and contine buttons
        goRerollAndContinue.SetActive(true);
    }

    public void ContinueChoice()
    {
        //print("choice : " + choice);

        // load AD
        MyAd.instance.LoadInterstitialAd();

        if (nameScene == "Wheel1")
        {
            if (choice == 1)
            {
                SceneManager.LoadSceneAsync("Choice1-1-1");
            }
            else if (choice == 2)
            {
                SceneManager.LoadSceneAsync("Choice1-2-1");
            }
            else if (choice == 3)
            {
                SceneManager.LoadSceneAsync("Choice1-3-1");
            }
        }
        else if (nameScene == "Wheel2")
        {
            if (choice == 1)
            {
                SceneManager.LoadSceneAsync("Choice2-1-1");
            }
            else if (choice == 2)
            {
                SceneManager.LoadSceneAsync("Choice2-2-1");
            }
            else if (choice == 3)
            {
                SceneManager.LoadSceneAsync("Choice2-3-1");
            }
        }
        else if (nameScene == "Wheel3")
        {
            if (choice == 1)
            {
                SceneManager.LoadSceneAsync("Scene15");
            }
            else if (choice == 2)
            {
                SceneManager.LoadSceneAsync("Choice3-2-1");
            }
            else if (choice == 3)
            {
                SceneManager.LoadSceneAsync("Choice3-3-1");
            }
        }
        else if (nameScene == "Wheel4")
        {
            if (choice == 1)
            {
                SceneManager.LoadSceneAsync("Choice4-1-1");
            }
            else if (choice == 2)
            {
                SceneManager.LoadSceneAsync("Choice4-2-1");
            }
            else if (choice == 3)
            {
                SceneManager.LoadSceneAsync("Choice4-3-1");
            }
        }
    }

    void Reroll()
    {
        // hide re-roll and contine buttons
        goRerollAndContinue.SetActive(false);
        // show AD
        MyAd.instance.ShowInterstitialAd();
        // no result
        bResult = false;
        // hide result
        goResult.SetActive(false);
        // numbers of results
        int count = textProbabilities.Length;
        // Setting texts with white
        for (int i = 0; i < count; i++)
        {
            textProbabilities[i].color = Color.white;
            textResults[i].color = Color.white;
        }
        // turning wheel
        //ClickWheel();
    }

    public void RerollButton()
    {
        // play click sound
        SoundManager.instance.ClickButton();
        // load AD
        //MyAd.instance.LoadInterstitialAd();
        // show confirm window
        goConfirmReroll.SetActive(true);
    }

    public void RerollCancel()
    {
        // play click sound
        SoundManager.instance.ClickButton();
        // hide confirm window
        goConfirmReroll.SetActive(false);
    }

    public void RerollYes()
    {
        // play click sound
        SoundManager.instance.ClickButton();
        // hide confirm window
        goConfirmReroll.SetActive(false);
        Reroll();
    }

}
