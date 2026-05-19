using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreacionEnemigos : MonoBehaviour
{
    [SerializeField] List<GameObject> listaPuntosCreacionEnemigos;  
    [SerializeField] GameObject enemigo;
    [SerializeField] float tiempoCreacionEnemigos;
    int numAleatorio;

    private void Start()
    {
        InvokeRepeating("CreaEnemigos",  1f, tiempoCreacionEnemigos);
    }
    void CreaEnemigos()
    {
        numAleatorio = (int)Random.Range(0f, listaPuntosCreacionEnemigos.Count);
        Instantiate(enemigo, listaPuntosCreacionEnemigos[numAleatorio].transform.position, Quaternion.identity);
      
    }


}
