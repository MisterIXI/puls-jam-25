using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float _acceleration = 5f;
    private Vector2 _movementInput;
    private Rigidbody2D _rigidbody;
    private Vector2 _currentVelocity;
    public bool MovementDisabled = false;
    public static PlayerMovement Instance;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.linearDamping = 5f;
    }

    private void FixedUpdate()
    {
        if (_movementInput.magnitude > 0)
        {
            // move player
            _currentVelocity = Vector2.MoveTowards(_currentVelocity, _movementInput * movementSpeed, _acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // slow down player
            _currentVelocity = Vector2.MoveTowards(_currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
        if (MovementDisabled)
        {
            _currentVelocity = Vector2.zero;
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
