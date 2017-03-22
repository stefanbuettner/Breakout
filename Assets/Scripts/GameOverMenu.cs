using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
	public GameControl gc;

	public void PlayAgain()
	{
		gc.Reset();
	}

	public void Quit()
	{
		gc.Shutdown();
	}
}
