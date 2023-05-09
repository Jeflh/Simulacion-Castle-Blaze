using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI scoreText1;
    [SerializeField] private TextMeshProUGUI scoreText2;
    [SerializeField] private TextMeshProUGUI scoreText3;
    [SerializeField] private TextMeshProUGUI scoreText4;
    [SerializeField] private TextMeshProUGUI scoreText5;

    private const string FILE_NAME = "scores.json";

    [System.Serializable]
    private class ScoreData
    {
        public int score;
        public int round;
        public string time;
        public string date;
    }

    [System.Serializable]
    private class ScoreList
    {
        public List<ScoreData> scores = new List<ScoreData>();
    }

    private ScoreList scoreList = new ScoreList();

    private void Awake()
    {
        try
        {
            LoadScores();
            playerNameText.text = PlayerPrefs.GetString("playerName", "");
        }
        catch 
        {
            
        }
    }

    private void LoadScores()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            scoreList = JsonUtility.FromJson<ScoreList>(json);

            for (int i = 0; i < scoreList.scores.Count; i++)
            {
                ScoreData score = scoreList.scores[i];
                string scoreText = "Puntuación: " + score.score.ToString() + " | Ronda: " + score.round.ToString() + " | Tiempo: " + score.time + " | " + score.date;
                switch (i)
                {
                    case 0:
                        scoreText1.text = "1.- " + scoreText;
                        break;
                    case 1:
                        scoreText2.text = "2.- " + scoreText;
                        break;
                    case 2:
                        scoreText3.text = "3.- " + scoreText;
                        break;
                    case 3:
                        scoreText4.text = "4.- " + scoreText;
                        break;
                    case 4:
                        scoreText5.text = "5.- " + scoreText;
                        break;
                }
            }

        }
    }

    private void SaveScores()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);

        string json = JsonUtility.ToJson(scoreList);
        File.WriteAllText(filePath, json);
    }

    public void AddScore(int score, int round, string time)
    {
        ScoreData newScore = new ScoreData();
        newScore.score = score;
        newScore.round = round;
        newScore.time = time;
        newScore.date = DateTime.Now.ToString("dd/MM/yyyy");

        scoreList.scores.Add(newScore);

        // Ordenar la lista de puntajes de mayor a menor
        scoreList.scores.Sort((a, b) => b.score.CompareTo(a.score));

        // Limitar la lista de puntajes a 5 elementos
        if (scoreList.scores.Count > 5)
        {
            scoreList.scores.RemoveRange(5, scoreList.scores.Count - 5);
        }

        SaveScores();
    }

    public int GetTopScore()
    {
        if (scoreList.scores.Count > 0)
        {
            return scoreList.scores[0].score;
        }
        else
        {
            return 0;
        }
    }

    public int GetTopRound()
    {
        if (scoreList.scores.Count > 0)
        {
            return scoreList.scores[0].round;
        }
        else
        {
            return 0;
        }
    }

    public string GetTopTime()
    {
        if (scoreList.scores.Count > 0)
        {
            return scoreList.scores[0].time;
        }
        else
        {
            return "00:00";
        }
    }

}
