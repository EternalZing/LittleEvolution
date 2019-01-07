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
    public int IslandMinHeight;
    public int IslanMinLength;
    public int BlockLength = 35;
    //生成结果
    private  List<Vector3Int> generatorInfoVector3 = new List<Vector3Int>();
    private  List<TileBase> generatorInfoTileBase = new List<TileBase>();
    
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

    public void GeneratingSlope() {
        int left = Random.Range(2, BlockLength-7); ;

        int right = Random.Range(3, 7);
 
        for (int i = left;i <= left+right; i++) {
            generatorInfoVector3.Add(new Vector3Int(i,averageAltitude+Random.Range(1,2),0));
            generatorInfoTileBase.Add(floor);
        }
    }
    public void GeneratingFloatIsland(){
        int height = Random.Range(IslandMinHeight,IslandMinHeight+5);
        int left = Random.Range(0, BlockLength/2); ;
        int maxLength = BlockLength - left-1;
        int islandLength =  Random.Range(IslanMinLength,maxLength);
        for(int i=left;i<=left+islandLength;i++){
            generatorInfoVector3.Add(new Vector3Int(i,averageAltitude+height,0));
            generatorInfoTileBase.Add(floatingIsland);
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
                 GeneratingSlope();
                 GeneratingFloatIsland();
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
        InfoCompletionCheck();
        Random.InitState(seed.GetHashCode());
        Init();
    }

    public void ReleaseBlockInfo() {
        initialized = false;
    }
}