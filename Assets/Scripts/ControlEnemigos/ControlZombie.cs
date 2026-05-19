using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlZombie : MonoBehaviour
{
    [Header("Estadisticas")]
    public int vidaActual;
    public int vidaMaxima;
    public int puntuacionEnemigo;    
    //public float velocidad;
    public float distanciaPerseguir;
    public float distanciaAtacar;
    private float distanciaAlJugador;

    public NavMeshAgent agenteNavMesh;
    private Transform posicionJugador;
    public Animator animator;
    public Image barraVida;
    private CapsuleCollider capsuleCollider;

    [Header("Referencias")]
    public float cronometro;
    public int rutina;
    public float grado;
    public Quaternion angulo;

    bool estaMuerto =  false;   
    AudioSource audioSource;

    public GameObject[] extras;

    Comportamiento comportamiento;

    public Transform[] puntosDePatrulla = null;

    public bool patrulla = false;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        posicionJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>(); 
        comportamiento = GetComponent<Comportamiento>();
    }


    /* ------------ Esta es la version vieja del comportamiento ---------------
    private void Update()
    {
        barraVida.transform.LookAt(posicionJugador);
        distanciaAlJugador = Vector3.Distance(transform.position, posicionJugador.transform.position);

        audioSource.volume = 3f / distanciaAlJugador;

        if (distanciaAlJugador > distanciaPerseguir && !estaMuerto)
        {            
          
            // Crea un conometro que va subiendo de numero
            cronometro += 1 * Time.deltaTime;
            // Cuando el conometro es mayor o igual a 4 rutina es igual a un numero entre 0 y 1
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            // Con el número aleatorio guardado en rutino decidiremos lo que hará el enemigo
            switch (rutina)
            {
                case 0:
                    // Cancelamos la animacion de caminar
                    animator.SetBool("walk", false);
                    animator.SetBool("run", false);
                    animator.SetBool("attack", false);
                    break;
                case 1:
                    // Determinamos la direccion en la que se desplazará
                    grado = Random.Range(0, 360);
                    // Angulo tendra el valor de grado en el eje Y
                    angulo = Quaternion.Euler(0, grado, 0);
                    // Rutina valdra 2 para que entre en el case 2
                    rutina++;
                    break;
                case 2:
                    // La rotacion del enemigo será igual al angulo establecido
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    // Se movera hacia delante con una velocidad de 1
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    // Activaremos la animación de caminar
                    animator.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if (!estaMuerto)
            {
                // Persigue al jugador
                agenteNavMesh.SetDestination(posicionJugador.transform.position);
                animator.SetBool("attack", false);
                animator.SetBool("walk", false);
                animator.SetBool("run", true);

                // Deja de perseguir a la distancia de atacar
                agenteNavMesh.stoppingDistance = distanciaAtacar;
                if (distanciaAlJugador <= distanciaAtacar + 1)
                {
                    animator.SetBool("run", false);
                    animator.SetBool("attack", true);                    
                }
            }
            
        }
    }
*/

    private void Update()
    {
        // La barra de vida es siempre visible
        barraVida.transform.LookAt(posicionJugador);
        audioSource.volume = 3f / distanciaAlJugador;

        distanciaAlJugador = Vector3.Distance(transform.position, posicionJugador.transform.position);

        if (distanciaAlJugador > distanciaPerseguir && !estaMuerto)
        {
            animacion("walk");
            if (patrulla)
            {
                try
                {
                    comportamiento.Patrulla(2f, puntosDePatrulla);
                }
                catch (System.Exception)
                {

                  
                }
                
            }
            /*else
            {                
                comportamiento.Deambular2();
            }            
           */
        }
        else
        {
            if (comportamiento.PuedesVerObjetivo())
            {
                comportamiento.Persecucion_3(6f, estaMuerto);
                animacion("run");
                if (distanciaAlJugador <= distanciaAtacar + 1)
                {
                    agenteNavMesh.stoppingDistance = distanciaAtacar;
                    animacion("attack");
                }
                else
                {
                    animacion("run");
                }
            }
        }
       
    }

    public void QuitarVida(int cantidadVidaQuitar)
    {
        
        if (vidaActual == 0)
        {
            int numAlea = Random.Range(0, 3);
            Instantiate(extras[numAlea], transform.position, Quaternion.identity);
            
            ControlJuego.instancia.ActualizarPuntuacion();
            Datos.instancia.IncrementarZombieEliminado();
            agenteNavMesh.enabled = false;            
            capsuleCollider.enabled = false;            
            animator.SetTrigger("die");
            Destroy(gameObject, 5f);
            estaMuerto = true;
        }
        else
        {
            vidaActual -= cantidadVidaQuitar; 
            float datosBarraVida = (float)vidaActual / (float)vidaMaxima;
            barraVida.fillAmount = datosBarraVida;
        }
    }

    void animacion(string _accion)
    {
        animator.SetBool("walk", false);
        animator.SetBool("run", false);
        animator.SetBool("attack", false);

        animator.SetBool(_accion, true);
    }
   
    public void PasarPuntosPatrulla(Transform[] _puntosPatrulla)
    {
        puntosDePatrulla = _puntosPatrulla; 
    }

    public void ActivarPatrulla(bool _activar)
    {
        patrulla = _activar;
    }


}
