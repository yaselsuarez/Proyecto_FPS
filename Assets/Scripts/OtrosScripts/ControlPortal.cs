using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPortal : MonoBehaviour
{
    GameObject portal1;
    GameObject portal2;
    [SerializeField]Vector3 distanciaPortal = new Vector3();


    private void Awake()
    {
        portal1 = GameObject.Find("Portal1");
        portal2 = GameObject.Find("Portal2");
        
    }


    private void OnTriggerEnter(Collider other)
    {

        string nombre = this.gameObject.name;
        

        if (other.CompareTag("Player"))
        {
            switch (nombre)
            {
                case "Portal1":
                    other.transform.position = portal2.transform.position + distanciaPortal;
                    other.transform.rotation = portal2.transform.rotation;
                    break;
                case "Portal2":
                    other.transform.position = portal1.transform.position + distanciaPortal;
                    other.transform.rotation = portal1.transform.rotation;
                    break;
            } 
        }

        /*
        if (other.CompareTag("Player"))
        {
            if (this.gameObject.name == "Portal1")
            {
                other.transform.position = portal2.transform.position;
                other.transform.rotation = portal2.transform.rotation;
                
            }
            else     
            {
                other.transform.position = portal1.transform.position;
                other.transform.rotation = portal1.transform.rotation;
               
            }
        }
        */
    }
}
