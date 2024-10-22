using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    private int coinAmount = 10;
    public GameObject map;
    public float radioCheck = 5.0f;
    public float minDistanceBetweenCoins = 2.0f;
    public LayerMask layerMask;
    private Vector3 superiorLimit;
    private Vector3 inferiorLimit;
    private List<Vector3> coinPositions;
    // Start is called before the first frame update
    void Start()
    {
        coinPositions = new List<Vector3>();
        DefineMapLimits();
        GenerateCoins();
    }

    void DefineMapLimits()
    {
        BoxCollider boxCollider = map.GetComponent<BoxCollider>();
        superiorLimit = map.transform.position + boxCollider.center + new Vector3(boxCollider.size.x, boxCollider.size.y, boxCollider.size.z) / 2;
        inferiorLimit = map.transform.position + boxCollider.center - new Vector3(boxCollider.size.x, boxCollider.size.y, boxCollider.size.z) / 2;
    }

    void GenerateCoins()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            Vector3 randomPosition;
            bool validPosition = false;

            int tries = 0;
            do
            {
                randomPosition = GenerateRandomPosition();
                tries++;

                if (!Physics.CheckSphere(randomPosition, radioCheck, layerMask) && IsValidPos(randomPosition))
                {
                    validPosition = true;
                }
                if (tries > 100)
                {
                    Debug.Log("No se pudo generar la moneda");
                    break;
                }
            } while (!validPosition);

            if (validPosition)
            {
                coinPositions.Add(randomPosition);
                Instantiate(coinPrefab, randomPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(inferiorLimit.x, superiorLimit.x);
        float y = inferiorLimit.y + 3;
        float z = Random.Range(inferiorLimit.z, superiorLimit.z);

        return new Vector3(x, y, z);
    }

    bool IsValidPos(Vector3 pos)
    {
        foreach (Vector3 coinPos in coinPositions)
        {
            if (Vector3.Distance(coinPos, pos) < minDistanceBetweenCoins)
            {
                return false;
            }
        }
        return true;
    }
}
