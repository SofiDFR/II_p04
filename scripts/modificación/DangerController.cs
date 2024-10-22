using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DangerController : MonoBehaviour
{
    public TextMeshProUGUI dangerText;
    private GoTowards goTowards;

        void OnEnable()
    {
        EggCollider.OnCubeCollisionWithEgg += GoToEgg;
    }

    void OnDisable()
    {
        EggCollider.OnCubeCollisionWithEgg -= GoToEgg;
    }

    void Start()
    {
        goTowards = GetComponent<GoTowards>();
    }

    void GoToEgg(GameObject egg)
    {
        if (goTowards != null)
        {
            goTowards.SetTarget(egg.transform);
            ShowDangerGUI();
        }
    }

    public void ShowDangerGUI()
    {
        dangerText.text = "Danger Zone!";
        dangerText.gameObject.SetActive(true);
        Invoke("HideDangerGUI", 1f);
    }

    void HideDangerGUI()
    {
        dangerText.gameObject.SetActive(false);
    }
}