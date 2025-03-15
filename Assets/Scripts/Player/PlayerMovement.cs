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
        }
        else
        {
            // slow down player
            _currentVelocity = Vector2.Lerp(_currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }

        _rigidbody.linearVelocity = _currentVelocity;

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
