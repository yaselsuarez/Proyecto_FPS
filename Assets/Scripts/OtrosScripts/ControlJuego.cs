using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlJuego : MonoBehaviour
{
    public int puntuacionParaGanar;
    public int puntuacionActual;
    public bool juegoPausado;
    public bool juegoFinalizado;
    public static ControlJuego instancia;
    bool nivelCompletado = false;

    int targets;

    public AudioSource[] sonidosTodoElJuego;

    private void Awake()
    {
        
        
        targets = GameObject.FindGameObjectsWithTag("Target").Length;

        if (ControlJuego.instancia == null)
        {
            ControlJuego.instancia = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        juegoPausado = false;
    }

    private void Update()
    {
        int misionesCompletas = Datos.instancia.tareasCompletadas;

        if (Input.GetButtonDown("Cancel") && !nivelCompletado)
        {
            CambiarPausa();
        }

        if (misionesCompletas == 2 && !nivelCompletado)
        {
            nivelCompletado = true;
            CambiarFinNivel();
        }
        
    }

    public void CambiarPausa()
    {
        juegoPausado = !juegoPausado;
        Time.timeScale = juegoPausado ? 0.0f : 1.0f;    
        ControlHUD.instancia.CambiarEstadoVentanaPausa(juegoPausado);
        switch (juegoPausado)
        {
            case true:
                Cursor.lockState = CursorLockMode.None;
                break;
            case false:
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
        
    }

    public void CambiarFinNivel()
    {
        juegoPausado = true;
        Time.timeScale = juegoPausado ? 0.0f : 1.0f;

        ControlHUD.instancia.CambiarEstadoVentanaFinNivel(juegoPausado);

        switch (juegoPausado)
        {
            case true:
                Cursor.lockState = CursorLockMode.None;
                break;
            case false:
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }

        sonidosTodoElJuego = GameObject.FindObjectsOfType<AudioSource>();

        for (int i = 0; i < sonidosTodoElJuego.Length; i++)
        {           
               sonidosTodoElJuego[i].Stop();
        }

        ControlHUD.instancia.PonerMusicaFinNivel();
    }

    public void GameOver()
    {
        juegoPausado = true;
        Time.timeScale = juegoPausado ? 0.0f : 1.0f;

        ControlHUD.instancia.GameOver();

        switch (juegoPausado)
        {
            case true:
                Cursor.lockState = CursorLockMode.None;
                break;
            case false:
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }

        sonidosTodoElJuego = GameObject.FindObjectsOfType<AudioSource>();

        for (int i = 0; i < sonidosTodoElJuego.Length; i++)
        {
            sonidosTodoElJuego[i].Stop();
        }

        ControlHUD.instancia.PonerMusicaFinNivel();
    }

    public void PonerPuntuacion(int puntuacion)
    {
        puntuacionActual += puntuacion;
        ControlHUD.instancia.ActualizarPuntuacion(puntuacionActual);
        if (puntuacionActual >= puntuacionParaGanar)
        {
            //TerminarJuego(true);
        }
    }

    public void ActualizarPuntuacion()
    {
        puntuacionActual += 1;
        ControlHUD.instancia.ActualizarPuntuacion(puntuacionActual);
    }

    public void Continue()
    {
        CambiarPausa();
    }
    
}
