using UnityEngine;

public class Settings : MonoBehaviour
{
	public static Settings instance = null;

	void Awake()
	{
		if (instance == null)
		{
			DontDestroyOnLoad(this);
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(this);
		}
	}

	public enum PlayerType
	{
		HUMAN,
		COMPUTER
	}

	public PlayerType playerType = PlayerType.HUMAN;
}
