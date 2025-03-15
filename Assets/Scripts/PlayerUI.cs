using System;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance;
    [SerializeField] private TMP_Text playerFishCounterText;
    [SerializeField] private TMP_Text fishChestCounterText;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetPlayerFishCounter(0);
        SetFishChestCounter(0);
    }

    public void SetPlayerFishCounter(int fishCount)
    {
        playerFishCounterText.text = "Inventory: " + fishCount.ToString(); 
    }

    public void SetFishChestCounter(int fishCount)
    {
        fishChestCounterText.text = "Delivered: " + fishCount.ToString();
    }
}
