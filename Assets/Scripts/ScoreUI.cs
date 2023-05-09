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
        // Actualizar la puntuaci�n del jugador aqu�
        if(score > 0) ScoreText.SetText("Puntuaci�n: " + score);
    }
}
