using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Creature
{
    Rigidbody _rigidbody;

    public Rigidbody target;

    public Transform pivot;
 

    public Body body;
    public float SwordMagnitude = 1;


    
private void OnEnable() {
    if(_rigidbody==null)
            {
           _rigidbody = gameObject.GetComponent<Rigidbody>();
            }

            _rigidbody.maxAngularVelocity =1;
}

void ForceTowards(Rigidbody rb,Vector3 des)
{
    rb.AddForceAtPosition(des*SwordMagnitude,pivot.position,ForceMode.Force);
}

//  public void rotateRigidBodyAroundPointBy(Rigidbody rb, Vector3 origin, Vector3 axis, float angle)
//  {
//      Quaternion q = Quaternion.AngleAxis(angle, axis);
//      rb.MovePosition(q * (rb.transform.position - origin) + origin);
//      rb.MoveRotation(rb.transform.rotation * q);
//      rb.transform.LookAt(pivot.position,Vector3.forward);
//  }

Ray ray;
RaycastHit hit;
private void Update() {
    Current = body.Current;

    switch(Current){
        case (States.Standing): _rigidbody.angularDrag = Mathf.Infinity;
        break;
        case (States.Moving): _rigidbody.angularDrag = 5;
        _rigidbody.AddForceAtPosition(-target.velocity,pivot.position,ForceMode.Force);
        break;
        case(States.Swording): _rigidbody.angularDrag = 0;
        break;
    }
    
    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
  
             if (Physics.Raycast(ray, out hit))
             {
                 if (Input.GetMouseButton(0))
                 {
                     ForceTowards(_rigidbody,hit.point);
                     Current = States.Swording;
                 }
                 
                 

                 //Debug.Log(hit);
             }
}

private void OnGUI() {
    GUI.Label(new Rect(0,0,100,100),hit.point.ToString());
}

private void OnDrawGizmos() {
    Gizmos.DrawWireSphere(pivot.position,3);
    Gizmos.DrawWireSphere(hit.point,3);
    Gizmos.DrawWireSphere(_rigidbody.transform.position,3);
}
}
