using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGreenWorld : MonoBehaviour
{
    /// <summary>
    /// Player.
    /// </summary>
    public GameObject m_Player = null;

    /// <summary>
    /// Green World
    /// </summary>
    public GameObject m_greenWorld = null;

    // Start is called before the first frame update
    void Start()
    {
        // Always disable the world.
        m_greenWorld.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // TODO 2 - Comprobamos que el transform del objeto que colisiona, es el player
        // If the player touches the trigger, respawn it.
        if (other.tag == m_Player.tag)
        {
            m_greenWorld.SetActive(true);
        }

        // TODO 3 - Enviamos un mensaje al GameManager llamando a la función "RespawnPlayer"
    }
}
