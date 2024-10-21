using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance; // Singleton
    private int points = 0;
    private int pointsForNextReward = 100;

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

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        Debug.Log("Total Points: " + points);

        if (points >= pointsForNextReward)
        {
            GiveReward();
            pointsForNextReward += 100;
        }
    }

    public int GetPoints()
    {
        return points;
    }

    void GiveReward()
    {
        Debug.Log("Reward!");
        RecolectEgg.instance.ShowRewardGUI("Reward!");
    }
}
