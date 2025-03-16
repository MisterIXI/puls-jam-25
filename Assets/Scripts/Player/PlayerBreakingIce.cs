using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerBreakingIce : MonoBehaviour
{
    [SerializeField] private int breakingDistance = 10;
    [SerializeField] private GameObject iceHolePrefab;
    private Vector2 lastIceBreak;
    [SerializeField] private float timeToBreakIce = 1f;
    [SerializeField] private AudioClip[] iceBreakSounds;
    
    private void Start()
    {
        lastIceBreak = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Vector2.Distance(lastIceBreak, transform.position) > breakingDistance)) return;
        StartCoroutine(BreakIce());
    }

    IEnumerator BreakIce()
    {
        lastIceBreak = new Vector2(transform.position.x, transform.position.y);
        yield return new WaitForSeconds(timeToBreakIce);
        yield return new WaitUntil(() => Vector2.Distance(lastIceBreak, transform.position) > iceHolePrefab.transform.localScale.y);
        RaycastHit2D hit = Physics2D.Raycast(lastIceBreak, Vector2.down, 1);
        if(hit.collider?.gameObject.tag == "SafeZone") yield break;
        AudioManager.Instance.PlayClip(iceBreakSounds[Random.Range(0, iceBreakSounds.Length)],transform.position, PlayerPrefs.GetFloat("soundVolume")*1.4f, Random.Range(0.9f, 1.1f));
        Instantiate(iceHolePrefab, lastIceBreak, Quaternion.identity);

    }
}
