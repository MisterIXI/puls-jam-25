using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTriggerCheck : MonoBehaviour
{
    bool canInteract = false;
    [SerializeField] private ReelGame _reelGame;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FishSwarm")
        {
            Debug.Log("Triggered");
            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "FishSwarm")
        {
            Debug.Log("Triggered exit");
            canInteract = false;
        }
    }

    public void InteractWithFishSwarm(InputAction.CallbackContext context)
    {
        if (!canInteract) return;
        if (context.started && _reelGame.gameObject.activeInHierarchy == false)
        {
            _reelGame.StartNewGame();
            Debug.Log("Interacting with fish swarm: Starting mini game");
        }
    }
}
