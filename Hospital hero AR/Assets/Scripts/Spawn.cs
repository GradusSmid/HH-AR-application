using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{

    public GameObject Balloon;

    public List<GameObject> BalloonChoice;
    public List<Button> animalButtons;
    private int correspondingNumber;
    public void Update()
    {
        if(this.transform.childCount == 0)
        Instantiate(Balloon, transform);
    }

    public void Selection(int balloonNumber)
    {
        correspondingNumber = balloonNumber;
        Balloon = BalloonChoice[balloonNumber];
        Destroy(transform.GetChild(0).gameObject);
        Instantiate(Balloon, transform);
    }

    public void selectButton()
    {
        animalButtons[correspondingNumber].Select();
    }
}
