using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class AutoSpawnAR : MonoBehaviour
{

    public GameObject[] placedObject;

    private ARPlaneManager _arPlaneManager;
    // Update is called once per frame
    void Awake()
    {

        _arPlaneManager = GetComponent<ARPlaneManager>();
        _arPlaneManager.planesChanged += PlaneChanged;
    }

    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null)
        {
            ARPlane arPlane = args.added[0];

                int selection = Random.Range(0, placedObject.Length);
                Instantiate(placedObject[selection], arPlane.transform.position, Quaternion.identity);

        }
    }
}
