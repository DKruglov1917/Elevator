using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photocell : MonoBehaviour
{
    public bool stop;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
            stop = true;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
            stop = false;
    }
}
