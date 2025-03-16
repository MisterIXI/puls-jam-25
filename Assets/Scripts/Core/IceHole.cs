using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class IceHole : MonoBehaviour
{
    [SerializeField] private float increaseSpeed = 0.5f;
    [SerializeField] private float maxSize;

    private IEnumerator Start()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 1);
        foreach (RaycastHit2D hit in hits)
        {
            if(hit.collider?.tag =="SaveZone")
                Destroy(gameObject);
        }


        while (transform.localScale.x < maxSize)
        {
            yield return null;
            transform.localScale += new Vector3(transform.localScale.x * increaseSpeed, transform.localScale.y * increaseSpeed, transform.localScale.z) * Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player") return;
        if(other.GetComponent<PlayerHealth>().GetIsInvincible()) return;
        other.GetComponent<PlayerHealth>().PlayerSunk();
    }
    
}
