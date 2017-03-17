using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BrickManager : MonoBehaviour
{
	private List<GameObject> m_Bricks = new List<GameObject>();

	public GameObject brickPrefab;

	public int xCount = 17;
	public int yCount = 8;

	public int currXCount = 0;
	public int currYCount = 0;


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
			for (int x = currXCount; x < xCount; ++x)
			{
				for (int y = 0; y < currYCount; ++y)
				{
					GameObject newBrick = GameObject.Instantiate(brickPrefab,
																 GetBrickPosition(x, y, xCount, currYCount),
																 Quaternion.identity,
																 gameObject.transform);
					// Important to use the new xCount!
					m_Bricks.Insert(y * xCount + x, newBrick);
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
																 GetBrickPosition(x, y, currXCount, yCount),
																 Quaternion.identity,
																 gameObject.transform);
					m_Bricks.Add(newBrick);
				}
			}
		}
		else if (yCount < currYCount)
		{
			for (int y = 0; y < currYCount - yCount; ++y)
			{
				for (int x = 0; x < currXCount; ++x)
				{
					GameObject.DestroyImmediate(m_Bricks[y * currXCount + x]);
				}
			}
			m_Bricks.RemoveRange(yCount * currXCount, (currYCount - yCount) * currXCount);
		}

		currYCount = yCount;
	}

	private Vector3 GetBrickPosition(int x, int y, int width, int height)
	{
		if (brickPrefab == null)
			return Vector3.zero;

		return new Vector3(x * brickPrefab.transform.localScale.x, y * brickPrefab.transform.localScale.y, 0);
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
