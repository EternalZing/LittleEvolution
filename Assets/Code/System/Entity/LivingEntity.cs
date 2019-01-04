using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : EntityBase
{
    // Start is called before the first frame update
    public int FacingDirection{get;set;}
    public float movingSpeed = 0;

    public Dictionary<string,float> additionalStatus;
    public virtual void Move(){

    }
    public virtual void TargetTo(){
        
    }

}
