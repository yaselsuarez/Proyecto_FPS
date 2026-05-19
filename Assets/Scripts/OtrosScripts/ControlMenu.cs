using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ControlMenu : MonoBehaviour
{
    [SerializeField] GameObject panelCredits;
    [SerializeField] GameObject panelMenuPrincipal;
    int numEscena;

    private void Start()
    {
        numEscena = SceneManager.GetActiveScene().buildIndex;
    }

    public void OnMenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }


    public void OnStart()
    {
        SceneManager.LoadScene(numEscena);
    }

    public void OnCredits()
    {
        ActivarPanel(panelCredits);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnVolverMenuPrincipal()
    {
        ActivarPanel(panelMenuPrincipal);
    }

    public void OnContinue()
    {
        
    }

    void ActivarPanel(GameObject panel)
    {
        panelMenuPrincipal.SetActive(false);
        panelCredits.SetActive(false);

        panel.SetActive(true);
    }

    public void NextLevel()
    {
        Datos.instancia.ResetMisionesCompletadas();
        SceneManager.LoadScene(numEscena + 1);
        
    }

}
