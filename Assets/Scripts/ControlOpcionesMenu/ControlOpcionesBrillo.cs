using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOpcionesBrillo : MonoBehaviour
{
    [Header("Control Brillo")]
    public Slider slider;
    public float sliderValue;
    public Image panelBrillo;

    private void Start()
    {
        // Guardamos el valor de la variable brillo, si no tiene ningun valor nos guardará 0.5f de manera predefinida
        slider.value = PlayerPrefs.GetFloat("brillo", 0.5f);

        // Establecemos el color inicial para la imagen panelBrillo
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value);
    }

    public void ChangeSlider(float valor)
    { 
        // Guardamos el valor pasado por parametros
        sliderValue = valor;
        // Guardamos el valor en la variable brillo
        PlayerPrefs.SetFloat("brillo", sliderValue);
        // Establecemos el nuevo valor alfa del panelBrillo
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value);

    }
}
