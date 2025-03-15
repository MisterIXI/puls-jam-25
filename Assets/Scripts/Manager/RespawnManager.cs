using System;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Transform RespawnTransform;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RespawnPlayer()
    {
        PlayerTransform.transform.position = RespawnTransform.position;
    }

}
