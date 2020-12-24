using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Check collision with ground for Gameover state.
/// </summary>
public class TileMapCollider : MonoBehaviour {

	// Use this for initialization
	bool collisionTrigger;
	float time = 0;

	/// <summary> Time delay to gameOver </summary>
	[SerializeField] float TimeToEnd = 0.3f;
	[SerializeField] GameObject ObjectTrigger;

	// Update is called once per frame
	void FixedUpdate () 
	{

		if (collisionTrigger)
		{
			time += Time.deltaTime;
			if (time >= TimeToEnd)
			{
				if (SceneData.Instance.taped && SceneData.Instance.waitState)
				{
					SceneData.Instance.gameover = true;
				}
				collisionTrigger = false;
				time = 0;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag(ObjectTrigger.tag))
		{
			if (SceneData.Instance.taped)
			{
				SceneData.Instance.waitState = true;
				collisionTrigger = true;
			}
		}
	}
}
