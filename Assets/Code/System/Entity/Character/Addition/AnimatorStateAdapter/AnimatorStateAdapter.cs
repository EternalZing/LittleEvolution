using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateAdapter : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    void Start(){
        
    }
    public void SetState(string str){
        switch(str){
            case "walk":{
                 animator.SetInteger("walk",1);
                break;
            }
            case "idle":{
                animator.SetInteger("walk",0);
                break;
            }
            case "jump":{
                animator.SetInteger("jump",1);
                break;
            }
            case "no-jump":{
                animator.SetInteger("jump",0);
                break;
            }
        }
    }
    // Update is called once per frame
    void Update(){
        
    }
}
