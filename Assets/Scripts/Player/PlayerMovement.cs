using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float deceleration = 5f;
    private Vector2 _movementInput;
    private Rigidbody2D _rigidbody;
    private Vector2 _currentVelocity;
    [SerializeField] private float _stepSoundDelay = 0.5f;
    [SerializeField] private AudioClip[] footsepSounds1;
    [SerializeField] private AudioClip[] footsepSounds2;
    private bool _canPlayFootstepSound = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.linearDamping = 5f;
    }

    private void FixedUpdate()
    {
        if (_movementInput.magnitude > 0)
        {
            // move player
            _currentVelocity = _movementInput * movementSpeed;
            if (_canPlayFootstepSound)
            {
                AudioManager.Instance.PlayClip(footsepSounds1[Random.Range(0, footsepSounds1.Length)],transform.position, PlayerPrefs.GetFloat("soundVolume")*0.3f, UnityEngine.Random.Range(0.9f, 1.1f));
                AudioManager.Instance.PlayClip(footsepSounds2[Random.Range(0, footsepSounds2.Length)],transform.position, PlayerPrefs.GetFloat("soundVolume")*0.3f, UnityEngine.Random.Range(0.9f, 1.1f));
                _canPlayFootstepSound = false;
                StartCoroutine(FootstepSoundCooldown());
            }
        }
        else
        {
            // slow down player
            _currentVelocity = Vector2.Lerp(_currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }

        _rigidbody.linearVelocity = _currentVelocity;
    }
    
    private IEnumerator FootstepSoundCooldown()
    {
        yield return new WaitForSeconds(_stepSoundDelay);
        _canPlayFootstepSound = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }


    public void Move(Vector2 direction)
    {
        _movementInput = direction * movementSpeed;
    }
}
