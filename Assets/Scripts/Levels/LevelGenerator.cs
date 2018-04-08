using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public GameObject ground;
	public int mapSize = 10;

	private Sprite[] groundSprites;
	private float tileW = 0.65f;
	private float tileH = 0.89f;

	// Use this for initialization
	void Start () {

		groundSprites = Resources.LoadAll<Sprite>("fullTiles");

	}

	void GenerateSquareGrid(){
		for (int j = 0; j < mapSize; j++) {
			for(int i = 0; i < mapSize; i++){
				if (j % 2 == 0) {
					Instantiate (ground, new Vector3 (i * tileW, -j * (tileH / 2), 0), Quaternion.identity);
				} else {
					Instantiate (ground, new Vector3 ((i * tileW)+(tileW/2), -j * (tileH/2), 0), Quaternion.identity);
				}

			}
		}
	}

}
