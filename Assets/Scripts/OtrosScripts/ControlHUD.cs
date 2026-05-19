using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlHUD : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI puntuacionTexto;
    public TextMeshProUGUI numBalasTexto;
    public Image barraVidas;
    public Image barraBalas;
 
    public GameObject panelObjetivos;
    public GameObject panelCompletados;

    [Header("Ventana Pausa")]
    public GameObject ventanaPausa;
    public GameObject ventanaFinNivel;
    public GameObject ventanaGameOver;

    [Header("Ventana Fin Juego")]
   // public GameObject ventanaFinJuego;

    [Header("Extras")]
    public GameObject inmunidad;
    public GameObject SuperSalto;

    public static ControlHUD instancia;
    public GameObject txtGameOver;
    public AudioSource musica;
    public AudioSource sonidoAmbiente;
    public AudioSource musicaFinNivel;   

   
    

    [Header("Textos")]
    public TextMeshProUGUI txtSoldados;
    public TextMeshProUGUI txtZombies;
    public TextMeshProUGUI txtScoreFinNivel;

    private void Awake()
    {   
        instancia = this;
    }
    public void ActualizarBarraVida(float vidaActual, float vidaMaxima)
    {
        float cantidadVida = vidaActual / vidaMaxima;
        // Actualiza el valor de la barra de vida
        barraVidas.fillAmount = cantidadVida;
    }

    public void ActualizarPuntuacion(int puntuacion)
    {
        // Los 00000 hacen que se muestre el dato en 5 cifras
        puntuacionTexto.text = puntuacion.ToString("00000");
    }

    public void ActualizarNumBalas(float municionActual, float municionMaxima, float municionRecamara)
    {
        float cantidadBalas = municionRecamara / municionMaxima;
        barraBalas.fillAmount = cantidadBalas;


        numBalasTexto.text = municionActual.ToString();
    }

    public void CambiarEstadoVentanaPausa(bool pausa)
    {
        ventanaPausa.SetActive(pausa);
       /* switch (pausa)
        {
            case true:
                Cursor.lockState = CursorLockMode.None;
                break;
            case false:
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }*/
    }

    public void CambiarEstadoVentanaFinNivel(bool pausa)
    {
        ventanaFinNivel.SetActive(pausa);
        MuestraPuntuacion();      
    }

    public void GameOver()
    {
        ventanaGameOver.SetActive(true);
        MuestraPuntuacion();        
        
    }

    public void MostrarIconoInmunidad(bool activar)
    {
        inmunidad.SetActive(activar);
    }

    public void MostrarIconoSuperSalto(bool activar)
    {
        SuperSalto.SetActive(activar);
    }

    public void OnBotonMenu()
    {
        SceneManager.LoadScene("Menu");    
        ControlJuego.instancia.CambiarPausa();
    }

    public void OnbotonEmpezar()
    {
        SceneManager.LoadScene("Juego");
    }  

    public void PonerMusicaFinNivel()
    {
        musicaFinNivel.Play();
    }


    public void MuestraPuntuacion()
    {        
        panelObjetivos.SetActive(false);
        panelCompletados.SetActive(false);
        
        int soldadosEliminados = Datos.instancia.soldadoEliminados;
        int zombiesEliminados = Datos.instancia.zombieEliminados;
        int score = (zombiesEliminados * 15) + (soldadosEliminados * 25);

        txtSoldados.text = "Soldiers: " + soldadosEliminados.ToString();
        txtZombies.text = "Zombies: " + zombiesEliminados.ToString();

        txtScoreFinNivel.text = score.ToString();    

    }

    
}
