using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoThroughText : MonoBehaviour
{
    public string[] textLines;
    public AudioSource[] voiceLines;
    public int currentTextLine = 0;
    public TextMeshProUGUI text;
    public Button beginnenButton;
    public Button nextTextButton;
    public Button previousTextButton;
    // Start is called before the first frame update
    void Start()
    {
        Split();
        text.text = textLines[currentTextLine];
        voiceLines[currentTextLine].Play();
    }

    // Update is called once per frame
    public void goToNextTextLine()
    {
        currentTextLine++;
        text.text = textLines[currentTextLine];
        previousTextButton.gameObject.SetActive(true);
        voiceLines[currentTextLine - 1].Stop();
        voiceLines[currentTextLine].Play();
        if (currentTextLine == textLines.Length - 1)
        {
            beginnenButton.gameObject.SetActive(true);
            nextTextButton.gameObject.SetActive(false);
        }
    }

    void Split()
    {
        //The text is devided by the /
        string s = text.text;
        textLines = s.Split('/');
    }

    public void GoToPreviousTextLine()
    {
        currentTextLine--;
        text.text = textLines[currentTextLine];
        nextTextButton.gameObject.SetActive(true);
        beginnenButton.gameObject.SetActive(false);
        voiceLines[currentTextLine + 1].Stop();
        voiceLines[currentTextLine].Play();
        if (currentTextLine == 0)
        {
            previousTextButton.gameObject.SetActive(false);
        }
    }
}
