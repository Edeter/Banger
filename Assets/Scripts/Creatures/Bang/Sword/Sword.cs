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

    public float DragSpeed = 1;
public float Deadzone;
public float AngelAmont;

float currentangularvelocity;


    
private void OnEnable() {
    if(_rigidbody==null)
            {
           _rigidbody = gameObject.GetComponent<Rigidbody>();
            }

            _rigidbody.maxAngularVelocity =100;
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

        case (States.Standing):
                 _rigidbody.angularDrag = 40;
            break;

        case (States.Moving):
                 _rigidbody.angularDrag = 5;
                 _rigidbody.AddForceAtPosition(-target.velocity*DragSpeed,pivot.position,ForceMode.Force);
            break;

        case (States.Swording):
                 _rigidbody.angularDrag = 0;

                  _rigidbody.maxAngularVelocity =Mathf.Infinity;
            break;
    }
    
    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
  
             if (Physics.Raycast(ray, out hit))
             {
                 if (Input.GetMouseButton(0))
                 {
                     //ForceTowards(_rigidbody,hit.point);
                     RotateTowards();
                     Current = States.Swording;
                     
                 }else{
                     if (currentangularvelocity!=0)
             {
                 currentangularvelocity/=1.1f;
             }
                 }
             }
             
}

float Alpha()
{
return Vector3.SignedAngle(hit.point - body.gameObject.transform.position, pivot.position - body.gameObject.transform.position, Vector3.up);
}
void RotateTowards()
{
    bool AlphaGreater = Alpha() > Deadzone;
    bool AlphaLesser = Alpha() < -Deadzone;
    
    if (AlphaGreater)
    {
        currentangularvelocity-= AngelAmont;
    }
    else if (AlphaLesser)
    {
         currentangularvelocity+=AngelAmont;
    }
    _rigidbody.angularVelocity = new Vector3(0,currentangularvelocity,0);
}


private void OnGUI() {
    GUI.Label(new Rect(0,0,100,100),Vector3.SignedAngle(hit.point - body.gameObject.transform.position, pivot.position - body.gameObject.transform.position, Vector3.up).ToString());
    GUI.Label(new Rect(0,15,100,100),_rigidbody.angularVelocity.ToString());

}

private void OnDrawGizmos() {
    Gizmos.DrawWireSphere(pivot.position,3);
    Gizmos.DrawWireSphere(hit.point,3);
    //Gizmos.DrawWireSphere(_rigidbody.transform.position,3);
    
    Gizmos.color = Color.magenta;
    Gizmos.DrawLine(body.gameObject.transform.position,pivot.position);

    
    Gizmos.color = Color.blue;
    Gizmos.DrawLine(body.gameObject.transform.position,hit.point);
}
}
