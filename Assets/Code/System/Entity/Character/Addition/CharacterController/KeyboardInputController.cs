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
            if(horizontalInput>0){
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            }else{
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
            }
        }else{
            this.gameObject.GetComponent<AnimatorStateAdapter>().SetState("idle");
        }    
    }
}
