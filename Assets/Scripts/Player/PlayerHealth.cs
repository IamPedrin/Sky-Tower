using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3;
    private int currentHealth;

    public HealthUI healthUI;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy)
        {
            print("Collisao");
            TakeDamage(enemy.damage);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);

        StartCoroutine(flashRed());

        if(currentHealth <= 0)
        {
            //Destroy(gameObject);
        }
    }

    private IEnumerator flashRed()
    {
        sprite.color = Color.red;
        Physics2D.IgnoreLayerCollision(6, 8, true);
        yield return new WaitForSeconds(0.8f);
        sprite.color = Color.white;
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
}
