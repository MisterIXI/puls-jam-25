using System;
using Unity.VisualScripting;
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
    public Action CanInteractHasChanged;
    public bool CanInteract => canInteract;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FishSwarm")
        {
            interactingFishSwarm = other.gameObject;
            canInteract = true;
            CanInteractHasChanged?.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "FishSwarm")
        {
            interactingFishSwarm = null;
            canInteract = false;
            CanInteractHasChanged?.Invoke();
        }
    }

    public void InteractWithFishSwarm(InputAction.CallbackContext context)
    {
        if (!canInteract) return;
        if (context.started && _reelGame.gameObject.activeInHierarchy == false)
        {
            _reelGame.StartNewGame();
            GetComponent<PlayerBreakingIce>().enabled = false;
            AudioManager.Instance.PlayClip(startFishingSound, transform.position, PlayerPrefs.GetFloat("soundVolume"), Random.Range(0.9f, 1.1f));
        }
    }

    public void DestroyFishSwarm()
    {
        for (int i = 0; i < interactingFishSwarm.transform.childCount; i++)
        {
            var main = interactingFishSwarm.transform.GetChild(i).GetComponent<ParticleSystem>().main;
            main.loop = false;
        }
        Destroy(interactingFishSwarm.GetComponent<CircleCollider2D>());

        Destroy(interactingFishSwarm, 10);
    }
}
