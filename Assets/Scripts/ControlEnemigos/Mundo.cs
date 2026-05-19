using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Mundo
{
    private static readonly Mundo instancia = new Mundo();
    private static GameObject[] escondites;

    static Mundo()
    {
        escondites = GameObject.FindGameObjectsWithTag("oculto");
    }
    private Mundo() { }
    public static Mundo Instancia
    {
        get { return instancia; }
    }
    public GameObject[] GetEscondites()
    {
        return escondites;
    }
}
