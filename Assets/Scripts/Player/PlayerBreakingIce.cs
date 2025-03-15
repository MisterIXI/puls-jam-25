using System;
using UnityEngine;

public class PlayerBreakingIce : MonoBehaviour
{
    [SerializeField] private int breakingDistance = 10;
    private Vector2 lastIceBreak;



    private void Start()
    {
        lastIceBreak = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(lastIceBreak, transform.position) > breakingDistance)
        {
            lastIceBreak = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            Debug.Log("Breaking ice at: " + lastIceBreak);
        }
    }
}
