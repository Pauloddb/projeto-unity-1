using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float power;
    [SerializeField] private GameObject alvo;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(this.alvo.tag))
        {
            float alvoPower = alvo.GetComponent<Bullet>().power;

            if (alvoPower > this.power)
            {
                Destroy(col.gameObject);
            }
            else if (alvoPower < this.power)
            {
                Destroy(this);
            }
            else
            {
                Destroy(col.gameObject);
                Destroy(this);
            }
        }
        else if (col.CompareTag("Player") && this.tag == "Enemy Attack")
        {
            col.GetComponent<Player>().health -= this.power;
        }
    }
}
