using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreacionExtras : MonoBehaviour
{
    [SerializeField] List<GameObject> listaExtras;
    [SerializeField] float tiempoCreacionExtras;
    [SerializeField] float posicionX;
    [SerializeField] float posicionZ;
    [SerializeField] float posicionY;
    [SerializeField] float diametro;

    void Start()
    {
        InvokeRepeating("CreaExtras", 0f, tiempoCreacionExtras);
    }

    void CreaExtras()
    {
        int numAleatorio = Random.Range(0, 3);
        Instantiate(listaExtras[numAleatorio], new Vector3(Random.Range(posicionX, posicionX + diametro), posicionY, Random.Range(posicionZ, posicionZ + diametro)), Quaternion.identity);       
    }

}