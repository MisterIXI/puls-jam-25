using UnityEngine;
using UnityEngine.InputSystem;


public class ReelZone : MonoBehaviour
{
    [SerializeField] private ReelFish _fish;
    [SerializeField] private GameObject _reelZone;
    [SerializeField] private GameObject _reelBackground;
    [SerializeField] private FishingProgressBar _progressBar;
    [SerializeField] private float _maxReelSpeed = 1f;
    [SerializeField] private float _reelAcceleration = 1f;
    [SerializeField][Range(0.0f, 1.0f)] private float _progressDecayRate = 0.2f;
    [SerializeField][Range(0.0f, 1.0f)] private float _progressRate = 0.5f;

    private bool _isReeling = false;
    private float MAX_REEL_DISTANCE;
    private float REEL_SIZE;
    private float _curReelSpeed;

    private void Awake()
    {
        REEL_SIZE = _reelZone.transform.localScale.y;
        MAX_REEL_DISTANCE = (_reelBackground.transform.localScale.y - REEL_SIZE) / 2;
    }
    private void Start() {
        _progressBar.SetProgress(0.3f);
    }

    public void ResetPosition()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        _curReelSpeed = 0;
    }
    private void FixedUpdate()
    {
        if (_isReeling)
            _curReelSpeed = Mathf.MoveTowards(_curReelSpeed, _maxReelSpeed, _reelAcceleration * Time.fixedDeltaTime);
        else
            _curReelSpeed = Mathf.MoveTowards(_curReelSpeed, -_maxReelSpeed, _reelAcceleration * Time.fixedDeltaTime);
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            Mathf.Clamp(transform.localPosition.y + _curReelSpeed, -MAX_REEL_DISTANCE, MAX_REEL_DISTANCE),
            transform.localPosition.z
        );
        if (transform.localPosition.y == MAX_REEL_DISTANCE || transform.localPosition.y == -MAX_REEL_DISTANCE)
        {
            _curReelSpeed = 0;
        }

        // check if fish is in reel zone
        if (Mathf.Abs(_fish.transform.localPosition.y - transform.localPosition.y) < REEL_SIZE / 2 + _fish.FishSize / 2)
        {
            _progressBar.SetProgress(_progressBar.Progress + _progressRate * Time.fixedDeltaTime);
        }
        else
        {
            _progressBar.SetProgress(_progressBar.Progress - _progressDecayRate * Time.fixedDeltaTime);
        }
    }

    public void OnPrimary(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isReeling = true;
        }
        else if (context.canceled)
        {
            _isReeling = false;
        }
    }

}
