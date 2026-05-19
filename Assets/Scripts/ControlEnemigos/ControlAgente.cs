using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlAgente : MonoBehaviour
{

    [Header("Referencias")]   
    private NavMeshAgent agente;
    public GameObject jugador;
    public int rutina;
    public float cronometro;
    public Animator animator;
    public Quaternion angulo;
    public float grado;
    public ControlArmaAgente arma;

    public float distanciaPerseguirAlJugador;
    public float distanciaDispararAlJugador;

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        //arma = GetComponent<ControlArmaAgente>();
    }

    private void Update()
    {
        Comportamiento_Enemigo();
    }

    public void Comportamiento_Enemigo()
    {
        // Si la distancia entre el enemigo y el jugador es mayor que 5 ejecutara lo que esta dentro del if
        // Si entra al if ejecutara el codigo para caminar aleatoriamente por el mapa
        if (Vector3.Distance(transform.position, jugador.transform.position) > distanciaPerseguirAlJugador)
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
        // Si la distancia entre el enemigo y el jugador NO es mayor que 5 ejecutara lo siguiente
        else
        {
            Debug.Log(Vector3.Distance(transform.position, jugador.transform.position));
            // Entra en el if si la distancia entre el enemigo y el jugador es mayor 
            if (Vector3.Distance(transform.position, jugador.transform.position) > distanciaDispararAlJugador ) 
            {
                var lookPos = jugador.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                // El enemigo se rota hacia la posicion del jugador con una velocidad de 2
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                animator.SetBool("walk", false);
                // Activamos la animacion de correr
                animator.SetBool("run", true);
                // Se desplaza hacia delante a una velocidad de 2
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);

                animator.SetBool("fire", false);
            }
            // Entra si esta a una distancia menor a 1
            else
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);

                animator.SetBool("fire", true);
                arma.Disparar();
                
            }
        }        
    }
}
