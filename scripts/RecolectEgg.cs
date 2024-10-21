using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecolectEgg : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI rewardText;
    public static RecolectEgg instance;

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
        Egg egg = other.GetComponent<Egg>();
        if (egg != null)
        {
            if (other.CompareTag("EggType1"))
            {
                Debug.Log("Recolected Egg Type 1 + " + egg.points);
            }
            else if (other.CompareTag("EggType2"))
            {
                Debug.Log("Recolected Egg Type 2 + " + egg.points);
            }
            egg.Collect();
            UpdatePointsUI();
        }
    }

    void UpdatePointsUI()
    {
        pointsText.text = "Points: " + PointsManager.instance.GetPoints().ToString();
    }

    public void ShowRewardGUI(string reward)
    {
        rewardText.text = reward;
        rewardText.gameObject.SetActive(true);
        Invoke("HideRewardGUI", 1f);
    }

    void HideRewardGUI()
    {
        rewardText.gameObject.SetActive(false);
    }
}
