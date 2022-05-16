using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TextMeshProUGUI slidervalue;
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        slidervalue.text = "sensitivity: " + sensitivitySlider.value;
    }
}
