using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//----------------------------------------------------------------------------------------------------------------------
// clase Persecucion (Pursue) que hereda de State --> para realizar la persecución 
//-----------------------------------------------------------------------------------------------------------------------
public class Persecucion : Estado
{
    public Persecucion(GameObject _npc, NavMeshAgent _agente, Animator _anim, Transform _player)
        : base(_npc, _agente, _anim, _player)
    {
        nombre = ESTADO.PERSECUCION;
        agente.speed = 5;
        agente.isStopped = false;
    }

    public override void Entrar()
    {
        anim.SetBool("run", true);
        base.Entrar();
    }

    public override void Actualizar()
    {
        //base.Update();
        agente.SetDestination(player.position);

        if (agente.hasPath) // ¿El agente tiene actualmente una ruta? (Solo lectura) 
        {
            if (PuedeAtacarJugador())
            {
                sigEstado = new Ataque(npc, agente, anim, player);
                etapa = EVENTO.SALIR;
            }
            else if (!PuedeVerJugador())
            {
                sigEstado = new Patrulla(npc, agente, anim, player);
                etapa = EVENTO.SALIR;
            }
        }
    }

    public override void Salir()
    {
        anim.SetBool("run", false);
        base.Salir();
    }
}


