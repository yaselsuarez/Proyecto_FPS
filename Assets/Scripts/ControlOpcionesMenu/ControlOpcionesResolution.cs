using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ControlOpcionesResolution : MonoBehaviour
{
    [Header("Control Pantalla")]
    public Toggle toggle;

    public Dropdown resolucionesDrowpDown;
    Resolution[] resoluciones;

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

        RevisarResolucion();
    }

    private void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        resolucionesDrowpDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            opciones.Add(opcion);

            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }
        resolucionesDrowpDown.AddOptions(opciones);
        resolucionesDrowpDown.value = resolucionActual;
        resolucionesDrowpDown.RefreshShownValue();

        resolucionesDrowpDown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        PlayerPrefs.SetInt("numeroResolucion", resolucionesDrowpDown.value);

        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    // Activa y desactiva la pantalla completa
    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
}
