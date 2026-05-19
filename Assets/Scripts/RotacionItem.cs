using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionItem : MonoBehaviour
{
    public float velocidadRotacion;

    private void Update()
    {
        var angles = transform.rotation.eulerAngles;
        angles.y += Time.deltaTime * velocidadRotacion;
        transform.rotation = Quaternion.Euler(angles);
    }
}
