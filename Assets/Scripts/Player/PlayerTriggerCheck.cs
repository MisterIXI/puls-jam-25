using UnityEngine;

public class PlayerTriggerCheck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entering");
    }


    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exiting");
    }
}
