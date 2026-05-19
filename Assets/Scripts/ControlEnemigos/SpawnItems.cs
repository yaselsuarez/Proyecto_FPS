using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [Header("Configuracion de Spawn")]
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private float tiempoEntreSpawns = 5f;
    [SerializeField] private float radioSpawn = 10f;
    [SerializeField] private int maxEnemigos = 10;
    [SerializeField] private LayerMask sueloMask;

    private List<GameObject> enemigosActivos = new List<GameObject>();
    private Transform jugador;

    private void Start()
    {
        jugador = FindObjectOfType<ControlJugador>()?.transform;
        StartCoroutine(IESpawnearEnemigos());
    }

    private IEnumerator IESpawnearEnemigos()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreSpawns);

            // Limpiar enemigos destruidos de la lista
            enemigosActivos.RemoveAll(e => e == null);

            if (enemigosActivos.Count < maxEnemigos && jugador != null)
            {
                Vector3 posicion = ObtenerPosicionAleatoria();
                if (posicion != Vector3.zero)
                {
                    GameObject nuevoEnemigo = Instantiate(enemigoPrefab, posicion, Quaternion.identity);
                    enemigosActivos.Add(nuevoEnemigo);
                }
            }
        }
    }

    private Vector3 ObtenerPosicionAleatoria()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 direccionAleatoria = Random.insideUnitSphere * radioSpawn;
            direccionAleatoria.y = 0;
            Vector3 posicionCandidata = transform.position + direccionAleatoria;

            Ray rayo = new Ray(new Vector3(posicionCandidata.x, 50f, posicionCandidata.z), Vector3.down);
            if (Physics.Raycast(rayo, out RaycastHit hit, 100f, sueloMask))
            {
                return hit.point;
            }
        }
        return Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioSpawn);
    }
}