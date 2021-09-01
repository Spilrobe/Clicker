using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private AudioClip clickFX;
    [SerializeField] private AudioSource uiFX;

    public void OnStart()
    {
        SceneManager.LoadScene("Game");
        uiFX.PlayOneShot(clickFX);
    }

    public void OnExit()
    {
        Application.Quit();
        uiFX.PlayOneShot(clickFX);
    }
}
