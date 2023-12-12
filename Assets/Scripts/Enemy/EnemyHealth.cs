using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3;
    public int currentHealth;


    // public HealthUI healthUI;
    // private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy();
        }
    }


    public void Destroy(){
        //GameObject poofClone = Instantiate(poofVFXPrefab,transform.position,Quaternion.identity);
        //Destroy(poofClone,1.5f);
        gameObject.GetComponent<Enemy>().Pontuacao();
        Destroy(gameObject);
    }


}

