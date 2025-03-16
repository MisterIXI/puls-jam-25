using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class IceBreakingIndicator : MonoBehaviour
{
    [SerializeField] private float timeUntilIceBreak;
    SpriteRenderer spriteRenderer;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] AudioClip[] iceBreakSounds;
    AudioSource audioSource;

    IEnumerator BreakIceAtPlayerPosition()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = AudioManager.Instance.PlayClip(iceBreakSounds[Random.Range(0, iceBreakSounds.Length)], transform.position, false, PlayerPrefs.GetFloat("soundVolume") * 1.4f, Random.Range(0.9f, 1.1f));
        spriteRenderer.color = startColor;
        float timer = 0f;
        while (timer < timeUntilIceBreak)
        {
            spriteRenderer.color = Color.Lerp(startColor, endColor, timer / timeUntilIceBreak);
            timer += Time.deltaTime;
            yield return null;
        }
        PlayerBreakingIce.instance.SpawnIceHoleAtPlayerPosition();
        Destroy(gameObject);
    }
    
    public void SpawnIceAtPlayerPosition()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 1);
        foreach (RaycastHit2D hit in hits)
        {
            if(hit.collider?.tag =="SaveZone")
                Destroy(gameObject);
        }
        StopAllCoroutines();
        StartCoroutine(BreakIceAtPlayerPosition());
    }
    

    public void SpawnIceOnPreviousPosition()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 1);
        foreach (RaycastHit2D hit in hits)
        {
            if(hit.collider?.tag =="SaveZone")
                Destroy(gameObject);
        }
        StopAllCoroutines();
        StartCoroutine(IceBreakingIce());
    }

    IEnumerator IceBreakingIce()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = AudioManager.Instance.PlayClip(iceBreakSounds[Random.Range(0, iceBreakSounds.Length)], transform.position, false, PlayerPrefs.GetFloat("soundVolume") * 1.4f, Random.Range(0.9f, 1.1f));
        spriteRenderer.color = startColor;
        float timer = 0f;
        while (timer < timeUntilIceBreak)
        {
            spriteRenderer.color = Color.Lerp(startColor, endColor, timer / timeUntilIceBreak);
            timer += Time.deltaTime;
            yield return null;
        }
        PlayerBreakingIce.instance.SpawnIceHoleAtOldPlayerPosition(transform.position);
        Destroy(gameObject);
    }
    
    

    private void OnDestroy()
    {
        audioSource?.Stop();
        StopAllCoroutines();
    }
    
    
}
