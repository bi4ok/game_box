using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffect;

    public float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ApplyDamage(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyDamage(collision);
    }

    private void ApplyDamage(Collider2D collision)
    {
        if(!collision.CompareTag("Player") && !collision.CompareTag("Bonus"))
        {
            Debug.Log(collision.name);
            BlastHim(collision);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);

        }

    }

    private void BlastHim(Collider2D collision)
    {
        IDamageAble enemy = collision.GetComponent<IDamageAble>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage, gameObject.tag);
        }
        

    }

}
