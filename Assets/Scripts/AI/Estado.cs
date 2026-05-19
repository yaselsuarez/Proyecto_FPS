using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//----------------------------------------------------------------------------------------------------------------------
// clase Estado --> Clase base de los estados por lo que pasa la animación de un NPC - enemigo
// Sigue el patrón State
//----------------------------------------------------------------------------------------------------------------------
public class Estado 
{
    // Estados que el NPC puede tener
    public enum ESTADO
    {
        REPOSO, PATRULLA, PERSECUCION, ATAQUE, DORMIDO, HUIDA
    };

    // Fases por las que pasa un estado - que se produce en la ejecución de un estado
    public enum EVENTO
    {
        ENTRAR, ACTUALIZAR, SALIR
    };

    public ESTADO nombre;  // Nombre del estado
    protected EVENTO etapa; // evento del estado

    protected GameObject npc; // El NPC del juego
    protected Animator anim;  // Componente animator del NPC
    protected Transform player; // Transform del personaje.
    protected Estado sigEstado;
    protected NavMeshAgent agente; // El componente NavMeshAgent del NPC

    float distanciaVision = 10.0f;
    float anguloVision = 50.0f;
    float distanciaAtaque = 2.5f;


    /// <summary>
    /// Constructor de la clase Estado
    /// </summary>
    /// <param name="_npc"></param>
    /// <param name="_agente"></param>
    /// <param name="_anim"></param>
    /// <param name="_player"></param>
    public Estado( GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        agente = _agent;
        anim = _anim;
        player = _player;

        etapa = EVENTO.ENTRAR; 
    }


    // 3 método por cada una de las fases por las que pasa el estado
    public virtual void Entrar() { etapa = EVENTO.ACTUALIZAR; }
    public virtual void Actualizar() { etapa = EVENTO.ACTUALIZAR; }
    public virtual void Salir() { etapa = EVENTO.SALIR; }


    /// <summary>
    /// El método que se ejecutará desde el exterior 
    /// y hará progresar el estado a través de cada una de las diferentes etapas.
    /// </summary>
    /// <returns> Devuelve el estado en el que se encuentra el NPC</returns>
    public Estado Proceso()
    {
        if (etapa == EVENTO.ENTRAR) Entrar();
        if (etapa == EVENTO.ACTUALIZAR) Actualizar();
        if((etapa == EVENTO.SALIR))
        {
            Salir();
            return sigEstado;
        }

        return this;
    }

    /// <summary>
    /// Devuelve si el NPC ha visto al jugador según el calculo de la distancia y el ángulo de visión
    /// </summary>
    /// <returns></returns>
    public bool PuedeVerJugador()
    {
        bool visto = false;

        // Distancia entre el enemigo y el jugador
        Vector3 direccion = player.position - npc.transform.position;

        // ángulo de visión
        float angulo = Vector3.Angle(direccion, npc.transform.forward);

        if( direccion.magnitude < distanciaVision && angulo < anguloVision)
        {
            visto = true;
        }

        return visto ;
    }

    public bool PuedeAtacarJugador()
    {
        bool ataca = false;

        // Distancia entre el enemigo y el jugador
        Vector3 direccion = player.position - npc.transform.position;

        if( direccion.magnitude <= distanciaAtaque)
        {
            ataca = true;
        }

        return ataca;

    }

    public bool EstaJugadorDetras()
    {
        bool empezar = false;
        float distanciaHuida = 3f;

        Vector3 direccion = npc.transform.position - player.position;
        float angulo = Vector3.Angle(direccion, npc.transform.forward);

        if( direccion.magnitude < distanciaHuida && angulo < 30)
        {
            empezar = true;
        }

        return empezar;
    }
}