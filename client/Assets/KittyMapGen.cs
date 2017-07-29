using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittyMapGen : MonoBehaviour {

	public Transform grassyTilePrefab;
	public Transform dirtTilePrefab;
	public Transform navmeshFloor;

	public Vector2 spawnSize;
	public Vector2 pathSize;

	//maps are every path before each checkpoint in this case, until we get a generate map ;
	public Vector2 mapSize;

	[Range(0,1)]
	public float outlinePercent;

	void Start(){
		
		mapSize.x = spawnSize.x;
		mapSize.y = spawnSize.y + pathSize.y;

		GenerateSpawn ();
		GenerateRoad ();

	}

	public void GenerateSpawn(){

		string holderName = "Generated Spawn";
		if (transform.Find (holderName)) {
			DestroyImmediate (transform.Find (holderName).gameObject);
		}

		Transform spawnHolder = new GameObject (holderName).transform;
		spawnHolder.parent = transform;

		for (int x = 0; x < spawnSize.x; x++) {
			for (int y = 0; y < spawnSize.y; y++) {
				Vector3 tilePosition = new Vector3 (-spawnSize.x / 2 + 0.5f + x, 0, -spawnSize.y / 2 + 0.5f + y);
				Transform newSpawnTile = Instantiate (grassyTilePrefab, tilePosition, Quaternion.identity) as Transform;
				newSpawnTile.localScale = Vector3.one * (1 - outlinePercent);
				newSpawnTile.parent = spawnHolder;
			}
		}

		navmeshFloor.localScale = new Vector3 (mapSize.x, mapSize.y);
	}

	public void GenerateRoad(){

		string holderName = "Generated Path";
		if (transform.Find (holderName)) {
			DestroyImmediate (transform.Find (holderName).gameObject);
		}

		Transform pathHolder = new GameObject (holderName).transform;
		pathHolder.parent = transform;

		for (int x = 0; x < spawnSize.x; x++) {
			for (int y = (int)spawnSize.y + (int)pathSize.y; y >= spawnSize.y; y--) {
				Vector3 tilePosition = new Vector3 (-spawnSize.x / 2 + 0.5f + x, 0, -spawnSize.y / 2 + 0.5f + y);
				Transform newPathTile = Instantiate (dirtTilePrefab, tilePosition, Quaternion.identity) as Transform;
				newPathTile.localScale = Vector3.one * (1 - outlinePercent);
				newPathTile.parent = pathHolder;
			}
		}
	}

	public void GenerateMap(){
		//calls generatespawn() and generateroad() until the map is complete;
		//needs to rotate 90 degrees every checkpoint (for the spiraling map);
		//can use the map size of this to determine the navmesh size;
	}
}
