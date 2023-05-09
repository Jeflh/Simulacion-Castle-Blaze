using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundScript : MonoBehaviour
{
    public TextMeshProUGUI RoundText;
    public EnemysController enemysController;
    private int round;

    void Update()
    {
        round = enemysController.GetRound();
        // Actualizar la puntuación del jugador aquí
        RoundText.SetText("Ronda: " + round);
    }
}
