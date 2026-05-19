using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

public class ControlEnemigo : MonoBehaviour
{
    [Header("Estadisticas")]
    public int vidaActual;
    public int vidaMaxima;
    public int puntuacionEnemigo;

    [Header("Movimiento")]
    public float velocidad;
    public float rangoAtaque;
    public float yPathOffset;
    public float distanciaPerseguir;

    public List<Vector3> listaCaminos;
    private ControlArma arma;
    public ControlJugador objetivo;
    public Boolean siemprePerseguir;

    public NavMeshAgent navMeshAgent;


    private void Awake()
    {
        arma = GetComponent<ControlArma>();
        objetivo = FindObjectOfType<ControlJugador>();
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        // llama a la funcion ActualizarCamino y cada 0.5 segundos la llama
        InvokeRepeating("ActualizarCamino", 0.0f, 0.5f);
    }

    private void Update()
    {
        // Obtengo distancia entre el enemigo y el jugador
        float distancia = Vector3.Distance(transform.position, objetivo.transform.position);

        // mira hacia el jugador
        transform.LookAt(objetivo.transform.position);

        if (distancia < distanciaPerseguir || siemprePerseguir)
        {
            // Si la distancia es menor del rango de ataque lo persigo
            if (distancia > rangoAtaque)
            {
                PerseguirObgetivo();
            }
            else
            {
                // si estoy dentro del rango de ataque instento disparar
                if (arma.PuedeDisparar())
                {
                    arma.Disparar();
                }

            }
        }

        
        
    }

    private void PerseguirObgetivo()
    {
        navMeshAgent.destination = objetivo.transform.position;            
           
    }

    private void ActualizarCamino()
    {
      
    }

    public void QuitarVida(int cantidadVidaQuitar)
    {
        if (vidaActual == 0)
        {
            ControlJuego.instancia.ActualizarPuntuacion();
            Destroy(gameObject);
        }
        else
        {
            vidaActual -= cantidadVidaQuitar;
            Debug.Log("Vida: " + vidaActual);
        }
        

    }
}
