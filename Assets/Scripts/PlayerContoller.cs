using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerContoller : MonoBehaviour {

	PlayerMotor playermotor;

	// Use this for initialization
	void Start () {
		
		playermotor = GetComponent<PlayerMotor> ();
	}
	
	// Update is called once per frame
	void Update () {

		playermotor.Move (Input.GetAxisRaw ("Horizontal") , Input.GetAxisRaw ("Vertical"));

		playermotor.Look_Y(Input.GetAxisRaw("Mouse X"));

		playermotor.Look_X(Input.GetAxisRaw("Mouse Y"));

		playermotor.ThrustForce (Input.GetButtonDown ("Jump"));
	}
}
