using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Blowing : MonoBehaviour
{

    // Default Mic
    public string selectedDevice;
    AudioClip microphoneInput;
    bool microphoneInitialized;
    public float sensitivity;
    float growFactor = 0.2f;
    [SerializeField] bool canBlow;

    public Transform BlowIcon;
    public int score;
    public TextMeshProUGUI scoreText;

    public GameObject introductionPanel3;
    public TextMeshProUGUI blowText;
    public bool intoNotFinished;
    public string[] compliments;
    float targetScore =5;
    // Start is called before the first frame update
    void Awake()
    {
        //init microphone input
        if (Microphone.devices.Length > 0)
        {
            selectedDevice = Microphone.devices[0].ToString();
            microphoneInput = Microphone.Start(selectedDevice, true, 1, AudioSettings.outputSampleRate);
            microphoneInitialized = true;
            Debug.Log(selectedDevice);
            InvokeRepeating("Switch", 0f, 2f);
        }
    }

    void Start()
    {
        introductionPanel3.SetActive(true);
    }
    void Update()
    {
        if (canBlow)
        {
            BlowIcon.gameObject.SetActive(true);
            if (intoNotFinished)
                blowText.text = "Blaas nu rustig";
        }
        else
        {
            BlowIcon.gameObject.SetActive(false);
            if (intoNotFinished)
                blowText.text = "Adem in";
        }
        Blow();
        scoreText.text = "Score:" + score;
        if (score == 1)
        {
            blowText.text = "Goed gedaan, probeer het nu zelf";
        }

        if(score == 2 && intoNotFinished)
        {
            StartCoroutine("tutorialFinished");
        }

        if (score == targetScore)
        {
            StartCoroutine("GiveCompliment");
            targetScore += 5;
        }
        if (transform.childCount != 0)
        {
            if(this.transform.GetChild(0).localScale.x >= 2)
            {
                transform.DetachChildren();
                score++;
            }
            
        }
   
    }
    public void Switch()
    {
        canBlow = !canBlow;
    }

    public void Blow()
    {
        //get mic volume
        int dec = 128;
        float[] waveData = new float[dec];
        int micPosition = Microphone.GetPosition(selectedDevice) - (dec + 1); // null means the first microphone
        microphoneInput.GetData(waveData, micPosition);

        // Getting a peak on the last 128 samples
        float levelMax = 0;
        for (int i = 0; i < dec; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        float level = Mathf.Sqrt(Mathf.Sqrt(levelMax));

        if (level > sensitivity && canBlow && transform.childCount!=0)
        {
            Debug.Log("Nice");
            this.transform.GetChild(0).localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
        }

        if (level < sensitivity)
        {
            Debug.Log("Silence");
        }
    }

    public IEnumerator tutorialFinished()
    {
        intoNotFinished = false;
        Debug.Log("1x doen");
        blowText.text = "Wow dit gaat fantastisch, ga zo door!";
        yield return new WaitForSeconds(2f);

        introductionPanel3.SetActive(false);
    }

    public IEnumerator GiveCompliment()
    {
        introductionPanel3.SetActive(true);
        blowText.text = compliments[Random.Range(0, compliments.Length)];

        yield return new WaitForSeconds(2f);

        introductionPanel3.SetActive(false);
    }
}
