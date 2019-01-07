using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : ScriptableObject{

    public virtual IEnumerator  GenerateTerrain(){
        return null;
    }
    public virtual IEnumerator  ReleaseTerrain(){
        return null;
    }
    public virtual IEnumerator  RefreshTerrain(){
        return null;
    }
    public virtual void Init(){

    }
}
