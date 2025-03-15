using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerTriggerCheck : MonoBehaviour
{
    public static PlayerTriggerCheck Instance;
    bool canInteract = false;
    [SerializeField] private ReelGame _reelGame;
    [SerializeField] private GameObject interactingFishSwarm;
    [SerializeField] private AudioClip startFishingSound;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FishSwarm")
        {
            Debug.Log("Triggered");
            interactingFishSwarm = other.gameObject;
            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "FishSwarm")
        {
            Debug.Log("Triggered exit");
            interactingFishSwarm = null;
            canInteract = false;
        }
    }

    public void InteractWithFishSwarm(InputAction.CallbackContext context)
    {
        if (!canInteract) return;
        if (context.started && _reelGame.gameObject.activeInHierarchy == false)
        {
            _reelGame.StartNewGame();
            AudioManager.Instance.PlayClip(startFishingSound, transform.position, PlayerPrefs.GetFloat("soundVolume"), Random.Range(0.9f, 1.1f));
            Debug.Log("Interacting with fish swarm: Starting mini game");
        }
    }

    public void DestroyFishSwarm()
    {
        Destroy(interactingFishSwarm);
    }
}
