using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderType2Controller : MonoBehaviour
{
    public Transform lookAtObj;
    public float rotationSpeed = 7f;
    private LookAt lookAt;

    void OnEnable()
    {
        ProximityDetector.OnProximity += LookAtObj;
    }

    void OnDisable()
    {
        ProximityDetector.OnProximity -= LookAtObj;
    }

    void Start()
    {
        lookAt = GetComponent<LookAt>();
    }

void LookAtObj()
{
    Debug.Log("OnProximity event triggered!");
    if (lookAt != null && lookAtObj != null)
    {
        lookAt.SetTarget(lookAtObj);
    }
}

}