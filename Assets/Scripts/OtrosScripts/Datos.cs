using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datos : MonoBehaviour
{
    public int zombieEliminados = 0;
    public int soldadoEliminados = 0;
    public int tareasCompletadas = 0;

    public static Datos instancia;

    private void Awake()
    {
        if (Datos.instancia == null)
        {
            Datos.instancia = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementarZombieEliminado()
    {
        zombieEliminados++;
        
    }

    public void IncrementarSoldadoEliminado()
    {
        soldadoEliminados++;
       
    }

    public void incrementarTareaCompletada()
    {
        tareasCompletadas++;
    }


    public void ResetMisionesCompletadas()
    {
        tareasCompletadas = 0;
    }
}
