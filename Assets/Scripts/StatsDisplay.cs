using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
	public Text Points;
	public Text Turns;

	public void SetPoints(int points)
	{
		Points.text = points.ToString("D3");
	}

	public void SetTurns(int turns)
	{
        Turns.text = turns.ToString();
	}
}
