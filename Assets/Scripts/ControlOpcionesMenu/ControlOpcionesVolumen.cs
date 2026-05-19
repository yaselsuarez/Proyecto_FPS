using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOpcionesVolumen : MonoBehaviour
{
    [Header("Control Volumen")]
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;   

    private void Start()
    {
        // Optenemos la variable volumenAudio 
        // Si la variable no tiene ningun valor por defecto le damos 0.5f
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        // Le damos el valor de slider.value al volumen del juego
        AudioListener.volume = slider.value;
    }

    public void ChangeSlider(float valor)
    {
        // Guardamos el valor pasado por parametro
        sliderValue = valor;
        // Guardamos este valor en la variable volumenAudio
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        // Le damos el valor de slider.value al volumen del juego
        AudioListener.volume = slider.value;
        // Comprobamos si ha bajado todo el volumen y activamos el icono de Mute
        RevisarSiEstoyMute();
    }

    // Si el volumen esta en 0 se muestra el icono de Mute
    public void RevisarSiEstoyMute()
    {
        if (sliderValue == 0)
        {
            imagenMute.enabled = true;
        }
        else
        {
            imagenMute.enabled = false;
        }
    }

    

}
