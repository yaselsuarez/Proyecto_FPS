using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrullaY : MonoBehaviour
{
    public float velocidadPatrullaje = 1f;  
    public Transform[] puntosDePatrulla;
    public NavMeshAgent agente;
    public float distancia;

    int objetivoActual = 0;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        Patrulla();
    }

    public void Patrulla()
    {
        agente.speed = velocidadPatrullaje;
        agente.SetDestination(puntosDePatrulla[objetivoActual].transform.position);
        distancia = Vector3.Distance(puntosDePatrulla[objetivoActual].transform.position, transform.position);
        if (distancia <= 3)
        {
            objetivoActual++;
            if (objetivoActual >= puntosDePatrulla.Length)
            {
                objetivoActual = 0;
            }
        }
    }
}