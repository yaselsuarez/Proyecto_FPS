using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//----------------------------------------------------------------------------------------------------------------------
// clase Huida (RunAway) que hereda de State --> para realizar el ataque 
//-----------------------------------------------------------------------------------------------------------------------

public class Huida : Estado
{
    GameObject casaSegura;

    public Huida(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
        : base(_npc, _agent, _anim, _player)
    {

        nombre = ESTADO.HUIDA;
        casaSegura = GameObject.FindGameObjectWithTag("Seguro");
    }

    public override void Entrar()
    {
        anim.SetTrigger("isRunning");
        agente.isStopped = false;
        agente.speed = 6;
        agente.SetDestination(casaSegura.transform.position);

        base.Entrar();
    }

    public override void Actualizar()
    {
        if (agente.remainingDistance < 1)
        {
            sigEstado = new Reposo(npc, agente, anim, player);
            etapa = EVENTO.SALIR;
        }
    }

    public override void Salir()
    {
        anim.ResetTrigger("isRunning");
        base.Salir();
    }
}