using UnityEngine;
using System.Collections;

public class SuperJumpPowerUp : MonoBehaviour
{
	// TODO 1 - Atributo público de tipo float que representará la duración del power up
	/// <summary>
	/// Atributo que indica el tiempo que durará el powerUp
	/// </summary>
	public float m_duration;
	/// <summary>
	/// Atributo que indica la altura máxima de salto que alcanzará
	/// el jugador cuando el power up esté activo
	/// </summary>
	public float m_SuperJumpHeight = 4.0f;

    /// <summary>
    /// Cuando el jugador toca el ítem, este debe otorgar la habilidad de super-salto al jugador
    /// durante un tiempo determinado
    /// </summary>
    /// <param name="other">
    /// Objeto que chica contra el item <see cref="Collider"/>
    /// </param>
    IEnumerator OnTriggerEnter(Collider other)
	{
        // TODO 2 - Si el objeto que entra en mi trigger tiene el tag player

        TrailRenderer trailRenderer = null;
        if (other.tag == "Player")
        {
            // TODO 3 - Le envío un mensaje "SetJumpHeight" con la altura que tengo configurada para el super-salto
            other.SendMessage("SetJumpHeight", m_SuperJumpHeight);
            // TODO 4 - Desactivo el renderer y el collider de mi gameObject
            // Pista: atributo "enabled"
            this.GetComponent<Renderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;

            // TODO Refactor 1 - Iniciar el timer del GUIManager (método StartPowerUpTimer)
            GUIManager.Instance.StartPowerUpTimer(m_duration);

            // TODO Refactor 2 - Obtener el componente TrailRenderer del jugador y activarlo
            trailRenderer = other.GetComponent<TrailRenderer>();
            if (trailRenderer != null)
                trailRenderer.enabled = true;
        }

        yield return new WaitForSeconds(m_duration);

        // TODO 5 - Envío un mensaje recuperando la altura del salto anterior (por defecto, 6)
        other.SendMessage("RestoreJumpHeight");

        // TODO Refactor 2 - Obtener el componente TrailRenderer del jugador y desactivarlo
        if (trailRenderer != null)
            trailRenderer.enabled = false;

        Destroy(gameObject);
    }
}