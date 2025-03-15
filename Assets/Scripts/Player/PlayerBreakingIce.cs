using System;
using System.Collections;
using UnityEngine;

public class PlayerBreakingIce : MonoBehaviour
{
    [SerializeField] private int breakingDistance = 10;
    [SerializeField] private GameObject iceHolePrefab;
    private Vector2 lastIceBreak;
    [SerializeField] private float timeToBreakIce = 1f;
    
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
        Instantiate(iceHolePrefab, lastIceBreak, Quaternion.identity);
    }
}
