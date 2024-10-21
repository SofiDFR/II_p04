using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController1 : MonoBehaviour
{
    public Transform target;
    private GoTowards goTowards;

    void Start()
    {
        goTowards = GetComponent<GoTowards>();
    }
    void OnEnable()
    {
        CyllinderCollider.OnCubeCollisionWithSphere += GoToSphere;
    }

    void OnDisable()
    {
        CyllinderCollider.OnCubeCollisionWithSphere -= GoToSphere;
    }

    void GoToSphere()
    {
        if (goTowards != null && target != null)
        {
            goTowards.SetTarget(target);
        }
    }
}
