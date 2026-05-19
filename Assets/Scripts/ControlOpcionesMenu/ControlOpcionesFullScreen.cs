using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOpcionesFullScreen : MonoBehaviour
{
    [Header("Control Pantalla")]
    public Toggle toggle;

    private void Start()
    {
        // Si estamos a pantalla completa se marca a true la casilla sino a false
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }

    // Activa y desactiva la pantalla completa
    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
}
