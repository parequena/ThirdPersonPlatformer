using System.Collections;
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

    private Rigidbody rigidbody;
    private ChomperAnimation chomperAnimation;
    private bool rotate;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        chomperAnimation = GetComponent<ChomperAnimation>();
        rotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        //CAMBIAMOS DE DIRECCIÓN CON UNA PROBABILIDAD DEL 5%
        if (Random.Range(0f, 1f) >= 0.95f && !rotate)
            StartCoroutine(Rotate(Random.Range(1, 10) % 2 == 0));

        chomperAnimation.Updatefordward(animationSpeed);
        //La velocidad depende de si está corriendo o no
        float s = run ? runSpeed : speed;
        //#TODO: Habra que meter gravedad
        transform.position +=  transform.forward * s * Time.deltaTime;
        //#TODO: Habra que comprobar colisiones.

    }

    IEnumerator Rotate(bool inverse)
    {
        float time = 0;
        rotate = true;
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
        rotate = false;
    }
}
