using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CargadorNivel : MonoBehaviour
{
    public GameObject panelLoading;
    public Image Loading;
    int numEscena;

    private void Start()
    {    
        panelLoading.SetActive(false);
        numEscena = SceneManager.GetActiveScene().buildIndex + 1;

    }
    public void CargarNivel()
    {        
        StartCoroutine(CargarAsync(numEscena));
    }

    IEnumerator CargarAsync(int numeroDeEscena)
    {
        panelLoading.SetActive(true);
        AsyncOperation Operacion = SceneManager.LoadSceneAsync(numeroDeEscena);

        while (!Operacion.isDone)
        {
            float progreso = Mathf.Clamp01(Operacion.progress / .9f);
            Loading.fillAmount = progreso;
            yield return null;
        }
    }
}
