using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject Balloon;

    public List<GameObject> BalloonChoice;
    public void Update()
    {
        if(this.transform.childCount == 0)
        Instantiate(Balloon, transform);
    }

    public void Selection(int balloonNumber)
    {
        Balloon = BalloonChoice[balloonNumber];
        Destroy(transform.GetChild(0).gameObject);
        Instantiate(Balloon, transform);
    }
}
