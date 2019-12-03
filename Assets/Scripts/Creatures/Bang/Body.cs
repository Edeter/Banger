using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Body : Creature
{
    Rigidbody rb;
    public float MovementMagnitude =1;
    
 


    
private void OnEnable() {
    if(rb==null)
            {
            rb = gameObject.GetComponent<Rigidbody>();
            }
        }

void MoveDir(Vector3 dir)
{
    rb.velocity = dir*MovementMagnitude;
    //rb.MovePosition(transform.position+dir*MovementMagnitude);
    //rb.AddForce(dir*MovementMagnitude,ForceMode.Force);
}

Vector3 xaxis;
Vector3 yaxis;
private void Update() {
 if(Input.anyKey)
 {
     MoveDir(new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")));
    Current = States.Moving;
 }
 else
 { 
    Current= States.Standing;
 
 }

    switch(Current){
        case (States.Standing): rb.velocity = new Vector3(0,0,0);
        break;
        case (States.Moving):
        break;
        
    }


}
private void OnDrawGizmos() {
    Gizmos.color = Color.green;
    Gizmos.DrawLine(transform.position,transform.position+new Vector3(0,0,10));
}

}
