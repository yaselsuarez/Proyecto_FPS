using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// ladrón persigue al ladrón
/// </summary>
public class Comportamiento : MonoBehaviour
{
    private GameObject objetivo;
    NavMeshAgent agente;
    Animator animator;

    Drive ds;
    Vector3 objetivoDeambular = Vector3.zero;  

    private void Awake()
    {
     
    }

    void Start()
    {
        agente = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        objetivo = GameObject.FindGameObjectWithTag("Player");
    }

    void Busqueda(Vector3 _localizacion)
    {
        agente.SetDestination(_localizacion);
    }

    void Huida(Vector3 _localización)
    {
        Vector3 vectorHuida = _localización - this.transform.position;
        agente.SetDestination(this.transform.position - vectorHuida);
    }

    public void Persecucion_1()
    {
        Vector3 direccionObjetivo = objetivo.transform.position - this.transform.position;

        float haciaDondeVa = direccionObjetivo.magnitude /
            (agente.speed + objetivo.GetComponent<Drive>().currentSpeed);

        Busqueda(objetivo.transform.position + objetivo.transform.forward * haciaDondeVa);
    }

    public void Persecucion_2()
    {
        Vector3 direccionObjetivo = objetivo.transform.position - this.transform.position;

        float anguloDondeVa = Vector3.Angle(this.transform.forward,
                                             this.transform.TransformVector(objetivo.transform.forward));

        float anguloObjetivo = Vector3.Angle(this.transform.forward,
                                              this.transform.TransformVector(direccionObjetivo));

        if ((anguloObjetivo > 90 && anguloDondeVa < 20) || objetivo.GetComponent<Drive>().currentSpeed < 0.01f)
        {
            Busqueda(objetivo.transform.position);
            return;
        }


        float haciaDondeVa = direccionObjetivo.magnitude /
            (agente.speed + objetivo.GetComponent<Drive>().currentSpeed);

        Busqueda(objetivo.transform.position + objetivo.transform.forward * haciaDondeVa);
    }

    public void Persecucion_3(float _velocidad, bool estaMuerto)
    {
        if (!estaMuerto)
        {
            agente.SetDestination(objetivo.transform.position);
            agente.speed = _velocidad;
        }
        
    }


    //-----------------------------------------------------------------------------------------
    void Evadir()
    {
        Vector3 direccionObjetivo = objetivo.transform.position - this.transform.position;

        float haciaDondeVa = direccionObjetivo.magnitude /
            (agente.speed + objetivo.GetComponent<Drive>().currentSpeed);

        Huida(objetivo.transform.position + objetivo.transform.forward * haciaDondeVa);
    }



    //-----------------------------------------------------------------------------------------
    public void Deambular(float _velocidad)
    {
        float radioDeambular = 10.0f;
        float distanciaDeambular = 20.0f;
        float deambularNervioso = 1.0f;


        objetivoDeambular += new Vector3(Random.Range(-1.0f, 1.0f) * deambularNervioso,0
            ,Random.Range(-1.0f, 1.0f) * deambularNervioso);

       objetivoDeambular.Normalize();
       objetivoDeambular *= radioDeambular;

       Vector3 objetivoLocal = objetivoDeambular + new Vector3(0, 0, distanciaDeambular);
       Vector3 objetivoGlobal = this.gameObject.transform.InverseTransformVector(objetivoLocal);

       agente.speed = _velocidad;
       Busqueda(objetivoGlobal);
    }

    //-----------------------------------------------------------------------------------------
    public float cronometro;
    public int rutina;
    public float grado;
    public Quaternion angulo;

    public void Deambular2()
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

