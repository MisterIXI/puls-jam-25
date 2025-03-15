using UnityEngine;

public class ReelFish : MonoBehaviour
{
    [SerializeField] private GameObject _reelBackground;
    [SerializeField] private Vector2 _dirChangeInterval;
    [SerializeField] private float _burstSpeed;
    [SerializeField][Range(0.0f,1.0f)] private float _decayRate = 0.5f;

    private float _burstCooldown;
    private float _currVelocity;
    private float FISH_SIZE;
    private float MAX_FISH_DISTANCE;
    public float FishSize => FISH_SIZE;
    private void Awake()
    {
        FISH_SIZE = GetComponent<SpriteRenderer>().bounds.size.y;
        MAX_FISH_DISTANCE = (_reelBackground.transform.localScale.y - FISH_SIZE) / 2;
    }
    private void FixedUpdate()
    {
        _currVelocity = Mathf.MoveTowards(_currVelocity, 0, _decayRate * Time.fixedDeltaTime);
        if (_burstCooldown <= 0)
        {
            _burstCooldown = Random.Range(_dirChangeInterval.x, _dirChangeInterval.y);
            _currVelocity = Random.Range(-_burstSpeed, _burstSpeed);
        }
        else
        {
            _burstCooldown -= Time.fixedDeltaTime;
        }

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            Mathf.Clamp(transform.localPosition.y + _currVelocity * Time.fixedDeltaTime, -MAX_FISH_DISTANCE, MAX_FISH_DISTANCE),
            transform.localPosition.z
        );



    }

}
