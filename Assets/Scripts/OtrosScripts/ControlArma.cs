using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlArma : MonoBehaviour
{
    private PoolObjetos bolaPool;
    public Transform puntoSalida;

    public int municionActual;
    public int municionMaxima;
    public int municionRecamara;
    public bool municionInfinita;

    public float velocidadBola;

    public float frecuenciaDisparo;
    private float ultimoTiempoDisparo;
    private bool esJugador;

    public AudioSource controlSonido;
    public AudioClip disparo;


    [Header("Sounds $ Visuals")]
    public GameObject flashEffect;


    private void Awake()
    {
        bolaPool = GetComponent<PoolObjetos>();

        //Soy el jugador
        if (GetComponent<ControlJugador>())
            esJugador = true;
    }


    private void Start()
    {
        
        if (esJugador)
        {
            ControlHUD.instancia.ActualizarNumBalas(municionActual, municionMaxima, municionRecamara);
            ControlHUD.instancia.ActualizarPuntuacion(0);
        }
    }

    public bool PuedeDisparar()
    {
        if (Time.time - ultimoTiempoDisparo >= frecuenciaDisparo)
        {
            if (municionRecamara > 0 || municionInfinita)
            {
                return true;
            }
        }
        return false;
    }

   

    public void Disparar()
    {
        GameObject flashclone = Instantiate(flashEffect, puntoSalida.position, puntoSalida.rotation);
        Destroy(flashclone, 1f);   

        ultimoTiempoDisparo = Time.time;
        municionRecamara--;       

        GameObject bola = bolaPool.GetObjeto();

        bola.transform.position = puntoSalida.position;
        bola.transform.rotation = puntoSalida.rotation;

        bola.GetComponent<Rigidbody>().velocity = puntoSalida.forward * velocidadBola;

        if (esJugador)
        {
            if (municionInfinita)
            {
                ControlHUD.instancia.numBalasTexto.text = "Munción Infinita";
            }
            else
            {
                ControlHUD.instancia.ActualizarNumBalas(municionActual, municionMaxima, municionRecamara);
            }
        }

        if (municionRecamara <= 0)
        {
            municionRecamara = 0;
        }

        // Sonido del disparo
        controlSonido.PlayOneShot(disparo);        
    }

    public void IncrementarNumBalas(int cantidadIncrementar)
    {
        municionActual += cantidadIncrementar;

        if (municionActual >= municionMaxima)
        {
            municionActual = municionMaxima;
        }
        ControlHUD.instancia.ActualizarNumBalas(municionActual, municionMaxima, municionRecamara);       
    }

    public bool PuedeRecargar()
    {
        if (municionActual > 0)
        {
            return true;
        }
        else
        {
            return false;
        }        
    }


    public void Recargar()
    {
        municionRecamara += municionActual;
        municionActual = 0;

        if (municionRecamara <= 0)
        {
            municionRecamara = 0;
        }
        ControlHUD.instancia.ActualizarNumBalas(municionActual, municionMaxima, municionRecamara);
    }


}
