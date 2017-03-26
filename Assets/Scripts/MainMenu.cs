using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void OnPlayPressed()
	{
		Settings s = Settings.instance;
		if (s)
			s.playerType = Settings.PlayerType.HUMAN;
		SceneManager.LoadScene("breakout");
	}

	public void OnWatchPressed()
	{
		Settings s = Settings.instance;
		if (s)
			s.playerType = Settings.PlayerType.COMPUTER;
		SceneManager.LoadScene("breakout");
	}

	public void OnQuitPressed()
	{
		Application.Quit();
	}
}
