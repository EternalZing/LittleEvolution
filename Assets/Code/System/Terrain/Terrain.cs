using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Terrain : MonoBehaviour
{
    public TerrainGenerator terrainGenerator;
    // Start is called before the first frame update
    public Vector3 CapturedTerrainRenderCentre {
        get {
            return GameObject.FindGameObjectWithTag("Hero").transform.position;
        }
    }


    void Start(){
        if(terrainGenerator.GetType().IsSubclassOf(typeof (TilemapTerrainGenerator))){
            ((TilemapTerrainGenerator)(terrainGenerator)).target = this.GetComponent<Tilemap>();
        }
    }
    
    // Update is called once per frame
    void Update(){
        
    }
}
