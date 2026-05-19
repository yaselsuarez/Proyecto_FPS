using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Datos.instancia.incrementarTareaCompletada();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        
    }
}
