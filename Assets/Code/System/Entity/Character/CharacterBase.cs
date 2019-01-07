using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : LivingEntity{
    public AnimatorStateAdapter animatorAdapter;
    protected const float FACING_BASE = 90f;
    protected const float FACING_BAIS = 90f;
    protected const float JUMP_SCALE = 5f;

    protected bool grounded = false;
    public new void Start(){
        movingSpeed = 7;
    }

    // Update is called once per frame
    public void TurnFace(int direction){
        if(this.FacingDirection !=  direction){
            this.gameObject.transform.rotation =  Quaternion.Euler(new Vector3(0,FACING_BASE-FACING_BAIS*direction,0));        
            this.FacingDirection = direction;    
        }

    }
    public void Idle(){
        animatorAdapter.SetState("idle");
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,GetComponent<Rigidbody2D>().velocity.y,0);
    }
    public override void Move(){
        animatorAdapter.SetState("walk");
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(FacingDirection*movingSpeed,GetComponent<Rigidbody2D>().velocity.y,0);
    }    
    public void Jump(){
        if(this.grounded ==true){
            animatorAdapter.SetState("jump");
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*JUMP_SCALE,ForceMode2D.Impulse);
            this.grounded = false;
        }else{
           
        }
    }
    public void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("Terrain")){
            this.animatorAdapter.SetState("no-jump");
            this.grounded = true;
        }
 
    }
}
