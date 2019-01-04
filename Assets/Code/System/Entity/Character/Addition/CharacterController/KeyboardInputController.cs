using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KeyboardInputController : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void keyboardInputCallback(GameObject gameObject,float value);
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if(horizontalInput!=0){
            /*暂时的写法 */
            this.gameObject.GetComponent<AnimatorStateAdapter>().SetState("walk");
            gameObject.GetComponent<CharacterBase>().TurnFace(horizontalInput>0?1:-1);
            gameObject.GetComponent<CharacterBase>().Move();
        }else{
             this.gameObject.GetComponent<AnimatorStateAdapter>().SetState("idle");
            gameObject.GetComponent<CharacterBase>().Idle();
        }
        if(Input.GetButtonDown("Jump")){
            this.gameObject.GetComponent<CharacterBase>().Jump();
            this.gameObject.GetComponent<AnimatorStateAdapter>().SetState("jump");
        }
    }
}
