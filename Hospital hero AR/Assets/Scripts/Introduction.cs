using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    public UnityEvent onTouch;
    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            onTouch.Invoke();
        }

        if(Input.GetMouseButton(0))
        {
            onTouch.Invoke();
        }
    }
}
