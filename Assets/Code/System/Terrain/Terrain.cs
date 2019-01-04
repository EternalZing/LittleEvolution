using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Terrain : MonoBehaviour
{
    public TerrainGenerator terrainGenerator;
    // Start is called before the first frame update
    void Start(){
        if(terrainGenerator.GetType().IsSubclassOf(typeof (TilemapTerrainGenerator))){
            ((TilemapTerrainGenerator)(terrainGenerator)).target = this.GetComponent<Tilemap>();
        }
        terrainGenerator.GenerateTerrain();
    }

    // Update is called once per frame
    void Update(){
        
    }
}
