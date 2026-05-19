using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour

{
    [Header("References")]
    public float tiempoInmunidad;
    public float tiempoSuperSalto;

    [Header("Estadisticas")]
    public int vidaActual;
    public int vidaMaxima;
   

    [Header("Movimiento")]
    public float velocidad;
    public float fuerzaSalto;

    [Header("Camara")]
    public float sensibilidadRaton;
    public float maxVistaX;
    public float minVistaX;
    private float rotacionX;

    Camera camara;
    Rigidbody fisica;
    ControlArma arma;
    Animator animator;
    BoxCollider boxCollider;
    float guardaFuerzaSalto;
    public bool inmunidad = false;
    private bool recargando = false;
    private bool sonandoRecarga = false;
    public AudioClip recargaFX;
    AudioSource audioSource;

    // Variables para comprobar si estoy en el suelo
    public LayerMask groundMask;

    


    private void Awake()
    {
        camara = Camera.main;
        fisica = gameObject.GetComponent<Rigidbody>();
        arma = gameObject.GetComponent<ControlArma>();
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
       

        
    }

    private void Start()
    {
        // Bloquea el mouse para que no se salga de la pantalla de juego
        Cursor.lockState = CursorLockMode.Locked;
        ControlHUD.instancia.ActualizarBarraVida(vidaActual, vidaMaxima);
    }

    private void Update()
    {

        // Si el juego esta pausado se sale del update por lo tanto nada todo se para
        if (ControlJuego.instancia.juegoPausado)
        {
            return;
        }

        //Generemos un nuevo método para el movimiento
        Movimiento();

        //Rotacion de la camara con el jugados
        VistaCamara();

        if (Input.GetButtonDown("Jump")) {
            Salto();
        }

        // Dispara si presionamos el boton Fire1 (Botón izquierdo del mouse)
        if (Input.GetButton("Fire1"))
        {
            if (arma.PuedeDisparar() && !recargando && !sonandoRecarga)
            {
                animator.SetBool("fire", true);
                arma.Disparar();
            }
        }            

        if (Input.GetButtonUp("Fire1"))
        {
                animator.SetBool("fire", false);
        }

        if (Input.GetKeyDown(KeyCode.R) )
        {       
            if (arma.PuedeRecargar())
            {
                StartCoroutine(ICargaArma());
            }
                       
        }
       
    }

    private void Movimiento()
    {
        float x = Input.GetAxisRaw("Horizontal") * velocidad;
        float z = Input.GetAxisRaw("Vertical") * velocidad;

        Vector3 direccion = transform.right * x + transform.forward * z;

        fisica.velocity = new Vector3(direccion.x, fisica.velocity.y, direccion.z); ;

        //fisica.velocity = new Vector3(x, fisica.velocity.y, z);

        if (Input.GetKey(KeyCode.LeftShift))
        {           
            velocidad = 12;
        }
        else 
        { 
            velocidad = 6; 
        }    
    }

    private void VistaCamara()
    {
        float y = Input.GetAxis("Mouse X") * sensibilidadRaton;
        rotacionX += Input.GetAxis("Mouse Y") * sensibilidadRaton;

        rotacionX = Mathf.Clamp(rotacionX, minVistaX, maxVistaX);

        camara.transform.localRotation = Quaternion.Euler(-rotacionX, 0, 0);

        transform.eulerAngles += Vector3.up * y;

    }

    private void Salto()
    {
        Ray rayo = new Ray(transform.position, Vector3.down);       

        if (Physics.Raycast(rayo,  2))
        {
            fisica.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }        
    }

    public void QuitarVida(int cantidadVidaQuitar)
    {
        if (!inmunidad)
        {
            vidaActual -= cantidadVidaQuitar;
            
            if (vidaActual <= 0)
            {
                vidaActual = 0;
                ControlJuego.instancia.GameOver();
                ControlHUD.instancia.MuestraPuntuacion();
                return;
            }
        }        
        ControlHUD.instancia.ActualizarBarraVida(vidaActual, vidaMaxima);
    }

    public void IncrementarVida(int cantidadVida)
    {
        vidaActual += cantidadVida;

        if (vidaActual >= vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }        
        ControlHUD.instancia.ActualizarBarraVida(vidaActual, vidaMaxima);
    }       

    public void ActivaInmunidad()
    {
        inmunidad = true;
        ControlHUD.instancia.MostrarIconoInmunidad(true);
        StartCoroutine(DesactivaInmunidad(tiempoInmunidad));
    }

    IEnumerator DesactivaInmunidad(float tiempo)
    {        
        yield return new WaitForSecondsRealtime(tiempo);
        inmunidad = false;
        ControlHUD.instancia.MostrarIconoInmunidad(false);
    }

    public void SuperSalto()
    {
        // Guarda la fuerza de salto en una variable temporal
        guardaFuerzaSalto = fuerzaSalto;
        // Incrementa la fuerza de salto
        fuerzaSalto += 10;
        // Muestra el icono de super salto
        ControlHUD.instancia.MostrarIconoSuperSalto(true);
        // Desactiva el icono se super salto despues del tiempo indicado
        StartCoroutine(ISuperSalto(tiempoSuperSalto));        
    }

    IEnumerator ISuperSalto(float tiempo)
    {
        yield return new WaitForSecondsRealtime(tiempo);
        fuerzaSalto = guardaFuerzaSalto;
        ControlHUD.instancia.MostrarIconoSuperSalto(false);
    }

    IEnumerator ICargaArma()
    {
        if (!audioSource.isPlaying && !sonandoRecarga)
        {
            audioSource.PlayOneShot(recargaFX, 4f);
        }
        
        animator.SetBool("reload", true);
        recargando = true;
        sonandoRecarga = true;
        yield return new WaitForSeconds(2.02f);
        arma.Recargar();
        animator.SetBool("reload", false);
        recargando = false;
        sonandoRecarga = false;
    }
}
