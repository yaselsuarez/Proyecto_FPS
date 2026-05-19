using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOpcionesQuality : MonoBehaviour
{
    public Dropdown dropdown;
    public int calidad;

    private void Start()
    {
        // Recuperamos el valor que indica la calidad, si la variable numeroDeCalidad no tiene un valor se le da 3 por defecto
        calidad = PlayerPrefs.GetInt("numeroDeCalidad", 3);
        // Le asignamos un valor por defecto al dropdown
        dropdown.value = calidad;
        // Definimos dicha calidad escogida
        AjustarCalidad();
    }

    // Establece la calidad del juego
    public void AjustarCalidad()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("numeroDeCalidad", dropdown.value);
        calidad = dropdown.value;
    }
}
