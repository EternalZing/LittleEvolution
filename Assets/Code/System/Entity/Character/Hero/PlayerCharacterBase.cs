using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterBase : CharacterBase
{
    // Start is called before the first frame update
    private const float DASH_SCALE = 4f;
    protected bool dashing = false;
    protected float dashingTime = 0.5f;
    IEnumerator DashStop(){
        yield return new WaitForSecondsRealtime(0.5f);
        dashing = false;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        animatorAdapter.SetState("no-dash");
    }
    public void Dash(){
        animatorAdapter.SetState("dash");
        if(dashing==false){
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(FacingDirection*movingSpeed*DASH_SCALE,GetComponent<Rigidbody2D>().velocity.y,0);
            animatorAdapter.SetState("dash");
            dashing  = true;
            StartCoroutine(DashStop());
        }
    
    }
    public override void Move(){
        if(dashing==false){
            base.Move();
        }
            
    }
    // Update is called once per frame
    void Update(){

    }
}
