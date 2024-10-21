using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    public Transform referenceObj;
    public float distanceThreshold = 15f;

    public delegate void OnProximityEvent();
    public static event OnProximityEvent OnProximity;
    private bool hasTeleported = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, referenceObj.position);

        if (distance < distanceThreshold && !hasTeleported)
        {
            if (OnProximity != null)
            {
                OnProximity?.Invoke();
                hasTeleported = true;
            }
        }
        if (distance >= distanceThreshold)
        {
            ResetTeleport();
        }
    }

    public void ResetTeleport()
    {
        hasTeleported = false;
    }
}
