using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderType1Controller : MonoBehaviour
{
    public Transform selectedObject;
    public Transform teleportObj;
    private GoTowards goTowards;
    private Renderer spiderRenderer;
    private Rigidbody rb;
    
    void OnEnable()
    {
        CollisionNotifier.OnCubeCollisionWithSpider += HandleCubeCollisionWithSpider;
        ProximityDetector.OnProximity += TeleportToObj;
    }

    void OnDisable()
    {
        CollisionNotifier.OnCubeCollisionWithSpider -= HandleCubeCollisionWithSpider;
        ProximityDetector.OnProximity -= TeleportToObj;
    }

    void Start()
    {
        goTowards = GetComponent<GoTowards>();

        spiderRenderer = GetComponentInChildren<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    void HandleCubeCollisionWithSpider(SpiderType spiderType)
    {
        if (spiderType == SpiderType.Type1)
        {
            MoveTowardsNearestType2Egg();
        }
        else if (spiderType == SpiderType.Type2)
        {
            MoveTowardsSelectedObject();
        }
    }

    void MoveTowardsSelectedObject()
    {
        if (goTowards != null && selectedObject != null)
        {
            goTowards.SetTarget(selectedObject);
        }
    }

    void MoveTowardsNearestType2Egg()
    {
        Transform nearestEgg = FindNearestEggOfType2();
        if (goTowards != null && nearestEgg != null)
        {
            goTowards.SetTarget(nearestEgg);
        }
    }

    Transform FindNearestEggOfType2()
    {
        GameObject[] eggs = GameObject.FindGameObjectsWithTag("EggType2");
        Transform nearestEgg = null;
        float minDistance = float.MaxValue;
        foreach (GameObject egg in eggs)
        {
            float distance = Vector3.Distance(transform.position, egg.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEgg = egg.transform;
            }
        }
        return nearestEgg;
    }

    void OnCollisionEnter(Collision collision)
    {
        AnimationsController animationsController = GetComponent<AnimationsController>();
        if (HasReachedDestination())
        {
            goTowards.SetTarget(null);
            if (animationsController != null)
            {
                animationsController.SetMovingState(false);
            }
        }
        
        if (collision.gameObject.tag == "EggType2" && spiderRenderer != null)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
            spiderRenderer.material.color = randomColor;

            // Force reapply by disabling and enabling the Renderer (forces a refresh)
            spiderRenderer.enabled = false;
            spiderRenderer.enabled = true;
        }
    }



    bool HasReachedDestination()
    {
        if (goTowards != null)
        {
            if (goTowards.GetTarget() != null)
            {
                float distance = Vector3.Distance(transform.position, goTowards.GetTarget().position);
                return distance <= 3f;
            }
        }
        return false;
    }

    void TeleportToObj()
    {
        if (teleportObj != null)
        {
            rb.MovePosition(teleportObj.position);
        }
    }
}
