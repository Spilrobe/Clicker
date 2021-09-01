using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyType
{
    Square, Circle, Triangle, Rhombus
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject spawnEffectPrefab;
    [SerializeField] private GameObject deathEffectPrefab;

    [SerializeField] protected int enemyHealth;

    public event UnityAction<EnemyController> OnScore;
    public event UnityAction<EnemyController> OnEnemyHealthIncreased;

    protected int minHealth = 1;
    protected int maxHealth = 4;

    public void DestroyObject()
    {
        Debug.Log("In Destroy function");

        OnScore?.Invoke(this);
        Destroy(gameObject);
        Spawner._enemyCounter--;
    } 
    public void IncreaseEnemyHealth()
    {
        OnEnemyHealthIncreased?.Invoke(this);
    }

    public void SetEnemyHealtht(EnemyController lvl)
    {
        Debug.Log("In SetEnemyHealth function");

        lvl.OnEnemyHealthIncreased -= SetEnemyHealtht;
        enemyHealth = Random.Range(minHealth, maxHealth);
    }

    protected void ScoreCheck()
    {
        var gameControl = FindObjectOfType<GameControl>();
        var spawner = FindObjectOfType<Spawner>();
        if (gameControl.Score % 3 == 0)
        {
            Debug.Log("score % 3");
            OnEnemyHealthIncreased += SetEnemyHealtht;
            IncreaseEnemyHealth();
        }
        else
        {
            enemyHealth = minHealth;
        }

        if (gameControl.Score % 5 == 0)
        {         
                Debug.Log("Score % 5");
                gameControl.OnSpawnTimerChanged += spawner.SetSpawnTime;
                gameControl.IncreaseSpawnRate();          
        }
    }

    protected void InstantiateSpawnEffect()
    {
        var effectPosition = new Vector2(transform.position.x, transform.position.y + 1.0f);
        var spawnVFX = Instantiate(spawnEffectPrefab, effectPosition, transform.rotation);
        Destroy(spawnVFX, 0.5f);
    }

    protected void InstantiateDeathEffect()
    {
        var deathVFX = Instantiate(deathEffectPrefab, transform.position, transform.rotation);
        Destroy(deathVFX, 0.25f);
    }
}