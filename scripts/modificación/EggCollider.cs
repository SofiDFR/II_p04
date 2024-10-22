using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollider : MonoBehaviour
{
    // Modificación
    public delegate void CubeCollidedWithEgg(GameObject cube);
    public static event CubeCollidedWithEgg OnCubeCollisionWithEgg;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            OnCubeCollisionWithEgg?.Invoke(this.gameObject);
        }
    } 
}