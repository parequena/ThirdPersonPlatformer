using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	/// <summary>
	/// Enumerado que almacena los posibles colores de las puertas
	/// Será útil para la activación/desacivación de las mismas
	/// </summary>
	public enum DoorColor
	{
		GREEN = 0,
		RED = 1,
		BLUE = 2,
		YELLOW = 3,
	}

	/// <summary>
	/// Punto de spawn inicial
	/// </summary>
	public Transform m_InitialSpawnPoint = null;

	/// <summary>
	/// ¿Quién es el jugador?
	/// </summary>
	public GameObject m_Player = null;

	/// <summary>
	/// Referencia a la puerta verde
	/// </summary>
	public GameObject m_GreenDoor = null;

	/// <summary>
	/// Referencia a la puerta roja
	/// </summary>
	public GameObject m_RedDoor = null;

	/// <summary>
	/// Referencia a la puerta azul
	/// </summary>
	public GameObject m_BlueDoor = null;

	/// <summary>
	/// Referencia a la puerta amarilla
	/// </summary>
	public GameObject m_YellowDoor = null;

	/// <summary>
	/// Este atributo controla si las puertas estarán activadas o no
	/// cuando el jugador comience el juego
	/// </summary>
	public bool m_Cheater = false;


    //CustomInspector
    public string greenSceneName;
    public string redSceneName;
    public string blueSceneName;
    public string yellowSceneName;


    /// <summary>
    /// Actual punto de spawn. 
    /// Durante el juego el punto de spawn puede variar (en función de los 
    /// niveles que hayamos desbloqueado)
    /// </summary>
    private Transform m_CurrentSpawnPoint = null;

	/// <summary>
	/// Diccionario que contiene cada puerta asociada a su color
	/// Se poblará en el Awake
	/// </summary>
	private Dictionary<DoorColor, GameObject> m_Doors = new Dictionary<DoorColor, GameObject>();

	/// <summary>
	/// En esta lista se almacenan los niveles ya cargados
	/// </summary>
	private List<DoorColor> m_LevelsLoaded = new List<DoorColor>();

	/// <summary>
	/// Este diccionario relaciona un color con el nombre del nivel que le corresponde
	/// </summary>
	private Dictionary<DoorColor, string> m_LevelNames = new Dictionary<DoorColor, string>();


	void Awake()
	{
		if (!m_InitialSpawnPoint)
			Debug.LogWarning("No se ha asignado un punto de spawn inicial");

		// Al principio, el punto incial será el punto actual de spawn
		m_CurrentSpawnPoint = m_InitialSpawnPoint;

		// Rellenamos el diccionario con las referencias necesarias
		m_Doors[DoorColor.GREEN] = m_GreenDoor;
		m_Doors[DoorColor.RED] = m_RedDoor;
		m_Doors[DoorColor.BLUE] = m_BlueDoor;
		m_Doors[DoorColor.YELLOW] = m_YellowDoor;

        // Rellenamos el diccionario con los nombres de los niveles
        /*m_LevelNames[DoorColor.GREEN] = "green_world";
		m_LevelNames[DoorColor.RED] = "red_world";
		m_LevelNames[DoorColor.BLUE] = "blue_world";
		m_LevelNames[DoorColor.YELLOW] = "yellow_world";*/
        m_LevelNames[DoorColor.GREEN] = greenSceneName;
        m_LevelNames[DoorColor.RED] = redSceneName;
        m_LevelNames[DoorColor.BLUE] = blueSceneName;
        m_LevelNames[DoorColor.YELLOW] = yellowSceneName;
    }

	/// <summary>
	/// Inicialización del jugador. Se recoloca en el escenario
	/// </summary>
	void Start()
	{

		// Si juego legalmente, desactivo las puertas
		if (!m_Cheater)
		{
			ActivateDoor(DoorColor.RED, false);
			ActivateDoor(DoorColor.BLUE, false);
			ActivateDoor(DoorColor.YELLOW, false);
		}
		RespawnPlayer();
	}

	/// <summary>
	/// Desde fuera pueden configurar el punto de spawn
	/// </summary>
	/// <param name="current">
	/// Nuevo punto de spawn <see cref="Transform"/>
	/// </param>
	public void SetCurrentSpawnPoint(Transform current)
	{
		m_CurrentSpawnPoint = current;
	}

	/// <summary>
	/// Desde fuera nos pueden pedir el punto de spawn
	/// </summary>
	/// <returns>
	/// Actual punto de spawn <see cref="Transform"/>
	/// </returns>
	public Transform GetCurrentSpawnPoint()
	{
		return m_CurrentSpawnPoint;
	}

	/// <summary>
	/// Esta función setea la posición del player, haciéndola coincidir
	/// con la posición del punto de spawn actual
	/// </summary>
	public void RespawnPlayer()
	{
		// Colocamos al player en el punto de spawn actual
		m_Player.transform.position = m_CurrentSpawnPoint.position;
	}

	/// <summary>
	/// Esta función activa/desactiva una puerta con un tipo dado
	/// </summary>
	/// <param name="doorColor">
	/// Color de la puerta que se quiere desactivar <see cref="DoorColor"/>
	/// </param>
	/// <param name="value">
	/// True, activa la puerta, false la desactiva <see cref="System.Boolean"/>
	/// </param>
	public void ActivateDoor(DoorColor doorColor, bool value)
	{
		// Si la puerta existe, la intentamos activar
		if (m_Doors.ContainsKey(doorColor))
		{
			GameObject doorToActivate = m_Doors[doorColor];
			if (doorToActivate)
			{
				// Si la referencia está bien configurada, es posible activarla
				doorToActivate.SetActive(value);
			}
			else
				Debug.LogWarning("La puerta que se está intentando activar, no se ha asignado en el GameManager");
		}
	}

	/// <summary>
	/// Función que carga un nivel determinado a partir del tipo
	/// </summary>
	/// <param name="doorColor">
	/// Color del nivel que se quiere cargar <see cref="DoorColor"/>
	/// </param>
	public void TriggerLoadAdditive(DoorColor doorColor)
	{
		// Si el nivel no está cargado, se carga
		if (!m_LevelsLoaded.Contains(doorColor))
		{
			string levelName = m_LevelNames[doorColor];
			SceneManager.LoadScene(levelName,LoadSceneMode.Additive);
			// Es muy importante añadir el nivel como cargado
			m_LevelsLoaded.Add(doorColor);
		}
		else
			Debug.LogWarning("El nivel [" + m_LevelNames[doorColor] + "] ya se ha cargado. No se va a volver a realizar la carga");
	}

	/// <summary>
	/// Función que devuelve el Game Object que representa a cada una de las puertas a partir de su color
	/// </summary>
	/// <param name="color">Color de la puerta que queremos obtener</param>
	/// <returns>Puerta del color</returns>
	public GameObject GetDoorGameObject(DoorColor color)
	{
		GameObject doorGo;
		m_Doors.TryGetValue(color, out doorGo);
		return doorGo;
	}
}