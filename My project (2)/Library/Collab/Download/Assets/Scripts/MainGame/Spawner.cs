using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
     public static int _enemyCounter;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private GameControl gameControl;
    [SerializeField] private UI_Handler uiHandler;
    [SerializeField] private AudioSource SpawnFX;
    [Header("Зона спауна объектов")]
    [SerializeField] private Transform spawnZone;

    [Header("Объекты, которые могут быть заспаунены")]
    [SerializeField] private List<Enemy> spawnableObjects;
      
    [Header("Настройки спаунера")]
    [SerializeField] private float spawnTime;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [HideInInspector] public bool isLose;

    private float _timer;
    private float _maxX, _minX, _maxY, _minY;

    void Start()
    {
        xAxisLimit();
        yAxisLimit();

        _timer = spawnTime;
    }

    private void Update()
    {
        if (!isLose)
        {
            SpawnTimeLimit();
        }
    }

    private void SpawnTimeLimit()
    {
        _timer -= Time.deltaTime;
         if(_timer < 0)
        {
            CreateEnemy();
            _timer = spawnTime;

            gameControl.IncreaseSpawnRate();
            Debug.Log("Timer: " + _timer);
        }
    }

    private void CreateEnemy()
    {
        var position = SetSpawnPosition();
        var index = SetEnemyType();

        var enemyObj = Instantiate(spawnableObjects[index].gameObject, position, Quaternion.identity, enemyParent);
        var enemy = enemyObj.GetComponent<Enemy>();

        SpawnFX.Play();

        enemy.OnScore += gameControl.AddScore;
        enemy.OnScore += uiHandler.SetScoreText;

        EnemyCount();
    }

    public void EnemyCount()
    {
        if (_enemyCounter >= 9)
        {
            gameControl.LoseScore();
            gameControl.OnLose += StopSpawning;
        }

        _enemyCounter++;
    }

    private void StopSpawning(GameControl game)
    {
        gameControl.OnLose -= StopSpawning;
        _enemyCounter = 0;
        isLose = true;
    }

    private Vector2 SetSpawnPosition()
    {
        return new Vector2(Random.Range(_minX, _maxX), Random.Range(_minY, _maxY));
    }

    private int SetEnemyType()
    {
        return Random.Range(0, spawnableObjects.Count);
    }

    private void xAxisLimit()
    {   
        _maxX = spawnZone.position.x - offsetX + spawnZone.localScale.x / 2;
        _minX = spawnZone.position.x + offsetX - spawnZone.localScale.x / 2;
    }

    private void yAxisLimit()
    {
         _maxY = spawnZone.position.y - offsetY + spawnZone.localScale.y / 4.5f;
         _minY = spawnZone.position.y + offsetY - spawnZone.localScale.y / 4.5f;       
    }

    public void SetSpawnTime(GameControl gameControl)
    {
        gameControl.OnSpawnTimerChanged -= SetSpawnTime;

        float minStep = 0.5f;
        spawnTime -= minStep;

        if (spawnTime <= minStep)
        {
            spawnTime = minStep;
        }
        Debug.Log("Changed SpawnTime:" + spawnTime);

    }
} 
