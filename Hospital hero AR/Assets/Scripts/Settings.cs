using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
    public void ValueChanged(Slider slider)
    {
        Blowing.blowStrength = slider.value;
    }
}