    //-----------------------------------------------------------------------------------------
    void Ocultar()
    {
        float distancia = Mathf.Infinity;
        Vector3 elegirLugar = Vector3.zero;

        for (int i = 0; i < Mundo.Instancia.GetEscondites().Length; i++)
        {
            Vector3 ocultaDir = Mundo.Instancia.GetEscondites()[i].transform.position - objetivo.transform.position;
            Vector3 ocultaPos = Mundo.Instancia.GetEscondites()[i].transform.position + ocultaDir.normalized * 5;
            // si hay mas arboles poner 10, para que encuentre mas arboles

            if (Vector3.Distance(this.transform.position, ocultaPos) < distancia)
            {
                elegirLugar = ocultaPos;
                distancia = Vector3.Distance(this.transform.position, ocultaPos);
            }
        }

        Busqueda(elegirLugar);

    }

    //-----------------------------------------------------------------------------------------    
    void OcultacionInteligente()
    {
        float distancia = Mathf.Infinity;
        Vector3 elegirLugar = Vector3.zero;

        Vector3 elegirDireccion = Vector3.zero;
        GameObject elegirGO = Mundo.Instancia.GetEscondites()[0];


        for (int i = 0; i < Mundo.Instancia.GetEscondites().Length; i++)
        {
            Vector3 ocultaDir = Mundo.Instancia.GetEscondites()[i].transform.position - objetivo.transform.position;
            Vector3 ocultaPos = Mundo.Instancia.GetEscondites()[i].transform.position + ocultaDir.normalized * 10;
            // si hay mas arboles poner 10, para que encuentre mas arboles

            if (Vector3.Distance(this.transform.position, ocultaPos) < distancia)
            {
                elegirLugar = ocultaPos;

                elegirDireccion = ocultaDir;
                elegirGO = Mundo.Instancia.GetEscondites()[i];

                distancia = Vector3.Distance(this.transform.position, ocultaPos);
            }
        }

        Collider ocultaCol = elegirGO.GetComponent<Collider>();
        Ray rayo = new Ray(elegirLugar, -elegirDireccion.normalized);
        RaycastHit info;
        float alcance = 250.0f;

        ocultaCol.Raycast(rayo, out info, alcance);


        Busqueda(info.point + elegirDireccion.normalized * 2);

    }

    //-----------------------------------------------------------------------------------------
    public bool PuedesVerObjetivo()
    {
        bool darObjetivo = false;

        RaycastHit rayoInfo;
        Vector3 rayoAObjetivo = objetivo.transform.position - this.transform.position;

        if (Physics.Raycast(this.transform.position, rayoAObjetivo, out rayoInfo))
        {
            if (rayoInfo.transform.gameObject.tag == "Player")
            {
                darObjetivo = true;
            }
        }

        return darObjetivo;

    }

    //-----------------------------------------------------------------------------------------
    public bool ObjetivoPuedeVerme()
    {
        bool puedeVerme = false;

        // Vector que uno al angente y al objetivo
        Vector3 vectorAgente = this.transform.position - objetivo.transform.position;

        // el angulo que forma el vector anterior con la mirada hacia el frente del objetivo
        float angulo = Vector3.Angle(objetivo.transform.forward, vectorAgente);

        if (angulo < 60)
        {
            puedeVerme = true;
        }

        return puedeVerme;
    }

    // Update is called once per frame


    //-----------------------------------------------------------------------------------------

    float distancia;
    int objetivoActual = 0;

    public void Patrulla(float velocidadPatrullaje, Transform[] puntosDePatrulla)
    {
        

        agente.speed = velocidadPatrullaje;
        agente.SetDestination(puntosDePatrulla[objetivoActual].transform.position);
        distancia = Vector3.Distance(puntosDePatrulla[objetivoActual].transform.position, transform.position);       

        if (distancia <= 3)
        {
            objetivoActual++;
            if (objetivoActual >= puntosDePatrulla.Length)
            {
                objetivoActual = 0;
            }
        }
    }

    //-----------------------------------------------------------------------------------------
    void Update()
    {
        /* if (PuedesVerObjetivo() && ObjetivoPuedeVerme())
             OcultacionInteligente();*/

    }
}
