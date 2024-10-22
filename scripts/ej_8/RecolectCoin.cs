using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecolectCoin : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI winText;
    public static RecolectCoin instance;

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
    }

    void OnTriggerEnter(Collider other)
    {
        Coin coin = other.GetComponent<Coin>();
        if (coin != null)
        {
            coin.Collect();
            UpdatePointsUI();
        }
    }

    void UpdatePointsUI()
    {
        coinsText.text = "Coins Collected: " + CoinsManager.instance.GetCoins().ToString() + " / 10";
    }

    public void ShowRewardGUI(string reward)
    {
        winText.text = reward;
        winText.gameObject.SetActive(true);
        Invoke("HideRewardGUI", 3f);
    }

    void HideRewardGUI()
    {
        winText.gameObject.SetActive(false);
    }
}
