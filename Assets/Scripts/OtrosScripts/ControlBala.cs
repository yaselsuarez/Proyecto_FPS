using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBala : MonoBehaviour
{
   
    public float tiempoActivo;   
    private float tiempoDisparo;
    public GameObject particulasExplosion;
 

    private void OnEnable()
    {
        tiempoDisparo = Time.time;
    }

    private void Update()
    {
        if (Time.time - tiempoDisparo >= tiempoActivo)
        {
            gameObject.SetActive(false);       
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        
        
        // Instancia las particulas cuando choca con algo
        GameObject particulas = Instantiate(particulasExplosion, transform.position, Quaternion.identity);

        // Se destruye cuando pase la duración del sistema de particulas accediendo al tiempo de esas particulas
        Destroy(particulas, particulas.GetComponent<ParticleSystem>().main.duration);

        if (other.gameObject.CompareTag("Enemigo"))
        {          

            other.gameObject.GetComponent<ControlSoldado>().QuitarVida(5); 
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Zombie"))
        {
            other.gameObject.GetComponent<ControlZombie>().QuitarVida(5);
           
        }        

        if (other.gameObject.CompareTag("Player"))
        {         
            other.gameObject.GetComponent<ControlJugador>().QuitarVida(5);
            gameObject.SetActive(false);
        }
    }   
  
}
