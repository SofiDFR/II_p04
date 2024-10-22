using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public int points;
    public GameObject eggPrefab;
    public GameObject chest;

    void OnEnable()
    {
        EggCollider.OnCubeCollisionWithEgg += GoToChest;
    }

    void OnDisable()
    {
        EggCollider.OnCubeCollisionWithEgg -= GoToChest;
    }


    void GoToChest(GameObject egg)
    {
        if (egg != null && egg.name == this.name) return;
        if (chest != null)
        {
            transform.position = chest.transform.position;
        }
    }

    public void Collect()
    {
        PointsManager.instance.AddPoints(points);
        Destroy(gameObject);
        SpawnNewEgg();
    }

    void SpawnNewEgg()
    {
        GameObject plane = GameObject.Find("Plane");
        Vector3 planeSize = plane.GetComponent<Renderer>().bounds.size;
        Vector3 planePosition = plane.transform.position;

        Camera mainCamera = Camera.main;

        Vector3 spawnPosition = Vector3.zero;
        bool isPositionValid = false;
        
        while (!isPositionValid)
        {
            spawnPosition = new Vector3(Random.Range(planePosition.x - planeSize.x / 2, planePosition.x + planeSize.x / 2),
                                        Random.Range(planePosition.y - planeSize.y / 2, planePosition.y + planeSize.y / 2),
                                        Random.Range(planePosition.z - planeSize.z / 2, planePosition.z + planeSize.z / 2));
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(spawnPosition);
            if (screenPoint.x >= 0 && screenPoint.x <= Screen.width && screenPoint.y >= 0 && screenPoint.y <= Screen.height)
            {
                isPositionValid = true;
            }
        }
        Instantiate(eggPrefab, spawnPosition, Quaternion.identity);
    }
}