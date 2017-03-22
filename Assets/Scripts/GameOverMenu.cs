using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
	public GameControl gameControl;
	public GameObject winText;

	public void PlayAgain()
	{
		gameControl.Reset();
	}

	public void Quit()
	{
		gameControl.Shutdown();
	}

	public void DisplayWin(bool win)
	{
		winText.SetActive(win);
	}
}
