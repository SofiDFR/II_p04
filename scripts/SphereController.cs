using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public Transform target;
    private GoTowards goTowards;

    void Start()
    {
        goTowards = GetComponent<GoTowards>();
    }
    void OnEnable()
    {
        CyllinderCollider.OnCubeCollisionWithSphere += GoToTarget;
    }

    void OnDisable()
    {
        CyllinderCollider.OnCubeCollisionWithSphere -= GoToTarget;
    }

    void GoToTarget()
    {
        if (goTowards != null && target != null)
        {
            goTowards.SetTarget(target);
        }
    }
}
