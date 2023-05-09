using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    [SerializeField] private Joystick floatingJoystick;
    [SerializeField] private Joystick fixedJoystick;
    [SerializeField] private GameObject shootButton;
    [SerializeField] private GameObject pauseButton;
    private PlayerScript playerScript;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        playerScript.deathPlayer += ActiveMenu;
    }

    private void ActiveMenu(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);
        shootButton.SetActive(false);
        floatingJoystick.gameObject.SetActive(false);
        fixedJoystick.gameObject.SetActive(false);
        pauseButton.SetActive(false);
    }

    public void Reload()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
