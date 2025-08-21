using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Life Settings")]
    public float maxHealth = 100f;
    public float health = 100f;

    [Header("Attack Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gun;
    [SerializeField] private Transform player;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifeTime = 5f;


    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }

    private void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
    }
}
