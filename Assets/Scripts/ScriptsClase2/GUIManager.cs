using UnityEngine;

public class GUIManager : MonoBehaviour
{

	public static GUIManager Instance;

	public PowerUpTimer PowerUpTimer;

	void Awake()
	{
		// Si ya existe un GUIManager nos destruimos
		if (Instance != null && Instance != this)
			Destroy(this);
		else
		{
			Instance = this;
            Instance.PowerUpTimer.gameObject.SetActive(false);
		}
	}

	public void StartPowerUpTimer(float time)
	{
		PowerUpTimer.TotalTime = time;
		PowerUpTimer.gameObject.SetActive(true);
	}

}
