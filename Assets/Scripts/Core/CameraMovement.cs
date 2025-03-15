using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;

    private void FixedUpdate()
    {
        Move(target.position);
    }
    
    
    void Move(Vector2 position)
    {
        if (target == null) return;
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}