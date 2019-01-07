using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine;
/// <summary>
/// 这个类只是存储了Block区块的基本类型信息,两个生成结果只是用来转交给外部使用的.
/// </summary>
[CreateAssetMenu(fileName = "BlockSet", menuName = "LittleEvolution/BlockSet", order = 0)]

public class BlockSet : ScriptableObject {
    //基本配置信息
    public TileBase floor;
    public TileBase dirt;
    public TileBase ladder;
    public TileBase floatingIsland;
    public int averageAltitude;

    public int BlockLength = 17;
    //生成结果
    private  List<Vector3Int> generatorInfoVector3;
    private  List<TileBase> generatorInfoTileBase;
    
    //随机种子
    
    public string seed;
    
    //表层地形
    private void GeneratingFloorInfo(){
        for(int i=0;i<BlockLength;i++){
            for(int j=averageAltitude-1;j>=0;j--){
                generatorInfoVector3.Add(new Vector3Int(i,j,0));
                generatorInfoTileBase.Add(dirt);
            }
            generatorInfoVector3.Add(new Vector3Int(i,averageAltitude,0));
            generatorInfoTileBase.Add(floor);
        }
    }
    
    //生成标记
    private bool initialized =false;
    //初始化地形信息.
    
    //初始化生成
    private void Init(){
        if(initialized==false){
                 generatorInfoTileBase.Clear();
                 generatorInfoVector3.Clear();
                 GeneratingFloorInfo();
                 initialized = true;
        }else{
            Debug.LogError("Terrian Already initilized before");
        }
    }
    
    //坐标信息
    public List<Vector3Int> GetTerrainInfoVec3I{
        get{
            return generatorInfoVector3;
  
        }
    }
    public bool InfoCompletionCheck() {
        if (seed.Trim().Equals("")) {
            Debug.LogError(string.Format("BlockSet {0} has no random seed.Have you ever assigned a block to it?",this.name));
        }

        if (ladder == null) {
            Debug.LogWarning(string.Format("BlockSet {0} has no ladder tiles.",this.name));
        }

        if (floor == null) {
            Debug.LogWarning(string.Format("BlockSet {0} has no floor tile,Are you sure you want to make a no-floor terrain?.",this.name));
        }
        return !(seed.Trim().Equals(""));
    }
    //Tile信息
    public List<TileBase> GetTerrianInfoTileBase{
        get{
            return generatorInfoTileBase;
        }
    }
    //释放当前的blockset属性以期待下次调用.
    public void LetBlockGetInfo() {
        initialized = false;
        Init();
    }

    public void ReleaseBlockInfo() {
        initialized = false;
    }
}