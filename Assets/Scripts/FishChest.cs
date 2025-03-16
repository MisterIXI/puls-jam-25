using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishChest : MonoBehaviour
{
    public static FishChest Instance;
    [SerializeField] private int fishScoreCounter = 0;
    [SerializeField] private AudioClip putFishInChestSound;
    
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        fishScoreCounter += other.gameObject.GetComponent<PlayerFishCounter>().GetFishCount();
        other.gameObject.GetComponent<PlayerFishCounter>().ClearFishCounter();
        AudioManager.Instance.PlayClip(putFishInChestSound, transform.position, PlayerPrefs.GetFloat("soundVolume"), Random.Range(0.9f, 1.1f));
        PlayerUI.Instance.SetFishChestCounter(fishScoreCounter);
    }

    public int GetFishCount()
    {
        return fishScoreCounter;
    }
}
