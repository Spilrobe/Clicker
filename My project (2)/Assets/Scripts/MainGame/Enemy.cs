using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyController
{
    [SerializeField] private AudioSource enemyAudioSource;
    [SerializeField] private bool isDead;
    private void Start()
    {
        ScoreCheck();
        InstantiateSpawnEffect();
        // enemyControl.OnLvlIncrease += DecreasedLvlOfProt;\ 
    }
    
    private void OnMouseDown()
    {
        enemyAudioSource.Play();
        if(isDead != true)
        OnHit();      
    }

    public void OnHit()
    {
        if (enemyHealth > minHealth)
        {
            enemyHealth--;
        }
        else
        {
            InstantiateDeathEffect();

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            foreach (Transform child in gameObject.transform)
            {
                child.GetComponent<SpriteRenderer>().enabled = false;
                isDead = true;
            }

            Invoke("DestroyObject", 0.3f);
            isDead = false;

        }
    }
}




