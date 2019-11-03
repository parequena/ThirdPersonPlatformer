using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallRotation : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.rotation.z);
        if(transform.rotation.z > 45)
        {
            Debug.Log("Adieu");
            GetComponent<CharacterController>().enabled = false;
        }
    }
}
