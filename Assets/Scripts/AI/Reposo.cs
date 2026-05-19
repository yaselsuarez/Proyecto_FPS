using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//----------------------------------------------------------------------------------------------------------------------
// clase Reposo (Idle) que hereda de Estado --> Posición inicial del NPC
//----------------------------------------------------------------------------------------------------------------------
public class Reposo : Estado
{
    public Reposo(GameObject _npc, NavMeshAgent _agente, Animator _anim, Transform _player)
        : base(_npc, _agente, _anim, _player)
    {
        nombre = ESTADO.REPOSO;
    }

    public override void Entrar()
    {
        anim.SetBool("idle", true);
        base.Entrar();
    }

    public override void Actualizar()
    {
        if (PuedeVerJugador())
        {
            sigEstado = new Persecucion(npc, agente, anim, player);
            etapa = EVENTO.ENTRAR;
        }

        // 10% de probabilidad en estado de inactividad
        else if (Random.Range(1, 500) < 10)
        {
            sigEstado = new Patrulla(npc, agente, anim, player);
            etapa = EVENTO.SALIR;
        }
        // base.Update(); --> IMPORTANTE comentar para que no se quede en el estado de Idle eternamente
    }

    public override void Salir()
    {
        anim.SetBool("idle", false);
        base.Salir();
    }
}

