using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
