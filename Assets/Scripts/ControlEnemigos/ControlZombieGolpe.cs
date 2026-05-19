using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlZombieGolpe : MonoBehaviour
{
    public int daño;
    public AudioClip zarpazoFX;
    AudioSource audioSource;
    BoxCollider boxCollider;
    public IndiceDeterioro indiceDeterioro;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        indiceDeterioro = GameObject.FindGameObjectWithTag("Deterioro").GetComponent<IndiceDeterioro>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            indiceDeterioro.Aparecer();
            other.gameObject.GetComponent<ControlJugador>().QuitarVida(daño);
            StartCoroutine(espera());
        }
    }

    IEnumerator espera()
    {
        audioSource.PlayOneShot(zarpazoFX, 0.5f);
        boxCollider.enabled = false;
        yield return new WaitForSeconds(1.5f);
        boxCollider.enabled = true;

    }



}
