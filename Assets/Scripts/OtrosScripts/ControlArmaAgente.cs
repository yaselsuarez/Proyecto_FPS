using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlArmaAgente : MonoBehaviour
{
    private PoolObjetos bolaPool;
    public Transform puntoSalida;

    public int municionActual;
    public int municionMax;
    public bool municionInfinita;

    public float velocidadBola;

    public float frecuenciaDisparo;
    private float ultimoTiempoDisparo;
    private bool esJugador;
    private float municionRecamara;

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
            ControlHUD.instancia.ActualizarNumBalas(municionActual, municionMax, municionRecamara);
            ControlHUD.instancia.ActualizarPuntuacion(0);
        }
    }
    public bool PuedeDisparar()
    {
        if (Time.time - ultimoTiempoDisparo >= frecuenciaDisparo)
        {
            //if (municionActual > 0 || municionInfinita == true)
            if (municionActual > 0 || municionInfinita)
            {
                return true;
            }

        }

        return false;
    }

    public void Disparar()
    {
        ultimoTiempoDisparo = Time.time;
        municionActual--;

        //GameObject bola = Instantiate(bolaPrefab, puntoSalida.position, puntoSalida.rotation);

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
                ControlHUD.instancia.ActualizarNumBalas(municionActual, municionMax, municionRecamara);
            }
        }

    }
}
