﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
	public GameControl gameControl;

	public void PlayAgain()
	{
		gameControl.Reset();
	}

	public void Quit()
	{
		gameControl.Shutdown();
	}
}
