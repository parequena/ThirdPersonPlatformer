using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GUIItem : MonoBehaviour
{

	public GameManager.DoorColor m_DoorColor;
	private GameManager m_gameManager;
	private GameObject m_doorGameObject;

	private Image m_Image;

	void Awake()
	{
		GameObject m_GameManagerGo = GameObject.FindGameObjectWithTag("GameManager");
		m_gameManager = m_GameManagerGo.GetComponent<GameManager>();

		m_Image = this.GetComponent<Image>();
	}

	// Use this for initialization
	void Start ()
	{
		m_doorGameObject = m_gameManager.GetDoorGameObject(m_DoorColor);
	}

	// Update is called once per frame
	void Update () {
        // TODO 1. En cada tick tenemos que comprobar si la puerta está activa
        if (m_doorGameObject == null)
            return;
        if (!m_doorGameObject.activeInHierarchy) return;
        // TODO 2. Si es así, tenemos que pintar la imagen m_Image del mismo color que la puerta
        switch (m_DoorColor)
        {
            case GameManager.DoorColor.BLUE:
                m_Image.color = Color.blue;
                break;
            case GameManager.DoorColor.GREEN:
                m_Image.color = Color.green;
                break;
            case GameManager.DoorColor.YELLOW:
                m_Image.color = Color.yellow;
                break;
            case GameManager.DoorColor.RED:
                m_Image.color = Color.red;
                break;
        }
        // TODO 3. Por último, para que el componente no haga chequeos inútiles 
        // lo desactivamos (sólo si la puerta está activa)
        this.enabled = false;
    }
}
