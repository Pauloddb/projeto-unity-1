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
    [SerializeField] private float range;


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
        float yPos = Random.Range(playerPos.y - range / 2f, playerPos.y + range / 2f);

        bullet.transform.position = new Vector3(bullet.transform.position.x, yPos, bullet.transform.position.z);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        bulletRb.velocity = new Vector2(-bulletSpeed, bulletRb.velocity.y);

        Destroy(bullet, bulletLifeTime);
    }

    private IEnumerator LoopAttack()
    {
        while (player.GetComponent<Player>().health > 0f)
        {
            Attack();
            yield return new WaitForSeconds(attackCooldown);
        }
    }
}
