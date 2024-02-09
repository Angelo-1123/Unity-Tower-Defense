using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    public float speed;
    public float prowlingSpeed = 0;
    public float baseHealth = 50;
    public float startHealth = 100;
    public float health = 100;
    public float baseArmor = 0;
    public float startArmor = 0;
    public float armor = 20;
    public int baseWorth = 3;
    public float worth = 3;
    public GameObject deathEffect;
    public Image healthBar;
    public int newGamePlus = 0;

    public int enemyDamage = 1;

    public int baseProwl = 0;
    public int startProwl = 0;
    public int prowl = 0;
    public bool Prowling = false;
    public bool Shortcut = false;
    public bool TakeShortcut = false;
    public bool Boss = false;
    public bool Fly = false;
    public bool Crumbling = false;
    public float crumbledSpeed = 0;
    private bool Collected;

    void Start ()
    {
        speed = startSpeed;
        health = startHealth;
        prowl = startProwl;
        armor = startArmor;
        Collected = false;
        if(prowl > 0)
        {
            healthBar.color = Color.blue;
        }
        else
        {
            healthBar.color = Color.green;
        }
    }

    public void TakeDamage (float amount, bool ignoreArmor)
    {   
        if(prowl > 0)
        {
            if(ignoreArmor == true)
            {
                return;
            }

            prowl--;

            if(Prowling)
            {
                startSpeed = prowlingSpeed; 
                Prowling = false;
            }

            if(prowl == 0)
            {
                healthBar.color = Color.green;
                if(Shortcut == true)
                {
                    TakeShortcut = true;
                }
            }
            
            return;
        }

        if(ignoreArmor == true)
        {
            health -= amount;
            healthBar.fillAmount = health / startHealth;
        }
        else
        {
            health -= (amount - armor);
            healthBar.fillAmount = health / startHealth;
            if(Crumbling == true && armor > 0)
            {
                armor--;
            }
            if(Crumbling == true && armor <= 0)
            {
                armor = -5;
                startSpeed = crumbledSpeed;
                healthBar.color = Color.red;
            }
            //Debug.Log("armor: " + armor);
            //Debug.Log("health: " + health);
        }

        if(health <= 0)
        {
            Die();
        }
    }

    public void Slow (float amount)
    {
        if(Boss == true)
        {
            if(Fly == true)
            {
                speed = startSpeed * (1 - (amount/3));
            }
            else
            {
                speed = startSpeed * (1 - (amount/2));
            }
            
        }
        else
        {
            if(Fly == true)
            {
                speed = startSpeed * (1 - amount/2);
            }
            else
            {
                speed = startSpeed * (1 - amount);
            }

        }
    }

    void Die ()
    {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        if(Collected == false)
        {
            PlayerStats.Money += worth;
            Collected = true;
        }
        Destroy(gameObject);
    }
}
