using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleARCore;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class ARController : MonoBehaviour
{
    // We will fill this list with the planes that ARCore detected in the current frame
    private List<TrackedPlane> m_NewTrackedPlanes = new List<TrackedPlane>();

    public GameObject GridPrefab;


    public GameObject ARCamera;

    public GameObject Portal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check ARCore Session status
        if(Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        // The following function will fill m_NewTrackedPlanes with the planes that ARCore detected in the current frame.
        Session.GetTrackables<TrackedPlane>(m_NewTrackedPlanes, TrackableQueryFilter.New);


        //Instantiate a Grid for each TrackedPlane in m_NewTrackedPlanes
        for(int i = 0; i< m_NewTrackedPlanes.Count; ++i)
        {
            GameObject grid = Instantiate(GridPrefab, Vector3.zero, Quaternion.identity, transform);

            // This function will set the position of grid and modify the vertices of the attached mesh
            grid.GetComponent<GridVisualiser>().Initialize(m_NewTrackedPlanes[i]);

            //check if the used touches the screen
            Touch touch;
            if(Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }

            //Let's now check if the user touched any of the tracked planes
            TrackableHit hit;
            if(Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit))
            {
                // Let's now place the portal on top of the tracked plane that we touched

                //Enable 

                Portal.SetActive(true);

                Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

                Portal.transform.position = hit.Pose.position;
                Portal.transform.rotation = hit.Pose.rotation;

                Vector3 cameraPosition = ARCamera.transform.position;

                cameraPosition.y = hit.Pose.position.y;

                //Rotate the portal to face the camera

                Portal.transform.LookAt(cameraPosition, Portal.transform.up);

                Portal.transform.parent = anchor.transform;
            }
        }
    }
}
