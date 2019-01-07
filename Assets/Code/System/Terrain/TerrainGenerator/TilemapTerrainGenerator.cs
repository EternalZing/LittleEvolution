using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System;
using System.Linq;
using System.Xml.Schema;
using Code.System.Terrain.TerrainGenerator;

[CreateAssetMenu(menuName="GameComponent/TilemapTerrainGenerator")]
public class TilemapTerrainGenerator : TerrainGenerator{
    // Start is called before the first frame update
    // 生成一类BlockSet地形
    protected const string TARGET_DEFAULT_NAME = "Terrain";

    [Serializable]
    public class TilemapTerrainBlock:ITerrainBlock{
        [HideInInspector]
        public BlockSet targetBlockSet;
        public string seed;
        public bool Wasted { get; set; }
        public int BlockId { get; set; }
        public Vector3Int shifter;
        public int blockLentgh;

        public TilemapTerrainBlock() {
            
        }

        public TilemapTerrainBlock(int i) {
            BlockId = i;
        }
    }

    public BlockSet defaultBlockSet;
    [HideInInspector]
    public Tilemap target;
    //预生成区块区间的长度.
    public int PregeneratedBlockLength = 2;
    public LinkedList<TilemapTerrainBlock> terrainBlocks = new LinkedList<TilemapTerrainBlock>();
    public Vector3Int generatingCenter;
    public string initSeed;
    private List<int> pregeneratingBlocksId = new List<int>();
    public TilemapTerrainBlock currentTilemapTerrainBlock;
    

    public TilemapTerrainBlock FromVectorToBlock(Vector3Int vector3Int) {
        foreach (var node in terrainBlocks) {
            if (vector3Int.x >= node.shifter.x && vector3Int.x <= node.shifter.x + node.blockLentgh) {
                return node;
            }
        }
        return null;
    }
    public int BlockId {
        get {
            return currentTilemapTerrainBlock.BlockId;
            
        }
    }
    public BlockSet TargetBlockSet{
        get{
            return defaultBlockSet;
        }
        set{
            currentTilemapTerrainBlock.targetBlockSet = value;
        }
    }

    public Vector3Int defaultShifter;
    [HideInInspector]
    public Vector3Int LeftBottomCorner {
        get { return currentTilemapTerrainBlock.shifter; }
    }
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// 为BlockSet的地形数据根据当前区块在位置上进行偏移.以在Tilemaps上使用
    /// </summary>
    /// <param name="vector3Ints"></param>
    protected void DoShiftForTerrainInfoVec3Is(List<Vector3Int> vector3Ints){
        Debug.Log(LeftBottomCorner);
        for(int i=0;i<vector3Ints.Count;i++){
            vector3Ints[i] = vector3Ints[i]+LeftBottomCorner;
        }
    }
    /// <summary>
    /// 生成当前区块的地形
    /// </summary>
    protected void GenerateCurrentTerrainBlock() {
        TargetBlockSet.LetBlockGetInfo();
        if (terrainBlocks.Count == 0) {
            currentTilemapTerrainBlock.shifter = defaultShifter;
            terrainBlocks.AddFirst(new LinkedListNode<TilemapTerrainBlock>(currentTilemapTerrainBlock));
        }
        else {
            if (BlockId < terrainBlocks.First.Value.BlockId) {
                currentTilemapTerrainBlock.shifter = terrainBlocks.First.Value.shifter - new Vector3Int(TargetBlockSet.BlockLength-1, 0, 0);
                terrainBlocks.AddFirst(new LinkedListNode<TilemapTerrainBlock>(currentTilemapTerrainBlock));
                
            }
            else {
                currentTilemapTerrainBlock.shifter = terrainBlocks.Last.Value.shifter + new Vector3Int(TargetBlockSet.BlockLength-1, 0, 0);
                terrainBlocks.AddLast(new LinkedListNode<TilemapTerrainBlock>(currentTilemapTerrainBlock));
               
            }
        }
        DoShiftForTerrainInfoVec3Is(TargetBlockSet.GetTerrainInfoVec3I);
        currentTilemapTerrainBlock.seed = this.initSeed + BlockId;
        target.SetTiles(TargetBlockSet.GetTerrainInfoVec3I.ToArray(),TargetBlockSet.GetTerrianInfoTileBase.ToArray());
        TargetBlockSet.ReleaseBlockInfo();
    }
    
    protected void GenerateTerrainForTerrainBlockWithId(int i) {
        TilemapTerrainBlock tilemapTerrainBlock = new TilemapTerrainBlock(i);
        tilemapTerrainBlock.targetBlockSet = defaultBlockSet;
        //将当前区块设置为目标区块
        currentTilemapTerrainBlock = tilemapTerrainBlock;
        //生成当前区块的地形
        GenerateCurrentTerrainBlock();
    }
    
    /// <summary>
    /// 生成当前需要的地形.
    /// </summary>
    public override void GenerateTerrain(){
        if(target==null)target = GameObject.FindGameObjectWithTag(TARGET_DEFAULT_NAME).GetComponent<Tilemap>();
        if(TargetBlockSet==null){
            Debug.LogError("No target block-set assigned to generator:"+this.GetType());
        }else {
            var center = GetBlockIdByPosition(generatingCenter);
            var rest = pregeneratingBlocksId.Where(x => (x <= center));
            foreach (var i in pregeneratingBlocksId.Where(x=> (x<center)).Reverse()) {

                GenerateTerrainForTerrainBlockWithId(i);
            }
          foreach (var i in pregeneratingBlocksId.Where(x=> (x>center))) {

               GenerateTerrainForTerrainBlockWithId(i);
           }
          
        }
    }

    public void ReleaseTerrainBlock(TilemapTerrainBlock tilemapTerrainBlock) {
        currentTilemapTerrainBlock = tilemapTerrainBlock;
        TargetBlockSet.seed = currentTilemapTerrainBlock.seed;
        TargetBlockSet.LetBlockGetInfo();
        foreach (var vic in TargetBlockSet.GetTerrainInfoVec3I) {
            target.SetTile(vic,null);
        }
        TargetBlockSet.ReleaseBlockInfo();
    }
    /// <summary>
    /// 释放无需的地形
    /// </summary>
    public override void ReleaseTerrain() {
        var blocksToDelete = terrainBlocks.Where(x => x.Wasted == true);
        foreach (var block in blocksToDelete) {
            ReleaseTerrainBlock(block);
            terrainBlocks.Remove(block);
        }
    }

    public List<int> GetSurroundedBlocksId(int id) {
        List<int> list = new List<int>();
        for (int i = id - PregeneratedBlockLength; i <= id + PregeneratedBlockLength; i++) {
            list.Add(i);
    
        }
        
        return list;
    }
    //找到中心区块的ID
    public int GetBlockIdByPosition(Vector3Int v3) {
        var result = FromVectorToBlock(v3);
        if (result!= null) {
            return result.BlockId;
        }
        else {
            return (v3 - defaultShifter).x/TargetBlockSet.BlockLength;
        }
    }
    public override void RefreshTerrain() {
        int centerId = GetBlockIdByPosition(generatingCenter);

        pregeneratingBlocksId = GetSurroundedBlocksId(centerId);
        foreach (var node in terrainBlocks) {
            var value = node.BlockId - centerId;
            if (value > PregeneratedBlockLength) {
                node.Wasted = true;
            }
            else {
                pregeneratingBlocksId.Remove(node.BlockId);
            }
        }
        GenerateTerrain();
        ReleaseTerrain();
    }
}
