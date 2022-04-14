using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class SpawnableManager : MonoBehaviour
    {
        [SerializeField] private ARRaycastManager _raycastManager;
        [SerializeField] private GameObject _spawnablePrefab;

        private List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();
        private GameObject _spawnedObject;
        ARPlaneManager m_ARPlaneManager;
        private bool isCreated = false;
        void Awake()
        {
            m_ARPlaneManager = GetComponent<ARPlaneManager>();
        }

        
        private void Start()
        {
            _spawnedObject = null;
        }

        private void Update()
        {
            // No touch events
            if (Input.touchCount == 0)
            {
                return;
            }

            // Save the found touch event
            Touch touch = Input.GetTouch(0);

            if (_raycastManager.Raycast(touch.position, _raycastHits))
            {
                // Beginning of the touch, this triggers when the finger first touches the screen
                if (touch.phase == TouchPhase.Began)
                {
                    bool isTouchOverUI = touch.position.IsPointOverUIObject();

                    if (!isTouchOverUI)
                    {
                        // Spawn a GameObject
                        SpawnPrefab(_raycastHits[0].pose.position);
                        SetAllPlanesActive(false);
                    }
                }
            }
        }


        // Instantiate a GameObject to the location where finger was touching the screen
        private void SpawnPrefab(Vector3 spawnPosition)
        {
            if (!isCreated)
            {
                _spawnedObject = Instantiate(_spawnablePrefab, spawnPosition, Quaternion.identity);
                isCreated = true;
            }
        }

        void SetAllPlanesActive(bool value)
        {
            foreach (var plane in m_ARPlaneManager.trackables)
                plane.gameObject.SetActive(value);
        }
    }
}
