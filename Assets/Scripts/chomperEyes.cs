using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chomperEyes : MonoBehaviour
{

    private GameObject chomper;
    private ChomperController chomperScript;
    private Transform tChomper;
    // Start is called before the first frame update
    void Start()
    {
        chomper = transform.parent.GetChild(0).gameObject;
        chomperScript = chomper.GetComponent<ChomperController>();
        tChomper = chomper.transform;
        
    }

    private void Update()
    {
        transform.position = tChomper.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            chomperScript.run = true;
            chomperScript.objetivo = other.transform;
            //Debug.Log(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // TODO Hacer que los ojos no sean hijos
        if (other.tag == "Player")
        {
            chomperScript.run = false;
            //Debug.Log("Te salvaste perro");
        }
    }
}
