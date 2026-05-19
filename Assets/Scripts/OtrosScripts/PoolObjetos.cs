using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjetos : MonoBehaviour
{
    public GameObject objetoPrefab;
    public int numObjetosOnStart;

    private List<GameObject> objetosPoled = new List<GameObject>();

    private void Start()
    {
        for (int x = 0; x < numObjetosOnStart; x++)
        {
            CrearNuevoObjeto();
        }
    }

    private GameObject CrearNuevoObjeto()
    {
        GameObject objeto = Instantiate(objetoPrefab);
        objeto.SetActive(false);
        objetosPoled.Add(objeto);

        return objeto;
    }

    public GameObject GetObjeto()
    {
        GameObject objeto = objetosPoled.Find(x => x.activeInHierarchy == false);

        if (objeto == null)
        {
            objeto = CrearNuevoObjeto();
        }

        objeto.SetActive(true);

        return objeto;

    }
}
