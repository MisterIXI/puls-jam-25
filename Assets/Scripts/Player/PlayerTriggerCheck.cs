using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTriggerCheck : MonoBehaviour
{    
    bool canInteract = false;
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
        if(!canInteract) return;
        if(context.started)
            Debug.Log("Interacting with fish swarm: Starting mini game");
    }
}
