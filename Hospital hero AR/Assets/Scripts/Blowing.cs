using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Blowing : MonoBehaviour
{
    public static float blowStrength = 80;
    float growFactor = 0.2f;
    private enum BlowStates {blowingOut, blowingIn, blowingBreak};
    [SerializeField]BlowStates currentBlowStates;
    public float blowOut = 6f;
    public float blowIn =2.5f;
    public float blowBreak = 2f;
    private float currentBlow = 2.5f;
    public bool playing;
    public Transform BlowIcon;
    private int score;
    public TextMeshProUGUI scoreText;
    public GameObject introductionPanel3;
    public TextMeshProUGUI blowText;
    public bool intoNotFinished;
    public string[] compliments;
    float targetScore =5;
    public AudioSource ademin;
    public AudioSource ademinhouden;
    public AudioSource ademuit;
    public AudioSource probeernuzelf;
    public AudioSource wowgazodoor;
    public AudioSource[] complimentVoices;
    bool playingaudio = false;


    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        if(currentBlowStates == BlowStates.blowingOut)
        {
            Blow();
        }
        
        ScoreEvents();
        Tutorial();

        if (transform.childCount != 0)
        {
            if (this.transform.GetChild(0).localScale.x >= 2)
            {
                transform.DetachChildren();
                score++;
            }
        }
    }
    void ScoreEvents()
    {
        scoreText.text = "Score:" + score;
        if (score == targetScore)
        {
            StartCoroutine("GiveCompliment");
            targetScore += 5;
        }
    }

    IEnumerator Lerp(Vector3 startValue, Vector3 endValue, float lerpDuration)
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            BlowIcon.localScale = Vector3.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        BlowIcon.localScale = endValue;
    }
    public void StartSwitch()
    {
        StartCoroutine("Switch", currentBlow);
        playing = true;
    }
    public void QuitPlaying()
    {
        playing = false;
    }
    public IEnumerator Switch(float blow)
    {
        
        if (currentBlowStates == BlowStates.blowingOut)
        {
            currentBlow = blow;
            blow = blowOut;
            StartCoroutine(Lerp(BlowIcon.localScale, new Vector3(1, 1, 1), blowOut));
        }

        if(currentBlowStates == BlowStates.blowingBreak)
        {
            currentBlow = blow;
            blow = blowBreak;
        }

        if (currentBlowStates == BlowStates.blowingIn)
        {
            currentBlow = blow;
            blow = blowIn;
            StartCoroutine(Lerp(BlowIcon.localScale, new Vector3(2, 2, 2), blowIn));
        }
        yield return new WaitForSeconds(blow);
        currentBlowStates += 1;
        if (currentBlowStates == BlowStates.blowingBreak + 1)
        {
            currentBlowStates = 0;
        }

        StartCoroutine("Switch", blow);
    }
    public void Blow()
    {
        float db = MicInput.MicLoudnessinDecibels;
        float volume = MicInput.MicLoudness;
        if(playing == true && transform.childCount != 0)
        {
            if (db < 1 && db > -blowStrength)
            {
                Debug.Log("Blazen");
                this.transform.GetChild(0).localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
            }
        }
    }
    void Tutorial()
    {
        if (score == 0 && playing)
        {
            if (currentBlowStates == BlowStates.blowingOut)
            {
                ademin.Play();
                introductionPanel3.SetActive(true);
                blowText.text = "Blaas nu rustig";
            }
            if (currentBlowStates == BlowStates.blowingIn)
            {
                ademinhouden.Play();
                introductionPanel3.SetActive(true);
                blowText.text = "Adem in";
            }
            if (currentBlowStates == BlowStates.blowingBreak)
            {
                ademuit.Play();
                introductionPanel3.SetActive(true);
                blowText.text = "houd je adem in";
            }
        }

        if (score == 1 && playingaudio == false)
        {
            blowText.text = "Goed gedaan, probeer het nu zelf";
            Debug.Log(("Play audio"));
            probeernuzelf.Play();
            playingaudio = true;
        }

        if (score == 2 && intoNotFinished)
        {
            StartCoroutine("tutorialFinished");
        }
    }
    public IEnumerator tutorialFinished()
    {
        intoNotFinished = false;
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
