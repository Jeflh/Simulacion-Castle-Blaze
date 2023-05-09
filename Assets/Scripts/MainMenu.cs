using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject scoreSelection;
    [SerializeField] private GameObject scoresGlobal;
    [SerializeField] private GameObject scoresMonth;
    [SerializeField] private GameObject scoresWeek;
    [SerializeField] private GameObject scoresPersonal;
    [SerializeField] private GameObject creditsMenu;

    private void Start()
    {
        if (PlayerPrefs.GetString("playerName") == "Ingresa nombre" || PlayerPrefs.GetString("playerName") == "")
        {
            Options();
        }
    }

    public void Play()
    {
        if (PlayerPrefs.GetString("playerName") == "Ingresa nombre" || PlayerPrefs.GetString("playerName") == "")
        {
            Options();
        }
        else
        {
            Debug.Log(PlayerPrefs.GetString("playerName"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1f;
        }
    }
    public void ScoreSelection()
    {
        mainMenu.SetActive(false);
        scoreSelection.SetActive(true);
    }

    public void ScoreGlobal()
    {
        scoreSelection.SetActive(false);
        scoresGlobal.SetActive(true);
    }

    public void ScoreMonth()
    {
        scoreSelection.SetActive(false);
        scoresMonth.SetActive(true);
    }

    public void ScoreWeek()
    {
        scoreSelection.SetActive(false);
        scoresWeek.SetActive(true);
    }

    public void ScorePersonal()
    {
        scoreSelection.SetActive(false);
        scoresPersonal.SetActive(true);
    }

    public void BackScoreSelection()
    {
        scoresGlobal.SetActive(false);
        scoresMonth.SetActive(false);
        scoresWeek.SetActive(false);
        scoresPersonal.SetActive(false);
        scoreSelection.SetActive(true);
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void Credits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        scoreSelection.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
