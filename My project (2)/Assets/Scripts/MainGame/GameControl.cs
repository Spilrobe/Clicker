using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public event UnityAction<GameControl> OnLose;
    public event UnityAction<GameControl> OnSpawnTimerChanged;

    [SerializeField] private Spawner spawner;
    [SerializeField] private int score;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public void AddScore(EnemyController enemy)
    {
        Debug.Log("In AddScore function");

        enemy.OnScore -= AddScore;
        score++;

        PlayerPrefs.SetInt("SaveScore", score);

    }

    public void LoseScore()
    {
        Debug.Log("GameOver");
        OnLose?.Invoke(this);
    }

    public void IncreaseSpawnRate()
    {
        OnSpawnTimerChanged?.Invoke(this);
    }

    public void OnRestart()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnMenuExit()
    {
        SceneManager.LoadScene("Menu");
    }
}
