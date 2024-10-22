using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public delegate void CoinCollected();
    public static event CoinCollected OnCoinCollected;

    public void Collect()
    {
        CoinsManager.instance.AddCoin(1);

        OnCoinCollected?.Invoke();

        Destroy(gameObject);
    }
}
