using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//----------------------------------------------------------------------------------------------------------------------
// clase Ataque (Attack) que hereda de Estado --> para realizar el ataque 
//-----------------------------------------------------------------------------------------------------------------------

public class Ataque : Estado
{
    float velocidadRotacion = 2.0f;
    AudioSource audioDisparo;


    public Ataque(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
        : base(_npc, _agent, _anim, _player)
    {
        nombre = ESTADO.ATAQUE;
        audioDisparo = _npc.GetComponent<AudioSource>();

    }

    public override void Entrar()
    {
        anim.SetBool("attack", true);
        agente.isStopped = true; // el enemigo lo paramos para que golpee
        //audioDisparo.Play();
        base.Entrar();
    }

    public override void Actualizar()
    {
        //base.Update();
        Vector3 direccion = player.position - npc.transform.position;
        float angulo = Vector3.Angle(direccion, npc.transform.forward);
        direccion.y = 0;

        // npc rota hacia el player
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                         Quaternion.LookRotation(direccion), Time.deltaTime * velocidadRotacion);

        if (!PuedeAtacarJugador())
        {
            sigEstado = new Reposo(npc, agente, anim, player);
            etapa = EVENTO.SALIR;
        }
    }

    public override void Salir()
    {
        anim.SetBool("attack", false);
        audioDisparo.Stop();
        base.Salir();
    }
}
