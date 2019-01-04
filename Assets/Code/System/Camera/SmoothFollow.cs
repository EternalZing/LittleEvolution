using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour{
    // Start is called before the first frame update

     
     public float dampTime = 0.15f;
     private Vector3 velocity = Vector3.zero;
     public Transform target;
 
     // Update is called once per frame
     void Update () 
     {
         if (target)
         {
             Vector3 point = this.gameObject.GetComponent<Camera>().WorldToViewportPoint(target.position);
             Vector3 delta = target.position - this.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
             Vector3 destination = transform.position + delta;
             transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
         }
     
    }
}
