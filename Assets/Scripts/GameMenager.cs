using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/// <summary>
/// Scene work flow
/// </summary>
public class GameMenager : MonoBehaviour {

	/// <summary> Trasform of ground level object </summary>
	[SerializeField] Transform ground_Level;
	/// <summary> Trasform of ball spawn point</summary>
	[SerializeField] Transform ball_Spawn;
	/// <summary> Trasform of ball object </summary>
	[SerializeField] Transform ball_Object;
	/// <summary> Offset for placing hole prefab </summary>
	[SerializeField] Vector2 hole_Offset = new Vector2(0.5f, 0.5f);
	/// <summary> Hole prefab </summary>
	[SerializeField] GameObject hole_Prefab;

	/// <summary> Grid of tiles </summary>
	[SerializeField] Tilemap ground_Grid;
	/// <summary> Ground tile </summary>
	[SerializeField] Tile groundTile;
	/// <summary> Ground tile without collider </summary>
	[SerializeField] Tile groundTileNoneColision;

	/// <summary> UI Gameover panel </summary>
	[SerializeField] Transform GameOverPanel;
	/// <summary> UI Score Value </summary>
	[SerializeField] Text Scores;
	/// <summary> UI End Score Velue </summary>
	[SerializeField] Text EndScores;
	/// <summary> UI Best Score Velue </summary>
	[SerializeField] Text BestScores;


	Vector2 hole_Position;
	Vector3Int hole_cell_Position;
	GameObject hole_Obj;

	void Start () 
	{
		Screen.SetResolution(1280, 720,false);
		hole_Obj = Instantiate(hole_Prefab, transform.position, Quaternion.identity);
		NewLevel(true);
	}

	void SaveScores()
	{
		int best_scores = PlayerPrefs.GetInt("BestScores");
		if (SceneData.Instance.scores > best_scores)
		{
			PlayerPrefs.SetInt("BestScores", SceneData.Instance.scores);
		}
		SceneData.Instance.best_scores = best_scores;
		EndScores.text = SceneData.Instance.scores.ToString();
		BestScores.text = SceneData.Instance.best_scores.ToString();
	}

	public void NewLevel(bool firstLevel)
	{
		ball_Object.position = ball_Spawn.position;
		SceneData.Instance.gameover = false;
		SceneData.Instance.taped = false;
		SceneData.Instance.holed = false;
		SceneData.Instance.velocity_Increment += SceneData.Instance.velocity_IncrementPerLvl;

		if (!firstLevel)
			ground_Grid.SetTile(hole_cell_Position, groundTile);

		hole_Position = new Vector3(Random.Range(0, 8), ground_Level.position.y - 1);
		hole_cell_Position = ground_Grid.WorldToCell(hole_Position);
		ground_Grid.SetTile(hole_cell_Position, groundTileNoneColision);
		hole_Obj.transform.SetPositionAndRotation(hole_Position + hole_Offset, Quaternion.identity);
	}

	public void resetScores()
	{
		SceneData.Instance.scores = 0;
		Scores.text = SceneData.Instance.scores.ToString();
	}


	// Update is called once per frame
	void Update () 
	{
		if (SceneData.Instance.gameover)
		{
			SaveScores();
			GameOverPanel.gameObject.SetActive(true);
		}

		if (!SceneData.Instance.gameover && GameOverPanel.gameObject.activeSelf)
			GameOverPanel.gameObject.SetActive(false);

		if (SceneData.Instance.holed)
			NewLevel(false);

		if (Input.GetKey("escape"))
			Application.Quit();

	}
}
