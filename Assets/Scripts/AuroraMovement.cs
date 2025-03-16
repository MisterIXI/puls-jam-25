using System.Collections;
using UnityEngine;

public class AuroraMovement : MonoBehaviour
{
    [SerializeField] private float startPointY = 20;
    [SerializeField] private float timeToReach = 40;
    private float _elapsedTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        Vector2 startPoint = new Vector2(transform.localPosition.x, startPointY);
        while (_elapsedTime < timeToReach)
        {
            _elapsedTime += Time.deltaTime;
            transform.localPosition = Vector2.Lerp(startPoint, Vector2.zero, _elapsedTime / timeToReach);
            yield return null;
        }
    }
    
}
