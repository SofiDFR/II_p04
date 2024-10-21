using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTowards : MonoBehaviour
{

    private Transform target;
    public float speed = 5f;
    private Rigidbody rb;
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = null;
    }

    void FixedUpdate()
    {
        if (target != null && rb != null)
        {
            Vector3 direction = target.position - transform.position;
            rb.MovePosition(transform.position + direction.normalized * speed * Time.fixedDeltaTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public Transform GetTarget()
    {
        return target;
    }
}
