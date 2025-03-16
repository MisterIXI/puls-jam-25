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
    IEnumerator Start()
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

    private void OnDestroy()
    {
        audioSource?.Stop();
        StopAllCoroutines();
    }
}
