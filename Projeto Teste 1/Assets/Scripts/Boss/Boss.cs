#pragma warning disable CS0618

using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Life Settings")]
    public float maxHealth;
    public float health;

    [Header("Attack Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject player;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float attackCooldown;


    private void Awake()
    {
        StartCoroutine(LoopAttack());
    }

    private void Update()
    {
        
    }

    private void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);

        Vector3 playerPos = player.transform.position;

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = (playerPos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        bullet.transform.rotation = Quaternion.LookRotation(direction);

        bulletRb.velocity = new Vector2(-bulletSpeed, bulletRb.velocity.y);

        Destroy(bullet, bulletLifeTime);
    }

    private IEnumerator LoopAttack()
    {
        while (player.GetComponent<Player>().health > 0)
        {
            Attack();
            yield return new WaitForSeconds(attackCooldown);
        }
    }
}
