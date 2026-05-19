using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlSoldado : MonoBehaviour
{
    [Header("Estadisticas")]
    public int vidaActual;
    public int vidaMaxima;
    public int puntuacionEnemigo;

    public NavMeshAgent agenteNavMesh;
    public Transform posicionJugador;
    public float distanciaAlJugador;
    public Animator animator;
    public float distanciaPerseguir;
    public float distanciaDisparar;
    public ControlArma arma; 
    public float velocidad;

    public Image barraVida;
    private CapsuleCollider capsuleCollider;
    private BoxCollider boxCollider;

    // Este mismo GameObject (el enemigo que nos mira)
    public GameObject objetoQueMira;
    // El GameObject del Jugador que somos nosotros(Player)
    public GameObject objetoAMirar;    
    // Con esta variable indicaremos a que posicion en y debe mirar el enemigo
    public float yPos;

    private Vector3 objetoAMirarPosicion;

    bool estaMuerto = false;

    [Header("Referencias")]
    public float cronometro;
    public int rutina;
    public float grado;
    public Quaternion angulo;

    public GameObject[] extras;

    private void Awake()
    {
        posicionJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        boxCollider = GetComponent<BoxCollider>();
    }


    private void Update()
    {
        barraVida.transform.LookAt(posicionJugador);
        // Calcula la distancia entre el enemigo y el jugador
        distanciaAlJugador = Vector3.Distance(transform.position, posicionJugador.transform.position);
        animator.SetBool("walk", true);

        if (Vector3.Distance(transform.position, posicionJugador.transform.position) >= distanciaPerseguir)
        {
            animator.SetBool("run", false);
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
        else //if (distanciaAlJugador <= distanciaPerseguir )
        {
            if (!estaMuerto)
            {
                accion("run");
                agenteNavMesh.SetDestination(posicionJugador.transform.position);
            }
            

            // Persigue al objetivo
            transform.position = Vector3.MoveTowards(transform.position, posicionJugador.transform.position, velocidad * Time.deltaTime); ;

            if (distanciaAlJugador <= distanciaDisparar && !estaMuerto)
            {
                animator.SetBool("run", false);
                agenteNavMesh.SetDestination(transform.position);
               
                    // si estoy dentro del rango de ataque instento disparar
                    if (arma.PuedeDisparar())
                    {
                        // mira siempre hacia el jugador            
                        objetoAMirarPosicion = posicionJugador.transform.position;
                        yPos = posicionJugador.transform.position.y;

                        objetoAMirarPosicion.y = yPos - 3;
                        objetoQueMira.transform.LookAt(objetoAMirarPosicion);
                        if (vidaActual > 0)
                        {
                            animator.SetBool("fire", true);
                            arma.Disparar();
                        }                                      
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
            Datos.instancia.IncrementarSoldadoEliminado();
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

    private void accion(string accion)
    {
        animator.SetBool("walk", false);
        animator.SetBool("run", false);
        animator.SetBool("fire", false);

        animator.SetBool(accion, true);
    }
}
