using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que implementa las animaciones de Chomp
/// </summary>
public class ChomperAnimation : MonoBehaviour
{
    private Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        //#TO-DO 1: cargar el componente Animator.
        m_Animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        //#TO-DO 2: lanzar el trigger Attack.
        m_Animator.SetTrigger("Attack");

    }

    public void Updatefordward(float ford)
    {
        //#TO-DO 3: Actualizar la variable Fordward
        m_Animator.SetFloat("Forward", ford);


    }
    // Update is called once per frame

}
