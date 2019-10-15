using UnityEngine;

public class Rotation : MonoBehaviour
{

    /// <summary>
    /// Velocidad de rotación
    /// </summary>
    public Vector3 m_RotationSpeed = Vector3.zero;

    /// <summary>
    /// En función de la rotación que se pasa desde fuera, se rota el objeto
    /// </summary>
    void Update()
    {
        transform.Rotate(m_RotationSpeed * Time.deltaTime);
    }
}