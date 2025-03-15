using System;
using UnityEngine;

public class PlayerFishCounter : MonoBehaviour
{
    public static PlayerFishCounter Instance;
    [SerializeField] private int fishCaughtCounter = 0;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void AddFish()
    {
        fishCaughtCounter++;
        PlayerUI.Instance.SetPlayerFishCounter(fishCaughtCounter);
    }

    public int GetFishCount()
    {
        return fishCaughtCounter;
    }

    public void ClearFishCounter()
    {
        fishCaughtCounter = 0;
        PlayerUI.Instance.SetPlayerFishCounter(0);
    }
}
