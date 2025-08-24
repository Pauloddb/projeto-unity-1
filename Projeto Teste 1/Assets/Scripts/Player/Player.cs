#pragma warning disable CS0618

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputActions controls;
    private Vector2 moveInput;

    private Rigidbody2D rb;

    private Animator anim;

    [Header("Player Settings")]
    [SerializeField] private float speed;
    public float maxHealth;
    public float health;

    [Header("Attack Settings")]
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float attackCooldown;
    [SerializeField] private bool canAttack;
    [SerializeField] private bool isAttacking;

    private void Awake()
    {
        controls = new InputActions();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        canAttack = true;
    }

    #region Input Actions

    private void OnEnable()
    {
        controls.Enable();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Attack.performed += ctx => Attack();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    #endregion

    private void FixedUpdate()
    {
        Move();
        SetAnimation();
    }

    private void Update()
    {
        SetAnimation();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput.x, moveInput.y) * speed;

        Vector3 scale = transform.localScale;

        if (rb.velocity != Vector2.zero)
        {
            scale.x = Mathf.Sign(rb.velocity.x);
        }

        transform.localScale = scale;
    }

    #region Attack Functions

    private void Attack()
    {
        if (canAttack)
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);

            bullet.transform.rotation = Quaternion.Euler(0, 0, GetAngleToMouse(gun.position));
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x, bullet.transform.localScale.y, Mathf.Sign(rb.velocity.x));

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            bulletRb.velocity = GetDirectionToMouse(gun.position) * bulletSpeed;

            Destroy(bullet, bulletLifeTime);

            canAttack = false;
            isAttacking = true;

            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        isAttacking = false;
    }

    public float GetAngleToMouse(Vector3 pos)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 mousePositon2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

        Vector3 direction = mousePositon2D - pos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return angle;
    }

    public Vector3 GetDirectionToMouse(Vector3 pos)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 mousePositon2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

        Vector3 direction = mousePositon2D - pos;
        direction.Normalize();

        return direction;
    }

    #endregion




    private void SetAnimation()
    {
        controls.Player.Attack.performed += ctx => anim.SetTrigger("Attack");

        if (moveInput != Vector2.zero) anim.SetBool("isMoving", true);
        else anim.SetBool("isMoving", false);
    }
}