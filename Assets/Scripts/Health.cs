using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 150;
    public float forceReaction = 10;
    public string healthBoxTag = "HealthBox";

    public Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void TakeHit(int damage)
    {
        health -= damage;
        HitReaction();

        //Debug.Log("Damage received. Health: " + health);

        if (health <= 0)
        {
            Debug.Log("Health is zero.");
            Destroy(gameObject);
        }
    }

    public void SetHealth(int add_health)
    {
        health += add_health;

        if (health > maxHealth)
            health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(healthBoxTag))
        {
            HealthBox healthBox = collision.gameObject.GetComponent<HealthBox>();
            health += healthBox.health;
            Destroy(collision.gameObject);
        }
    }

    public void HitReaction()
    {
        rigidbody2d.AddForce(Vector2.up * forceReaction, ForceMode2D.Impulse);
    }
}
