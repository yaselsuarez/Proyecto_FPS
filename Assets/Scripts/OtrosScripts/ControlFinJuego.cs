using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlFinJuego : MonoBehaviour
{
    public TextMeshProUGUI txtGameOver;
    public TextMeshProUGUI txtScore;
    public int puntuacion = 0;

    void Awake()
    {
        puntuacion = ControlJuego.instancia.puntuacionActual;

        if(puntuacion > 5)
        {
            txtGameOver.text = "HAS GANADO";
            txtScore.text = "Score: " + puntuacion;
        }
        else
        {
            txtGameOver.text = "GAME OVER";
            txtScore.text = "Score: " + puntuacion;
        }
    }
}
