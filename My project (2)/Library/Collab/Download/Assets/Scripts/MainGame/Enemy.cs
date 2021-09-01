using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyController
{
    [SerializeField] private AudioSource enemyAudioSource;
    private void Start()
    {
        ScoreCheck();
        InstantiateSpawnEffect();
        // enemyControl.OnLvlIncrease += DecreasedLvlOfProt;\ 
    }
    
    private void OnMouseDown()
    {
        enemyAudioSource.Play();
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
            }

            Invoke("DestroyObject", 0.3f);
        }
    }
}




