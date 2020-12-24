using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to controll ball movement
/// </summary>
public class BallController : MonoBehaviour 
{
	Rigidbody2D ball_RigidBody;
	TrajectoryProjection ball_Projection;
	Vector2 velocity_InitSet = new Vector2(0.5f, 0.5f);
	bool hole;
	float time;


	[Range(.0f, 1.0f), SerializeField] float velocity_IncrementStart = 0.05f;
	[Range(.0f, 0.2f), SerializeField] float velocity_IncrementPerLvl = 0.01f;
	[SerializeField] float TimeToEnd = 0.3f;
	[SerializeField] Text ScoresTxt;
	[SerializeField] GameObject ObjectTrigger;

	// Use this for initialization
	void Start () {
		ball_RigidBody = GetComponent<Rigidbody2D>();
		ball_Projection = GetComponent<TrajectoryProjection>();
		ResetVelocityIncrement();
	}
	
	public void ResetVelocityIncrement()
	{
		SceneData.Instance.velocity_Increment = velocity_IncrementStart;
		SceneData.Instance.velocity_IncrementPerLvl = velocity_IncrementPerLvl;
	}


	void FixedUpdate()
	{
		if (hole)
		{
			time += Time.deltaTime;
			if (time >= TimeToEnd)
			{
				InHole();
				StopBall();
				hole = false;
				time = 0;
			}
		}
	}
	void Update () 
	{


		if (SceneData.Instance.gameover)
			StopBall();

		if (SceneData.Instance.taped || SceneData.Instance.gameover)
			return;

		if (Input.GetKey("space"))
		{
			velocity_InitSet.x += SceneData.Instance.velocity_Increment;
			velocity_InitSet.y += SceneData.Instance.velocity_Increment;
			ball_Projection.Velocity = velocity_InitSet;
		}

		if (Input.GetKeyUp("space") || ball_Projection.SceneEnd)
		{
			SceneData.Instance.taped = true;
			ball_RigidBody.velocity = ball_Projection.Velocity;
		}
	}

	void InHole()
	{
		SceneData.Instance.taped = false;
		SceneData.Instance.holed = true;
		SceneData.Instance.scores++;
		ScoresTxt.text = SceneData.Instance.scores.ToString();
		ball_Projection.Velocity = new Vector2(0, 0);
	}

	void StopBall()
	{
		ball_RigidBody.velocity = new Vector2(0, 0);
		velocity_InitSet = new Vector2(0.5f, 0.5f);
		ball_Projection.Velocity = ball_RigidBody.velocity;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag(ObjectTrigger.tag))
		{
			if (!SceneData.Instance.gameover)
			{
				print("Hole!");
				SceneData.Instance.waitState = false;
				hole = true;
			}
		}
	}
}
