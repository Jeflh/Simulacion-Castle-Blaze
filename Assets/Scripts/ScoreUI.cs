using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private PlayerScript Player;
    private int score;

    void Update()
    {
        score = Player.GetScore();
        // Actualizar la puntuación del jugador aquí
        if(score > 0) ScoreText.SetText("Puntuación: " + score);
    }
}
