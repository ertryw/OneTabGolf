using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to check collision hole trigger for gain score.
/// </summary>
public class HoleCollider : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Ball")
		{
			SceneData.Instance.taped = false;
		}
	}

}
