using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BrickManager : MonoBehaviour
{
	private List<GameObject> m_Bricks = new List<GameObject>();

	public GameObject brickPrefab;

	[Range(1, 25)]
	public int xCount = 17;
	[Range(1, 20)]
	public int yCount = 8;

	private int currXCount = 0;
	private int currYCount = 0;


	void Start()
	{
		UpdateX();
		UpdateY();
	}


	void Update()
	{
		UpdateX();
		UpdateY();
	}

	void UpdateX()
	{
		if (brickPrefab == null)
			return;

		if (currXCount < xCount)
		{
			for (int y = 0; y < currYCount; ++y)
			{
				for (int x = currXCount; x < xCount; ++x)
				{
					GameObject newBrick = GameObject.Instantiate(brickPrefab,
																 gameObject.transform);
					// Important to use the new xCount!
					m_Bricks.Insert(y * xCount + currXCount, newBrick);
				}
			}
		}
		else if (xCount < currXCount)
		{
			for (int y = 0; y < currYCount; ++y)
			{
				for (int x = 0; x < currXCount - xCount; ++x)
				{
					GameObject.DestroyImmediate(m_Bricks[(y + 1) * xCount + x]);
				}
				m_Bricks.RemoveRange((y + 1) * xCount, currXCount - xCount);
			}
		}

		currXCount = xCount;
		RecalculatePositions();
	}

	void UpdateY()
	{
		if (brickPrefab == null)
			return;

		if (currYCount < yCount)
		{
			for (int y = currYCount; y < yCount; ++y)
			{
				for (int x = 0; x < currXCount; ++x)
				{
					GameObject newBrick = GameObject.Instantiate(brickPrefab,
																 gameObject.transform);
					m_Bricks.Add(newBrick);
				}
			}
		}
		else if (yCount < currYCount)
		{
			for (int y = yCount; y < currYCount; ++y)
			{
				for (int x = 0; x < currXCount; ++x)
				{
					GameObject.DestroyImmediate(m_Bricks[y * currXCount + x]);
				}
			}
			m_Bricks.RemoveRange(yCount * currXCount, (currYCount - yCount) * currXCount);
		}

		currYCount = yCount;
		RecalculatePositions();
	}

	private void RecalculatePositions()
	{
		for (int y = 0; y < currYCount; ++y)
		{
			for (int x = 0; x < currXCount; ++x)
			{
				m_Bricks[x + y * currXCount].transform.position = GetBrickPosition(x, y, currXCount, currYCount);
			}
		}
	}

	private Vector3 GetBrickPosition(int x, int y, int width, int height)
	{
		if (brickPrefab == null)
			return Vector3.zero;

		float xPos = (x - (width / 2f)) * brickPrefab.transform.localScale.x;
		float yPos = (y - (height / 2f)) * brickPrefab.transform.localScale.y;

		return new Vector3(xPos, yPos, 0);
	}

	void OnDestroy()
	{
		foreach (GameObject brick in m_Bricks)
		{
			GameObject.DestroyImmediate(brick);
		}
		m_Bricks.Clear();
	}
}
