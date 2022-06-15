using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenForAudioCommand : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float db = MicInput.MicLoudnessinDecibels;
        float volume = MicInput.MicLoudness;
        if (db < 1 && db > -20)
        {
            Debug.Log("Blazen");
            this.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime *1f;
        }

        Debug.Log("Volume is " + MicInput.MicLoudness.ToString("##.#####") + ", decibels is :" + MicInput.MicLoudnessinDecibels.ToString("######"));
    }


    float NormalizedLinearValue(float v)
    {
        float f = Mathf.InverseLerp(.000001f, .001f, v);
        return f;
    }

    float NormalizedDecibelValue(float v)
    {
        float f = Mathf.InverseLerp(-114.0f, 6f, v);
        return f;
    }
}
