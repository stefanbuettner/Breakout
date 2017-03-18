using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BrickManager : MonoBehaviour
{
	[SerializeField, HideInInspector]
	private List<GameObject> m_Bricks = new List<GameObject>();

	public GameObject brickPrefab;

	[Range(1, 25)]
	public int xCount = 17;
	[Range(1, 20)]
	public int yCount = 8;

	[SerializeField]
	private int currXCount = 0;
	[SerializeField]
	private int currYCount = 0;

	public float spacing = 0.1f;
	private float currSpacing = 0.0f;

	void Start()
	{
		RecalculatePositions();
	}


	void Update()
	{
		RecalculatePositions();
	}
#if UNITY_EDITOR
	/** Returns true if bricks were created or deleted. This means, that the positions need to be recalculated. */
	bool UpdateX()
	{
		if (brickPrefab == null)
			return false;

		if (currXCount < xCount)
		{
			for (int y = 0; y < currYCount; ++y)
			{
				for (int x = currXCount; x < xCount; ++x)
				{
					GameObject newBrick = UnityEditor.PrefabUtility.InstantiatePrefab(brickPrefab) as GameObject;
					newBrick.transform.parent = transform;
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
		else
		{
			return false;
		}

		currXCount = xCount;
		return true;
	}

	/** Returns true if bricks were created or deleted. This means, that the positions need to be recalculated. */
	bool UpdateY()
	{
		if (brickPrefab == null)
			return false;

		if (currYCount < yCount)
		{
			for (int y = currYCount; y < yCount; ++y)
			{
				for (int x = 0; x < currXCount; ++x)
				{
					GameObject newBrick = UnityEditor.PrefabUtility.InstantiatePrefab(brickPrefab) as GameObject;
					newBrick.transform.parent = transform;
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
		else
		{
			return false;
		}

		currYCount = yCount;
		return true;
	}

	/** Returns true if the spacing of the bricks changed. This means, that the positions need to be recalculated. */
	bool UpdateSpacing()
	{
		bool updateRequired = spacing != currSpacing;
		currSpacing = spacing;
		return updateRequired;
	}
#endif // UNITY_EDITOR

	private void RecalculatePositions()
	{
#if UNITY_EDITOR
		bool needsRecalculation = false;
		needsRecalculation |= UpdateX();
		needsRecalculation |= UpdateY();
		needsRecalculation |= UpdateSpacing();

		if (!needsRecalculation)
			return;
#endif // UNITY_EDITOR

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

		float xPos = (x - (width / 2.0f) + 0.5f) * (brickPrefab.transform.localScale.x + spacing);
		float yPos = (y - (height / 2.0f) + 0.5f) * (brickPrefab.transform.localScale.y + spacing);

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
