using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenuPrincipal : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelMenuPrincipal;
    public GameObject panelMenuCreditos;
    public GameObject panelMenuOpciones;

    private void Awake()
    {
     // Activa el menú principal, desactivando todos los demas 
        ActivarPanel(panelMenuPrincipal);   
    }

    // Comienza el juego
    public void OnStart()
    {
        SceneManager.LoadScene(1);
    }

    // Muestra las opciones del juego
    public void OnOptions()
    {
        ActivarPanel(panelMenuOpciones);
    }

    // Muestra los créditos del juego
    public void OnCredits()
    {
        ActivarPanel(panelMenuCreditos);
    }

    // Sale del juego
    public void OnExit()
    {
        Application.Quit();
    }

    // Vuelve al menú principal
    public void OnVolverMenuPrincipal()
    {
        ActivarPanel(panelMenuPrincipal);
    }

    // Desactiva todos los paneles y activa el que pasamos por parametro
    public void ActivarPanel(GameObject _panel)
    {
        panelMenuPrincipal.SetActive(false);
        panelMenuCreditos.SetActive(false);
        panelMenuOpciones.SetActive(false);
        _panel.SetActive(true);
    }
   
}
