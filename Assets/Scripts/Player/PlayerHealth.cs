using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public GameOver gameOver; 

    [SerializeField] public int maxHealth = 3;
    public int currentHealth;

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
            // InitPlayerDeath();
            gameObject.GetComponent<Player>().InitPlayerDeath();
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

    public void GainHealth()
    {
        // Verifica se a vida atual n�o ultrapassa o m�ximo antes de ganhar vida
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            healthUI.UpdateHearts(currentHealth);
        }
    }

    // public void InitPlayerDeath(){
    //     StartCoroutine(InitPlayerDeath());
    // }

    // IEnumerator PlayerDeath(){
    //     yield return new WaitForSecondRealTime(0.3f);
    //     isDead = true;
    //     GetComponent<PlayerInput>().enabled = false;
    //     _playerRb.velocity = Vector2.zero;
    //     Physics2D.IgnoreLayerCollision(7,9,true);
    //     SceneManager.LoadScene("GameOver");
    // }
}
