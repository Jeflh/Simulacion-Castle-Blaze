using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private string serverDB;
    [SerializeField] private string nameDB;
    [SerializeField] private string userDB;
    [SerializeField] private string passwordDB;

    private string dataConnection;
    private MySqlConnection connection;

    [SerializeField] private ScoreGlobalManager scoreGlobalManager;

    [System.Serializable]
    public class ScoreData
    {
        public string name;
        public int score;
        public int round;
        public string time;
        public string date;
    }

    void Awake()
    {
        dataConnection = "Server=" + serverDB + ";Database=" + nameDB + ";Uid=" + userDB + ";Pwd=" + passwordDB + ";";
        Connect();
    }

    private void Connect()
    {
        connection = new MySqlConnection(dataConnection);

        try
        {
            connection.Open();
            Debug.Log("Conexión con DB correcta");
            scoreGlobalManager.CheckLocalBest();
        }
        catch (MySqlException error)
        {
            Debug.Log("Error al conectar a la DB: " + error);
        }
    }
    private void ExecuteNonQuery(string query)
    {
        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
    }

    public void InsertScore(string name, int score, int round, string time)
    {
        string fechaActual = DateTime.Now.ToString("yyyy/MM/dd");
        string query = "INSERT INTO scores (name, score, round, time, date) VALUES ('" + name + "', " + score + ", " + round + ", '" + time + "', '" + fechaActual + "')";
        ExecuteNonQuery(query);
    }

    public bool CheckScoreExists(string name, int score, int round, string time)
    {
        string query = "SELECT COUNT(*) FROM scores WHERE name = '" + name + "' AND score = " + score + " AND round = " + round + " AND time = '" + time + "'";
        MySqlCommand command = new MySqlCommand(query, connection);
        int count = int.Parse(command.ExecuteScalar().ToString());
        return count > 0;
    }

    public bool CheckNameExists(string name)
    {
        string query = "SELECT COUNT(*) FROM scores WHERE name = '" + name + "'";
        MySqlCommand command = new MySqlCommand(query, connection);
        int count = int.Parse(command.ExecuteScalar().ToString());
        return count > 0;
    }

    public List<ScoreData> GetTopScores()
    {
        string query = "SELECT * FROM scores WHERE score > 0 ORDER BY score DESC LIMIT 10";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        List<ScoreData> topScores = new List<ScoreData>();

        while (reader.Read())
        {
            ScoreData score = new ScoreData();
            score.name = reader.GetString("name");
            score.score = reader.GetInt32("score");
            score.round = reader.GetInt32("round");
            score.time = reader.GetString("time");
            score.date = reader.GetDateTime(reader.GetOrdinal("date")).ToString("dd/MM/yyyy");
            topScores.Add(score);
        }

        reader.Close();

        return topScores;
    }


    public List<ScoreData> GetTopScoresOfWeek()
    {
        DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        DateTime endOfWeek = startOfWeek.AddDays(7);

        string query = $"SELECT * FROM scores WHERE score > 0 AND date BETWEEN '{startOfWeek.ToString("yyyy-MM-dd")}' AND '{endOfWeek.ToString("yyyy-MM-dd")}' ORDER BY score DESC LIMIT 5";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        List<ScoreData> topScores = new List<ScoreData>();

        while (reader.Read())
        {
            ScoreData score = new ScoreData();
            score.name = reader.GetString("name");
            score.score = reader.GetInt32("score");
            score.round = reader.GetInt32("round");
            score.time = reader.GetString("time");
            score.date = reader.GetDateTime(reader.GetOrdinal("date")).ToString("dd/MM/yyyy");
            topScores.Add(score);
        }

        reader.Close();

        return topScores;
    }


    public List<ScoreData> GetTopScoresOfMonth()
    {
        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        string query = $"SELECT * FROM scores WHERE score > 0 AND date BETWEEN '{firstDayOfMonth.ToString("yyyy-MM-dd")}' AND '{lastDayOfMonth.ToString("yyyy-MM-dd")}' ORDER BY score DESC LIMIT 7";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        List<ScoreData> topScores = new List<ScoreData>();

        while (reader.Read())
        {
            ScoreData score = new ScoreData();
            score.name = reader.GetString("name");
            score.score = reader.GetInt32("score");
            score.round = reader.GetInt32("round");
            score.time = reader.GetString("time");
            score.date = reader.GetDateTime(reader.GetOrdinal("date")).ToString("dd/MM/yyyy");
            topScores.Add(score);
        }

        reader.Close();

        return topScores;
    }


    public bool CheckIfBestScore(string name, int actualScore)
    {
        string query = "SELECT MAX(score) FROM scores WHERE name = '" + name + "'";
        MySqlCommand command = new MySqlCommand(query, connection);
        int bestScore = int.Parse(command.ExecuteScalar().ToString());
        return actualScore > bestScore;
    }

    public void UpdateScore(string name, int score, int round, string time)
    {
        string fechaActual = DateTime.Now.ToString("yyyy/MM/dd");
        string query = "UPDATE scores SET score = " + score + ", round = " + round + ", time = '" + time + "', date = '" + fechaActual + "' WHERE name = '" + name + "'";
        ExecuteNonQuery(query);
    }

    public List<string> GetAllNames()
    {
        string query = "SELECT name FROM scores";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<string> names = new List<string>();

        while (reader.Read())
        {
            string name = reader.GetString("name");
            names.Add(name);
        }

        reader.Close();

        return names;
    }


    private void OnApplicationQuit()
    {
        if (connection != null && connection.State != System.Data.ConnectionState.Closed)
        {
            connection.Close();
            Debug.Log("Desconexión de la base de datos exitosa.");
        }
    }
}
