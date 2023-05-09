using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Globalization;

public class ScoreGlobalManager : MonoBehaviour
{
    [Header("Score Global Text")]
    [SerializeField] private TextMeshProUGUI scoreText1;
    [SerializeField] private TextMeshProUGUI scoreText2;
    [SerializeField] private TextMeshProUGUI scoreText3;
    [SerializeField] private TextMeshProUGUI scoreText4;
    [SerializeField] private TextMeshProUGUI scoreText5;
    [SerializeField] private TextMeshProUGUI scoreText6;
    [SerializeField] private TextMeshProUGUI scoreText7;
    [SerializeField] private TextMeshProUGUI scoreText8;
    [SerializeField] private TextMeshProUGUI scoreText9;
    [SerializeField] private TextMeshProUGUI scoreText10;

    [Header("Score Month Text")]
    [SerializeField] private TextMeshProUGUI scoreMonthText1;
    [SerializeField] private TextMeshProUGUI scoreMonthText2;
    [SerializeField] private TextMeshProUGUI scoreMonthText3;
    [SerializeField] private TextMeshProUGUI scoreMonthText4;
    [SerializeField] private TextMeshProUGUI scoreMonthText5;
    [SerializeField] private TextMeshProUGUI scoreMonthText6;
    [SerializeField] private TextMeshProUGUI scoreMonthText7;
    //[SerializeField] private TextMeshProUGUI scoreMonthText8;
    //[SerializeField] private TextMeshProUGUI scoreMonthText9;
    //[SerializeField] private TextMeshProUGUI scoreMonthText10;

    [Header("Score Week Text")]
    [SerializeField] private TextMeshProUGUI scoreWeekText1;
    [SerializeField] private TextMeshProUGUI scoreWeekText2;
    [SerializeField] private TextMeshProUGUI scoreWeekText3;
    [SerializeField] private TextMeshProUGUI scoreWeekText4;
    [SerializeField] private TextMeshProUGUI scoreWeekText5;
    //[SerializeField] private TextMeshProUGUI scoreWeekText6;
    //[SerializeField] private TextMeshProUGUI scoreWeekText7;
    //[SerializeField] private TextMeshProUGUI scoreWeekText8;
    //[SerializeField] private TextMeshProUGUI scoreWeekText9;
    //[SerializeField] private TextMeshProUGUI scoreWeekText10;

    [Header("Manager's")]
    [SerializeField] private DatabaseManager databaseManager;
    [SerializeField] private ScoreManager scoreManager;

    [System.Serializable]
    public class ScoreData
    {
        public string name;
        public int score;
        public int round;
        public string time;
        public string date;
    }

    public void CheckLocalBest()
    {
        ScoreData bestLocal = new ScoreData();
        bestLocal.name = PlayerPrefs.GetString("playerName", "Jugador");
        bestLocal.score = scoreManager.GetTopScore();
        bestLocal.round = scoreManager.GetTopRound();
        bestLocal.time = scoreManager.GetTopTime();

        if(databaseManager.CheckNameExists(bestLocal.name) && databaseManager.CheckIfBestScore(bestLocal.name, bestLocal.score))
        {
            databaseManager.UpdateScore(bestLocal.name, bestLocal.score, bestLocal.round, bestLocal.time);
            Debug.Log("Actualizado");
        }
        else if (!databaseManager.CheckNameExists(bestLocal.name))
        {
            Debug.Log("Nuevo");
            databaseManager.InsertScore(bestLocal.name, bestLocal.score, bestLocal.round, bestLocal.time);
        }
        Debug.Log("Se mantiene igual");
    }

    public void LoadTopScores()
    {
        List<DatabaseManager.ScoreData> topScores = databaseManager.GetTopScores();
        topScores.Sort((x, y) => y.score.CompareTo(x.score));

        for (int i = 0; i < topScores.Count; i++)
        {
            string text = (i + 1).ToString() + ".- " + topScores[i].name + " | Puntuación: " + topScores[i].score.ToString() + " | Ronda: " + topScores[i].round.ToString() + " | Tiempo: " + topScores[i].time + " | " + topScores[i].date;
            switch (i)
            {
                case 0:
                    scoreText1.text = text;
                    break;
                case 1:
                    scoreText2.text = text;
                    break;
                case 2:
                    scoreText3.text = text;
                    break;
                case 3:
                    scoreText4.text = text;
                    break;
                case 4:
                    scoreText5.text = text;
                    break;
                case 5:
                    scoreText6.text = text;
                    break;
                case 6:
                    scoreText7.text = text;
                    break;
                case 7:
                    scoreText8.text = text;
                    break;
                case 8:
                    scoreText9.text = text;
                    break;
                case 9:
                    scoreText10.text = text;
                    break;
            }
        }
    }

    public void LoadTopScoresOfMonth()
    {
        List<DatabaseManager.ScoreData> topScoresOfWeek = databaseManager.GetTopScoresOfMonth();
        topScoresOfWeek.Sort((x, y) => y.score.CompareTo(x.score));

        for (int i = 0; i < topScoresOfWeek.Count; i++)
        {
            string text = (i + 1).ToString() + ".- " + topScoresOfWeek[i].name + " | Puntuación: " + topScoresOfWeek[i].score.ToString() + " | Ronda: " + topScoresOfWeek[i].round.ToString() + " | Tiempo: " + topScoresOfWeek[i].time + " | " + topScoresOfWeek[i].date;
            switch (i)
            {
                case 0:
                    scoreMonthText1.text = text;
                    break;
                case 1:
                    scoreMonthText2.text = text;
                    break;
                case 2:
                    scoreMonthText3.text = text;
                    break;
                case 3:
                    scoreMonthText4.text = text;
                    break;
                case 4:
                    scoreMonthText5.text = text;
                    break;
                case 5:
                    scoreMonthText6.text = text;
                    break;
                case 6:
                    scoreMonthText7.text = text;
                    break;
                /*case 7:
                    scoreMonthText8.text = text;
                    break;
                case 8:
                    scoreMonthText9.text = text;
                    break;
                case 9:
                    scoreMonthText10.text = text;
                    break;
                */
            }
        }
    }


    public void LoadTopScoresOfWeek()
    {
        List<DatabaseManager.ScoreData> topScoresOfWeek = databaseManager.GetTopScoresOfWeek();
        topScoresOfWeek.Sort((x, y) => y.score.CompareTo(x.score));

        for (int i = 0; i < topScoresOfWeek.Count; i++)
        {
            string text = (i + 1).ToString() + ".- " + topScoresOfWeek[i].name + " | Puntuación: " + topScoresOfWeek[i].score.ToString() + " | Ronda: " + topScoresOfWeek[i].round.ToString() + " | Tiempo: " + topScoresOfWeek[i].time + " | " + topScoresOfWeek[i].date;
            switch (i)
            {
                case 0:
                    scoreWeekText1.text = text;
                    break;
                case 1:
                    scoreWeekText2.text = text;
                    break;
                case 2:
                    scoreWeekText3.text = text;
                    break;
                case 3:
                    scoreWeekText4.text = text;
                    break;
                case 4:
                    scoreWeekText5.text = text;
                    break;
                /*case 5:
                    scoreWeekText6.text = text;
                    break;
                case 6:
                    scoreWeekText7.text = text;
                    break;
               case 7:
                    scoreWeekText8.text = text;
                    break;
                case 8:
                    scoreWeekText9.text = text;
                    break;
                case 9:
                    scoreWeekText10.text = text;
                    break;
                */
            }
        }
    }
}
