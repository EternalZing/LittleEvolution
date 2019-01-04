using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
[CreateAssetMenu(menuName="GameComponent/TilemapTerrainGenerator")]
public class TilemapTerrainGenerator : TerrainGenerator
{
    // Start is called before the first frame update

    public Tilemap target;
    public TileBase floorTile;
    public TileBase benethTile;
    public string seed;
    public int floor;
    protected const string _targetDefaultName = "Terrain";
    public int size;
    public Vector3Int leftBottomCorner;
    public override void GenerateTerrain(){
        if(target==null){
            target = GameObject.FindGameObjectWithTag(_targetDefaultName).GetComponent<Tilemap>();
        }
        TileBase[] tileBases = new TileBase[size];
        for(int i=0;i<size;i++){
            tileBases[i] = floorTile;
            target.SetTile(leftBottomCorner+Vector3Int.right*i,tileBases[i]);
        }
    }
}
