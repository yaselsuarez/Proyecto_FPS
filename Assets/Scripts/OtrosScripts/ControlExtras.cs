using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlExtras : MonoBehaviour
{
    public TipoExtra tipo;
    public int cantidadIncrementar;
    public AudioSource audioSource;
    public AudioClip sonidoPowerUp;    
    private bool reproduciendoSonido = false;
    
    private void Update()
    {
        if (!audioSource.isPlaying && reproduciendoSonido)
        {
            Destroy(gameObject);
        }
    }           
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (tipo)
            {
                case TipoExtra.Vida:
                    // Llamo a método de jugador para incrementar la vida
                    other.GetComponent<ControlJugador>().IncrementarVida(cantidadIncrementar);
                    break;
                case TipoExtra.Bala:
                    // Llamo a método de jugador para incrementar las bolas
                    other.GetComponent<ControlArma>().IncrementarNumBalas(cantidadIncrementar);
                    break;
                case TipoExtra.Inmunidad:
                    // Activa inmunidad
                    other.GetComponent<ControlJugador>().ActivaInmunidad();
                    break;
                case TipoExtra.Salto:
                    // Super salto por un tiempo determinado
                    other.GetComponent<ControlJugador>().SuperSalto();
                    break;
            }
            audioSource.PlayOneShot(sonidoPowerUp);
            reproduciendoSonido = true;
        }
        
        
    }

    
}

public enum TipoExtra
{
    Vida,
    Bala,
    Inmunidad,
    Salto,
    Velocidad
}


