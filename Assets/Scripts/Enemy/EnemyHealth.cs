using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 2;
    private int currentHealth;

    [Header("Loot")]
    public List<LootItem> lootTable = new List<LootItem>();
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
        gameObject.GetComponent<Enemy>().Pontuacao();
        Destroy(gameObject);
        foreach(LootItem lootItem in lootTable)
        {
            if(Random.Range(0, 100) <= lootItem.dropChance)
            {
                InstantiateLoot(lootItem.itemPrefab);
            }
            else
            {
                print("Nao dropou");
            }
        }
    }

    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject droppedLoot = Instantiate(loot, transform.position, Quaternion.identity);
        }
    }


}

