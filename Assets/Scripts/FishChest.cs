using System;
using Unity.VisualScripting;
using UnityEngine;

public class FishChest : MonoBehaviour
{
    [SerializeField] private int fishScoreCounter = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            fishScoreCounter += other.gameObject.GetComponent<PlayerFishCounter>().GetFishCount();
            other.gameObject.GetComponent<PlayerFishCounter>().ClearFishCounter();
            PlayerUI.Instance.SetFishChestCounter(fishScoreCounter);
        }
    }
}
