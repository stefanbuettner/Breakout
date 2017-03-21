﻿using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BrickManager : MonoBehaviour
{
	[SerializeField, HideInInspector]
	private List<GameObject> m_Bricks = new List<GameObject>();

	private string defaultBrickPrefabName = "StandardBrick";

	[Range(0, 25)]
	public int xCount = 17;
	[Range(0, 20)]
	public int yCount = 8;

	[SerializeField, HideInInspector]
	private int currXCount = 0;
	[SerializeField, HideInInspector]
	private int currYCount = 0;

	public float xSpacing = 4.1f;
	public float ySpacing = 1.1f;
	[SerializeField, HideInInspector]
	private float currXSpacing = 0.0f;
	[SerializeField, HideInInspector]
	private float currYSpacing = 0.0f;

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
		if (currXCount < xCount)
		{
			for (int y = 0; y < currYCount; ++y)
			{
				for (int x = currXCount; x < xCount; ++x)
				{
					GameObject newBrick = CreateNewBrick(defaultBrickPrefabName);
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
		if (currYCount < yCount)
		{
			for (int y = currYCount; y < yCount; ++y)
			{
				for (int x = 0; x < currXCount; ++x)
				{
					GameObject newBrick = CreateNewBrick(defaultBrickPrefabName);
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
		bool updateRequired = (xSpacing != currXSpacing) || (ySpacing != currYSpacing);
		currXSpacing = xSpacing;
		currYSpacing = ySpacing;
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
		float xPos = (x - (width / 2.0f) + 0.5f) * xSpacing;
		float yPos = (y - (height / 2.0f) + 0.5f) * ySpacing;

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

	GameObject CreateNewBrick(string prefabType)
	{
		GameObject brickPrefab = Resources.Load("Bricks/" + prefabType) as GameObject;
		GameObject newBrick = UnityEditor.PrefabUtility.InstantiatePrefab(brickPrefab) as GameObject;
		newBrick.transform.parent = transform;
		BrickType brickComponent = newBrick.GetComponent<BrickType>();
		if (brickComponent == null)
			brickComponent = newBrick.AddComponent<BrickType>();
		brickComponent.brickManager = this;
		brickComponent.prefabType = newBrick.name;

		return newBrick;
	}

	public void ChangeBrickTypeSelection(string brickType)
	{
		foreach (GameObject go in UnityEditor.Selection.gameObjects)
		{
			int idx = m_Bricks.IndexOf(go);
			GameObject newBrick = CreateNewBrick(brickType);
			newBrick.transform.position = go.transform.position;
			newBrick.transform.rotation = go.transform.rotation;
			newBrick.transform.localScale = go.transform.localScale;
			GameObject.DestroyImmediate(go);
			m_Bricks[idx] = newBrick;
		}
	}
}
