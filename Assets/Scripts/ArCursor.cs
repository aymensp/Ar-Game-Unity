using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]  
public class ArCursor : MonoBehaviour
{

    /*private Canvas canvas;*/
    private ARPlaneManager planeManager;
    public GameObject cursorChildObject;
    public ARSessionOrigin  aR_SessionOrigin;
    public GameObject objectToPlace;
    public ARRaycastManager raycastManager;

    public bool useCursor;
    private bool isObjectPlaced = false;
    // Start is called before the first frame update
    void Start()
    {
        /*canvas = GetComponent<Canvas>();*/
        planeManager = GetComponent<ARPlaneManager>();
        cursorChildObject.SetActive(useCursor);
    }

    // Update is called once per frame
    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            
            if (useCursor)
            {
                Instantiate(objectToPlace, transform.position, transform.rotation);
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                if (hits.Count > 0 && !isObjectPlaced)
                {
                    objectToPlace.transform.localScale = new Vector3(.1f, .1f, .1f);
                    Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                    isObjectPlaced = true;
                   /* canvas.gameObject.SetActive(true);*/
                    SetAllPlanesActive(false);
                }
            }
        }
          
    }

    void UpdateCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(.5f,.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if(hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }

    void SetAllPlanesActive(bool value)
    {
        foreach (var plane in planeManager.trackables)
            plane.gameObject.SetActive(value);
    }


    

}
