using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngineInternal;

/// <summary>
/// Projection ball trajectory
/// </summary>
public class TrajectoryProjection : MonoBehaviour {

	// private
	float start_VelocityX = .0f;
	float start_VelocityY = .0f;
	/// <summary> Out of scene bool  </summary>
	bool scene_End = false;
	/// <summary> Projection balls position  </summary>
	List<Vector3> balls_Projection = new List<Vector3>();
	/// <summary> Ball start position  </summary>
	Vector3 ball_start_position;

	/// <summary> Trasform of end of scene object  </summary>
	[SerializeField] Transform sceneEndTransform;
	/// <summary> Trasform of ground level object </summary>
	[SerializeField] Transform ground_Level;
	/// <summary> Projection ball show count </summary>
	[SerializeField] int balls_Count = 5;
	/// <summary> Equation step </summary>
	[SerializeField] float step_Time = 0.1f;
	/// <summary> Projection ball texture </summary>
	[SerializeField] Texture ballTexture;
	/// <summary> Projection ball size </summary>
	[SerializeField] Vector2Int ball_size;
	/// <summary> Bool that show if ball will be throw out of scene </summary>
	[HideInInspector] public bool SceneEnd => scene_End;
	/// <summary> Velocity that with be add to ball </summary>
	[HideInInspector] 
	public Vector2 Velocity 
	{ 
		get 
		{ 
			return new Vector2(start_VelocityX, start_VelocityY); 
		} 
		set
		{
			start_VelocityX = value.x;
			start_VelocityY = value.y;
		}
	}

	void Start () 
	{
		ball_start_position = transform.position;
	}

	/// <summary> Drawing projection </summary>
	void OnGUI()
	{
		if (!ballTexture)
		{
			Debug.LogError("Przypisz texture piłki w inspektorze");
			return;
		}

		if (SceneData.Instance.gameover)
			return;

		if (Velocity.magnitude <= 0)
			return;

		foreach (var item in balls_Projection)
		{
			var position = Camera.main.WorldToScreenPoint(item);
			if (item.y > ground_Level.position.y)
			{
				Rect ball_Rect = new Rect(position.x - (ball_size.x / 2), Screen.height - position.y - (ball_size.y / 2) + 5, ball_size.x, ball_size.y);
				GUI.DrawTexture(ball_Rect, ballTexture, ScaleMode.ScaleToFit, true, 1.0F);
			}
		}
	}

	void Update () 
	{
		ProjectTrajectory(ball_start_position, Velocity, step_Time, balls_Count);
	}

	/// <summary> Trajectory points projection </summary>
	public void ProjectTrajectory(Vector3 start, Vector3 startVelocity, float timestep, float ballsCount)
	{
		balls_Projection.Clear();

		for (int i = 1; i <= ballsCount; i++)
		{
			float t = timestep * i;
			Vector3 pos = ProjectionTrajectoryTime(start, startVelocity, t);
			if (pos.y > ground_Level.position.y)
				scene_End = pos.x > sceneEndTransform.position.x;

			balls_Projection.Add(pos);
		}
	}

	/// <summary> Trajectory move equation </summary>
	public Vector3 ProjectionTrajectoryTime(Vector3 start, Vector3 startVelocity, float time)
	{
		return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
	}

}
