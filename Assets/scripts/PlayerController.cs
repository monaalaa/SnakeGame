using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float rotationSnsativity = 3f;
    PlayerMotor motor;
	// Use this for initialization
	void Start () {
        motor = GetComponent<PlayerMotor>();
    }
	
	// Update is called once per frame
	void Update () {
        float _Xvalue = Input.GetAxisRaw("Horizontal");
        float _Zvalue = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _Xvalue;
        Vector3 _moveVertical = transform.forward * _Zvalue;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

        motor.Move(_velocity);

        //Calculate rotation
        float xrot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0, xrot, 0)*rotationSnsativity;

        //Apply rotation
        motor.Rotate(_rotation);

        float Yrot = Input.GetAxisRaw("Mouse Y");
        Vector3 _camerarotation = new Vector3(xrot, 0, 0) * rotationSnsativity;

        //Apply camera rotation
        motor.RotateCamera(_rotation);
    }
}
