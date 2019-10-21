using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	/// <summary>
	/// Primer waypoint 
	/// </summary>
	public Transform m_Waypoint1 = null;
	
	/// <summary>
	/// Segundo waypoint
	/// </summary>
	public Transform m_Waypoint2 = null;
	
	/// <summary>
	/// Velocidad del movimiento de la plataforma
	/// </summary>
	public float m_MovementSpeed = 30.0f;
	
	/// <summary>
	/// Distancia a la que se considera que la plataforma ha llegado a su destino
	/// </summary>
	public float m_MinDistance = 2.0f;
	
	/// <summary>
	/// Waypoint hacia el que se está moviendo la plataforma
	/// </summary>
	private Transform m_CurrentWaypoint = null;
	
	/// <summary>
	/// Distancia mínima al cuadrado (por eficiencia)
	/// </summary>
	private float m_MinDistanceSqr = 0.0f;
	
	void Awake()
	{
		if (!m_Waypoint1)
			Debug.LogWarning("Falta el Waypoint 1 en el componente MovingPlatform del GameObject:["+gameObject.name+"]");
		
		if (!m_Waypoint2)
			Debug.LogWarning("Falta el Waypoint 2 en el componente MovingPlatform del GameObject:["+gameObject.name+"]");
	}

	/// <summary>
	/// En la inicialización, se colocará la plataforma en el primer waypoint
	/// Y asignaremos el segundo waypoint como current
	/// De esta forma estamos seguros de que siempre el movimiento comenzará en un punto inicial
	/// razonable
	/// </summary>
	void Start ()
    {
        m_MinDistanceSqr = m_MinDistance * m_MinDistance;
		gameObject.transform.position = m_Waypoint1.position;
		m_CurrentWaypoint = m_Waypoint2;
	}
	
	/// <summary>
	/// En el Update, tenemos que mover la plataforma, y comprobar si hemos llegado lo suficientemente cerca del otro waypoint como para 
	/// tener que cambiar la dirección del movimiento
	/// </summary>
	void Update () 
    {	
		_DoMovement();
		_CheckArrived();
	}
	
	/// <summary>
	/// Realiza el movimiento de la plataforma
	/// </summary>
	void _DoMovement()
	{
        // TODO 1 - Obtener la dirección del desplazamiento y normalizarla
        Vector3 direction = m_CurrentWaypoint.position - transform.position;
        direction.Normalize();
        // TODO 2 - Mover la plataforma en función de la dirección obtenida, la velocidad, y el deltaTime
        transform.position += direction * m_MovementSpeed * Time.deltaTime;
    }

	/// <summary>
	/// Comprueba si la plataforma ha llegado al waypoint actual
	/// </summary>
	void _CheckArrived()
	{
        // TODO 3 - Comprobar si la plataforma está a menos distancia de la distancia mínima
        float remDist = Vector3.SqrMagnitude( m_CurrentWaypoint.position- transform.position);
        if (remDist < m_MinDistanceSqr)
        {
            // TODO 4 - Cambiar el currentWaypoint, para que sea el contrario
            if (m_CurrentWaypoint != m_Waypoint1)
                m_CurrentWaypoint = m_Waypoint1;
            else
                m_CurrentWaypoint = m_Waypoint2;
        }
    }
}
