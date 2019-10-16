using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PowerUpTimer : MonoBehaviour
{

	public float TotalTime;

	private float m_RemainingTime;
	private Image m_Image;

	// Use this for initialization
	void Start ()
	{
		m_Image = GetComponent<Image>();
	}

	void OnEnable()
	{
		// Al activarlo reseteamos el tiempo total que dura el powerup
		m_RemainingTime = TotalTime;
	}

	// Update is called once per frame
	void Update ()
	{
		// TODO 1 - Comprobamos si se ha acabado el tiempo
        if (m_RemainingTime <= 0)
        {
            // enabled = false;
            gameObject.SetActive(false);
            // TODO 2 - Desactivamos el gameobject para que no se pinte
 
        }
        else
        {

            float portion = Mathf.InverseLerp(0, TotalTime, m_RemainingTime);
            // TODO 4 - Asignamos este valor al fillAmount de la imagen
            m_Image.fillAmount = portion;
            // TODO 5 - Restamos al tiempo restante el tiempo que ha pasado
            m_RemainingTime -= Time.deltaTime;
        }
    }
}
