﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperController : MonoBehaviour
{
    [Range(0,1)]
    public float animationSpeed;
    public bool run;
    public float speed;
    public float runSpeed;
    public float angle;
    public float rotationTime;

    public Transform[] Mojones;

    private Rigidbody rigidbody;
    private ChomperAnimation chomperAnimation;
    // private bool rotate;
    private Quaternion _lookRotation;
    private Vector3 _direction;

    public Transform objetivo;
    private Transform player;
    private int indexObj;
    private RaycastHit hit;

    public float m_MinDistance = 1.0f;

    /// <summary>
	/// Game Manager para hacer respawn del jugador
	/// </summary>
	private GameObject m_GameManager = null;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        chomperAnimation = GetComponent<ChomperAnimation>();
        // rotate = false;
        indexObj = 0;
        m_GameManager = GameObject.FindGameObjectWithTag("GameManager");
        player = GameObject.FindGameObjectWithTag("Player").transform;

        objetivo = Mojones[indexObj];
    }

    // Update is called once per frame
    void Update()
    {
        //CAMBIAMOS DE DIRECCIÓN CON UNA PROBABILIDAD DEL 5%
        //if (Random.Range(0f, 1f) >= 0.95f && !rotate)
        //    StartCoroutine(Rotate(Random.Range(1, 10) % 2 == 0));

        // Debug.Log(player.position);
        // Does the ray intersect any objects excluding the player layer
        Debug.DrawLine(transform.position, player.position, Color.red);
        run = false;
        if (Physics.Raycast(transform.position, (player.position-transform.position).normalized, out hit, 10))
        {
            if (hit.collider.tag == "Player")
            {
                run = true;
                objetivo = player;
            }
        }

        if (!run)
        {
            _CheckArrived();
            objetivo = Mojones[indexObj];
        }
            

        _direction = (objetivo.position - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationTime);

        animationSpeed = run ? 1.0f : 0.5f; 
        chomperAnimation.Updatefordward(animationSpeed);
        //La velocidad depende de si está corriendo o no
        float s = run ? runSpeed : speed;
        //#TODO: Habra que meter gravedad
        transform.position +=  transform.forward * s * Time.deltaTime;
        // Debug.Log(transform.forward);
        //#TODO: Habra que comprobar colisiones.
    }

    IEnumerator Rotate(bool inverse)
    {
        float time = 0;
        // rotate = true;
        float realAngle = inverse ? -angle : angle;
        //Podemos rotar a izquierda o a derecha.
        Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * realAngle);
        Quaternion originalRotation = transform.rotation;
        while (time < rotationTime)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(originalRotation, newRotation, time / rotationTime);
            
            yield return new WaitForEndOfFrame();
        }
        // rotate = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            // TODO 3 - Enviamos un mensaje al GameManager llamando a la función "RespawnPlayer"
            m_GameManager.SendMessage("RespawnPlayer");
        }
    }

    void _CheckArrived()
    {
        // TODO 3 - Comprobar si la plataforma está a menos distancia de la distancia mínima
        float remDist = Vector3.SqrMagnitude(objetivo.position - transform.position);
        if (remDist < m_MinDistance)
        {
            // TODO 4 - Cambiar el currentWaypoint, para que sea el contrario
            indexObj = (indexObj + 1) % 2;
        }
    }
}
