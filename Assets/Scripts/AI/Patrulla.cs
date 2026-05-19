using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//----------------------------------------------------------------------------------------------------------------------
// clase Patrulla que hereda de Estado --> para realizar la patrulla
//----------------------------------------------------------------------------------------------------------------------
public class Patrulla : Estado
{
    int indiceActual = -1;

    public Patrulla(GameObject _npc, NavMeshAgent _agente, Animator _anim, Transform _player)
        : base(_npc, _agente, _anim, _player)
    {
        nombre = ESTADO.PATRULLA;
        agente.speed = 2;
        agente.isStopped = false; // no para entre punto y punto
    }

    public override void Entrar()
    {
        //currentIndex = 0; // Comienza en el primer punto de la ruta

        float distanciaMenor = Mathf.Infinity;

        for (int i = 0; i < GameEnvironment.Singleton.Puntos.Count; i++)
        {
            GameObject punto = GameEnvironment.Singleton.Puntos[i];
            float distancia = Vector3.Distance(npc.transform.position, punto.transform.position);

            if (distancia < distanciaMenor)
            {
                indiceActual = i - 1; // porque en el Update esta el indiceActual++
                distanciaMenor = distancia;
            }

        }

        anim.SetBool("walk", true); // El agente comienza a caminar
        base.Entrar();
    }

    /// <summary>
    /// el agente se moverá entre los puntos que configuran la patrulla
    /// </summary>

    public override void Actualizar()
    {
        if (agente.remainingDistance < 1) // la distancia que le queda por recorrer
        {
            if (indiceActual >= GameEnvironment.Singleton.Puntos.Count - 1)
                indiceActual = 0;
            else
                indiceActual++;

            agente.SetDestination(GameEnvironment.Singleton.Puntos[indiceActual].transform.position);
        }


        if (PuedeVerJugador())
        {
            sigEstado = new Persecucion(npc, agente, anim, player);
            etapa = EVENTO.SALIR;
        }
        else if (EstaJugadorDetras())
        {
            sigEstado = new Huida(npc, agente, anim, player);
            etapa = EVENTO.SALIR;
        }

        //base.Update();
    }

    public override void Salir()
    {
        anim.SetBool("walk", false);
        base.Salir();
    }

}

