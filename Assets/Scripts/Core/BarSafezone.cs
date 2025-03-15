using UnityEngine;

public class BarSafezone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().SetIsInvincible(true);
            collision.gameObject.GetComponent<PlayerBreakingIce>().enabled = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().SetIsInvincible(false);
            collision.gameObject.GetComponent<PlayerBreakingIce>().enabled = true;
        }
    }
}
