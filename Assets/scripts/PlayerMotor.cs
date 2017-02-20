using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {

    Vector3 velocity = Vector3.zero;
    Vector3 rotation = Vector3.zero;
    Vector3 cameraRotation = Vector3.zero;


    Rigidbody rd;
    void Start()
    {
        rd = GetComponent<Rigidbody>();
    }

    //called every physics iteration
    void FixedUpdate()
    {
        PreformMovement();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    // get rotation vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }


    // get rotation vector of the camera
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }
    //preform movment pased on velocity
    void PreformMovement()
    {
        //Stop the rigidbodyofmoving there if it collides with something in the way
        rd.MovePosition(rd.position + velocity * Time.fixedDeltaTime);
      //  if (cam)
            this.transform.Rotate(cameraRotation);
    }
}
