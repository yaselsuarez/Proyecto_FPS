using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    NavMeshAgent agente;
    Animator anim;    
    Estado estadoActual;

    public Transform personaje;


    
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        personaje = GameObject.FindGameObjectWithTag("Player").transform;

        estadoActual = new Reposo(this.gameObject, agente, anim, personaje);
    }

   
    void Update()
    {
        estadoActual = estadoActual.Proceso();
    }
}
