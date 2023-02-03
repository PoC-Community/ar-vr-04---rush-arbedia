using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class script : MonoBehaviour
{
    public GameObject Myobject;
    public GameObject spawnObject;
    public ARRaycastManager Raycastmanager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
{
    List<ARRaycastHit> touches = new List<ARRaycastHit>();
    Raycastmanager.Raycast(Input.GetTouch(0).position, touches, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

    if(touches.Count > 0)
    {
        Vector3 spawnPosition = touches[0].pose.position + Camera.main.transform.forward * 2;
        GameObject newObject = GameObject.Instantiate(Myobject, spawnPosition, touches[0].pose.rotation);
        StartCoroutine(ShrinkAndDestroy(newObject));
    }
}

    }

    IEnumerator ShrinkAndDestroy(GameObject shrinkingObject)
    {
        float scale = 1.0f;
        while (scale > 0)
        {
            scale -= 0.1f;
            shrinkingObject.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        GameObject.Destroy(shrinkingObject);
        GameObject newSphere = GameObject.Instantiate(spawnObject, shrinkingObject.transform.position, Quaternion.identity);
    }
}
