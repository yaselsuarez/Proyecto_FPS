using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMinimap : MonoBehaviour
{
    public Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }

    public void quitarMiniMapa()
    {

    }
}
