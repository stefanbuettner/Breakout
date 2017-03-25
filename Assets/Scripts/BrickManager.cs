using UnityEngine;

[ExecuteInEditMode]
public class BrickManager : MonoBehaviour
{
	[SerializeField, HideInInspector]
	private int activeBricks = 0;

#if UNITY_EDITOR
	private string defaultBrickPrefabName = "StandardBrick";
#endif

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

	public event Hittable.BallHitNotification OnBrickHit;

	void Start()
	{
		RecalculatePositions();
	}

	void OnEnable()
	{
		Brick[] bricks = GetComponentsInChildren<Brick>();
		foreach (Brick brick in bricks)
		{
			brick.OnBallHit += BrickHit;
		}
	}

	void OnDisable()
	{
		Brick[] bricks = GetComponentsInChildren<Brick>();
		foreach (Brick brick in bricks)
		{
			brick.OnBallHit -= BrickHit;
		}
	}

	public void Reset()
	{
		activeBricks = 0;
		Brick[] bricks = GetComponentsInChildren<Brick>(true);
		foreach (Brick brick in bricks)
		{
			brick.gameObject.SetActive(true);
			++activeBricks;
		}
	}

#if UNITY_EDITOR
	void Update()
	{
		RecalculatePositions();
	}


	/** Returns true if bricks were created or deleted. This means, that the positions need to be recalculated. */
	bool UpdateX()
	{
		if (currXCount < xCount)
		{
			for (int y = 0; y < currYCount; ++y)
			{
				for (int x = currXCount; x < xCount; ++x)
				{
					CreateNewBrick(defaultBrickPrefabName);
				}
			}
		}
		else if (xCount < currXCount)
		{
			Brick[] bricks = GetComponentsInChildren<Brick>();
			for (int y = 0; y < currYCount; ++y)
			{
				for (int x = 0; x < currXCount - xCount; ++x)
				{
					Brick brick = bricks[x + (y + 1) * xCount];
					if (brick != null)
						GameObject.DestroyImmediate(brick.gameObject);
				}
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
					CreateNewBrick(defaultBrickPrefabName);
				}
			}
		}
		else if (yCount < currYCount)
		{
			Brick[] bricks = GetComponentsInChildren<Brick>();
			for (int y = yCount; y < currYCount; ++y)
			{
				for (int x = 0; x < currXCount; ++x)
				{
					Brick brick = bricks[x + y * currXCount];
					if (brick != null)
						GameObject.DestroyImmediate(brick.gameObject);
				}
			}
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

		Brick[] bricks = GetComponentsInChildren<Brick>();
		for (int y = 0; y < currYCount; ++y)
		{
			for (int x = 0; x < currXCount; ++x)
			{
				Brick brick = bricks [x + y *currXCount];
				if (brick != null)
					brick.transform.localPosition = GetBrickPosition(x, y, currXCount, currYCount);
			}
		}
	}

	private Vector3 GetBrickPosition(int x, int y, int width, int height)
	{
		float xPos = (x - (width / 2.0f) + 0.5f) * currXSpacing;
		float yPos = (y - (height / 2.0f) + 0.5f) * currYSpacing;

		return new Vector3(xPos, yPos, 0);
	}

#if UNITY_EDITOR
	GameObject CreateNewBrick(string prefabType)
	{
		GameObject brickPrefab = Resources.Load("Bricks/" + prefabType) as GameObject;
		GameObject newBrick = UnityEditor.PrefabUtility.InstantiatePrefab(brickPrefab) as GameObject;
		newBrick.transform.parent = transform;
		Brick brickComponent = newBrick.GetComponent<Brick>();
		if (brickComponent == null)
			brickComponent = newBrick.AddComponent<Brick>();
		brickComponent.SetPrefabType(newBrick.name);

		return newBrick;
	}

	public void ChangeBrickTypeSelection(string brickType)
	{
		foreach (GameObject go in UnityEditor.Selection.gameObjects)
		{
			GameObject newBrick = CreateNewBrick(brickType);
			newBrick.transform.position = go.transform.position;
			newBrick.transform.rotation = go.transform.rotation;
			newBrick.transform.localScale = go.transform.localScale;
			GameObject.DestroyImmediate(go);
		}
	}
#endif

	public void BrickHit(Ball ball, Hittable hit)
	{
		--activeBricks;
		OnBrickHit(ball, hit);
	}

	public int GetActiveBricks()
	{
		return activeBricks;
	}
}
