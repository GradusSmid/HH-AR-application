using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Blowing : MonoBehaviour
{

    // Default Mic
    public string selectedDevice;
    AudioClip microphoneInput;
    bool microphoneInitialized;
    public float sensitivity;
    public Slider sensitivitySlider;
    float growFactor = 0.2f;
    [SerializeField] bool canBlow;
    public float blowOut = 6f;
    public float blowIn =2.5f;

    public Transform BlowIcon;
    public int score;
    public TextMeshProUGUI scoreText;

    public GameObject introductionPanel3;
    public TextMeshProUGUI blowText;
    public bool intoNotFinished;
    public string[] compliments;
    float targetScore =5;

    public AudioSource ademin;

    public AudioSource ademuit;

    public AudioSource probeernuzelf;

    public AudioSource wowgazodoor;

    public AudioSource[] complimentVoices;
    bool playingaudio = false;

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
        }
    }

    void Start()
    {
        introductionPanel3.SetActive(true);
        StartCoroutine("Switch", blowIn);
        ademin.Play();
    }
    void Update()
    {
        sensitivity = sensitivitySlider.value;
        if (canBlow)
        {
            BlowIcon.gameObject.SetActive(true);
            if (score == 0)
            {
                ademin.Play();
                blowText.text = "Blaas nu rustig";
            }
            
        }
        else
        {
            BlowIcon.gameObject.SetActive(false);
            if (score == 0)
            {
                ademuit.Play();
                blowText.text = "Adem in";
            }
        }
        Blow();
        scoreText.text = "Score:" + score;
        
        if (score == 1 && playingaudio == false)
        {
            blowText.text = "Goed gedaan, probeer het nu zelf";
            Debug.Log(("Play audio"));
            probeernuzelf.Play();
            playingaudio = true;
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
    public IEnumerator Switch(float blow)
    {
        yield return new WaitForSeconds(blow);
        canBlow = !canBlow;

        if (canBlow)
        {
            blow = blowOut;
        }

        if (!canBlow)
        {
            blow = blowIn;
            
        }

        StartCoroutine("Switch", blow);
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
            Debug.Log("Blowing");
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
        wowgazodoor.Play();
        yield return new WaitForSeconds(wowgazodoor.time);

        introductionPanel3.SetActive(false);
    }

    public IEnumerator GiveCompliment()
    {
        introductionPanel3.SetActive(true);
        int currentCompliment = Random.Range(0, compliments.Length);
        blowText.text = compliments[currentCompliment];
        complimentVoices[currentCompliment].Play();
        yield return new WaitForSeconds(2f);

        introductionPanel3.SetActive(false);
    }
}
