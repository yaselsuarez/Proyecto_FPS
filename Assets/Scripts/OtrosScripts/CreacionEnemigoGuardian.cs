using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreacionEnemigoGuardian : MonoBehaviour
{
    [SerializeField] Transform posicionEnemigo;
    [SerializeField] GameObject enemigoGuardian;
    [SerializeField] float tiempoAparecer = 0f;
    [SerializeField] Transform[] puntosDePatrulla;
    GameObject prefab;
    [SerializeField] bool Patrullando = false;
  

    private void Start()
    {
        Invoke("CreaEnemigoGuardian", tiempoAparecer);
    }

    void CreaEnemigoGuardian()
    {
        prefab = Instantiate(enemigoGuardian, posicionEnemigo);

        prefab.GetComponent<ControlZombie>().PasarPuntosPatrulla(puntosDePatrulla);

        prefab.GetComponent<ControlZombie>().ActivarPatrulla(Patrullando);
        
    }
}
