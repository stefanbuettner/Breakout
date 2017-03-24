using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
	public GameObject winText;

	public delegate void NoArgFunc();

	public event NoArgFunc OnPlayAgain;
	public event NoArgFunc OnQuit;

	public void OnPlayAgainPressed()
	{
		OnPlayAgain();
	}

	public void OnQuitPressed()
	{
		OnQuit();
	}

	public void DisplayWin(bool win)
	{
		winText.SetActive(win);
	}
}
