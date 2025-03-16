using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerBreakingIce : MonoBehaviour
{
    public static PlayerBreakingIce instance;
    [SerializeField] private int breakingDistance = 10;
    [SerializeField] private GameObject iceHolePrefab;
    private Vector2 lastIceBreak;
    [SerializeField] private float timeToBreakIce = 1f;
    [SerializeField] private AudioClip[] iceBreakSounds;
    private Rigidbody2D rb;

    [SerializeField] private AudioClip[] splashSounds;

    [SerializeField] private GameObject breakingIceIndicatorPrefab;
    [SerializeField] private float timeUntilIceBreaks = 1f;
    GameObject spawnedIceBreakingIndicator;
    bool isSpawnedIceBreakingIndicator;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastIceBreak = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawnedIceBreakingIndicator && 
            rb.linearVelocity.magnitude == 0 && 
            spawnedIceBreakingIndicator == null)
        {
            isSpawnedIceBreakingIndicator = true;
            BreakIceIndicator();
        }
        else if(rb.linearVelocity.magnitude != 0)
        {
            isSpawnedIceBreakingIndicator = false;
            Destroy(spawnedIceBreakingIndicator);
        }
        
        if (!(Vector2.Distance(lastIceBreak, transform.position) > breakingDistance)) return;
        StartCoroutine(BreakIce());
    }

    void BreakIceIndicator()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1);
        if (hit.collider?.gameObject.tag == "SafeZone")
        {
            isSpawnedIceBreakingIndicator = false;
            return;
        }
        spawnedIceBreakingIndicator = Instantiate(breakingIceIndicatorPrefab, transform.position, Quaternion.identity);
        spawnedIceBreakingIndicator.GetComponent<IceBreakingIndicator>().SpawnIceAtPlayerPosition();
    }

    IEnumerator BreakIce()
    {
        lastIceBreak = new Vector2(transform.position.x, transform.position.y);
        yield return new WaitForSeconds(timeToBreakIce);
        yield return new WaitUntil(() => Vector2.Distance(lastIceBreak, transform.position) > iceHolePrefab.transform.localScale.y);
        RaycastHit2D hit = Physics2D.Raycast(lastIceBreak, Vector2.down, 1);
        if(hit.collider?.gameObject.tag == "SafeZone") yield break;
        // AudioManager.Instance.PlayClip(iceBreakSounds[Random.Range(0, iceBreakSounds.Length)],transform.position, PlayerPrefs.GetFloat("soundVolume")*1.4f, Random.Range(0.9f, 1.1f));
        GameObject spawnBreakIndicator = Instantiate(breakingIceIndicatorPrefab, transform.position, Quaternion.identity);
        spawnBreakIndicator.GetComponent<IceBreakingIndicator>().SpawnIceOnPreviousPosition();
    }

    private void OnDisable()
    {
        Destroy(spawnedIceBreakingIndicator);
    }

    public void SpawnIceHoleAtPlayerPosition()
    {
        lastIceBreak = new Vector2(transform.position.x, transform.position.y);
        AudioManager.Instance.PlayClip(splashSounds[Random.Range(0, splashSounds.Length)],transform.position, PlayerPrefs.GetFloat("soundVolume")*1.4f, Random.Range(0.9f, 1.1f));
        Instantiate(iceHolePrefab, lastIceBreak, Quaternion.identity);
    }
    
    public void SpawnIceHoleAtOldPlayerPosition(Vector2 position)
    {
        lastIceBreak = position;
        AudioManager.Instance.PlayClip(splashSounds[Random.Range(0, splashSounds.Length)], position, PlayerPrefs.GetFloat("soundVolume")*1.4f, Random.Range(0.9f, 1.1f));
        Instantiate(iceHolePrefab, position, Quaternion.identity);
    }
    
}
