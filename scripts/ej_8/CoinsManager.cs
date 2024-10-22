using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager instance; // Singleton
    private int coins = 0;
    private int coinsToWin = 10;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void AddCoin(int coinsToAdd)
    {
        coins += coinsToAdd;
        Debug.Log("Total Coins: " + coins);

        if (coins >= coinsToWin)
        {
            GiveReward();
        }
    }

    public int GetCoins()
    {
        return coins;
    }

    void GiveReward()
    {
        Debug.Log("You Win!");
        RecolectCoin.instance.ShowRewardGUI("You Win!");
    }
}
