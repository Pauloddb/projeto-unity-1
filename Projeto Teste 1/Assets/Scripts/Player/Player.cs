#pragma warning disable CS0618

using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputActions controls;
    private Vector2 moveInput;

    private Rigidbody2D rb;

    [SerializeField] private float speed = 5f;

    private void Awake()
    {
        controls = new InputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    #region Input Actions

    private void OnEnable()
    {
        controls.Enable();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    #endregion

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput.x, moveInput.y) * speed;
    }
}
#pragma warning restore CS0618