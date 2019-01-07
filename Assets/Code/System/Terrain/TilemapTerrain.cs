using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapTerrain : Terrain
{
    // Start is called before the first frame update
    void Start() {
        var tilemapTerrainGenerator = terrainGenerator as TilemapTerrainGenerator;
        if (tilemapTerrainGenerator != null) {
            tilemapTerrainGenerator.generatingCenter = Vector3Int.RoundToInt(GameObject.FindGameObjectWithTag("hero").transform.position);
            tilemapTerrainGenerator.RefreshTerrain();
        }
    }

    // Update is called once per frame
    void Update() {
        TilemapTerrainGenerator tilemapTerrainGenerator = (TilemapTerrainGenerator) terrainGenerator;

  
    }
}
