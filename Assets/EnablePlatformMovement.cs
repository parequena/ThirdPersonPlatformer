using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlatformMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_blueWorld = null;

    public GameObject m_platform = null;

    void Start()
    {
        m_blueWorld.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        // TODO
        // Set the movement.

        m_platform.GetComponent<MovingPlatform>().MovementSpeed = 5.0f;
    }
}
