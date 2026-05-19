using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //Agregado ya que necesitamos usar 'OrderBy' para ordenar la secuencia de puntos

//--------------------------------------------------------------------------------------------
// patrón Singleton
//--------------------------------------------------------------------------------------------.

public sealed class GameEnvironment 
{
    // crear instancia de la clase GameEnvironment llamada "instancia"
    private static GameEnvironment instancia;

    // crear una lista de objetos llamados "Patrulla"
    private List<GameObject> puntos = new List<GameObject>() ;

    public List<GameObject> Puntos { get { return puntos;  } }

    public static GameEnvironment Singleton
    {
        get
        {
            if( instancia == null)
            {
                instancia = new GameEnvironment();
                instancia.Puntos.AddRange( GameObject.FindGameObjectsWithTag("Patrulla"));

                //Ordene los puntos en orden alfabético ascendente por nombre,
                //para que el NPC los siga correctamente
                instancia.puntos = instancia.puntos.OrderBy(waypoint => waypoint.name).ToList();
            }
            
            return instancia;
        }
    }


}
