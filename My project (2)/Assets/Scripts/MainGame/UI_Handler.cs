using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{
    [SerializeField] private Text scoreTxt;
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private Text HighScore;
    public int _highScore;

    private GameControl _gameControl;


    private void Start()
    {
        _gameControl = FindObjectOfType<GameControl>();

        UIPanel.SetActive(false);
         
        _gameControl.OnLose += SetGameOverUI;
        _highScore = PlayerPrefs.GetInt("SaveScore");


        HighScore.text = "High Score: " + _highScore.ToString();
    }
    public void SetScoreText(EnemyController enemy)
    {
        enemy.OnScore -= SetScoreText;
        var score = _gameControl.Score;
        scoreTxt.text = score.ToString();
    }
    public void SetGameOverUI(GameControl game)
    {
        Debug.Log("GameOverUI");
        _gameControl.OnLose -= SetGameOverUI;
        UIPanel.SetActive(true);
        
    }
}
