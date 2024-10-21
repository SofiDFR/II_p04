using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpiderType
{
    Type1,
    Type2
}

public class CollisionNotifier : MonoBehaviour
{
    public delegate void CubeCollidedWithSpider(SpiderType spiderType);
    public static event CubeCollidedWithSpider OnCubeCollisionWithSpider;
    public SpiderType spiderType;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            OnCubeCollisionWithSpider?.Invoke(spiderType);
        }
    } 
}
