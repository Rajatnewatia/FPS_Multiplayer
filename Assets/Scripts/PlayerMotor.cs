using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {


	ConfigurableJoint joint;
	[SerializeField]
	private Camera cam;
	float currentcamerarotation; 
    Rigidbody rigidbody;
	public float up = 5f;
	public float down = -3f;
	public float thrusterforce = 1200f;
	public float speed = 10f;
	public float turnspeed = 3f;
	float smoothinputmagnitude ;
	float smoothvelocity;
	float targetangle;
	[Header(" Spring Settings")]

	[SerializeField]
	private float  cameraRoatationLimit = 85f;


	[SerializeField]
	private float jointSpring = 20f;
	[SerializeField]
	private float jointMaxForce =  40f;

	// Use this for initialization
	void Start () {
	
		rigidbody = GetComponent<Rigidbody> ();
		joint = GetComponent<ConfigurableJoint> ();

		SetJointSettings (jointSpring);
	}

/*	public void Move( float input_x , float input_z)
	{
		Vector3 move_x = input_x * transform.right;
		Vector3 move_z = input_z * transform.forward;
		Vector3 input = new Vector3 (move_x.x, 0, move_z.z);
	   Vector3	dir = input.normalized;
		rigidbody.MovePosition (rigidbody.position + dir * speed * Time.deltaTime);
	
	}
*/
	public void Move( float input_x , float input_z)
	{
		Vector3 input = new Vector3 (input_x, 0, input_z);
		Vector3	dir = input.normalized;
		transform.Translate( dir * speed * Time.deltaTime);

	}

	public void Look_Y(float y_rot)
	{
		Vector3 rotation = new Vector3 (0, y_rot, 0) * turnspeed ;
		rigidbody.MoveRotation (rigidbody.rotation * Quaternion.Euler (rotation));

	}

	public void Look_X(float x_rot)
	{   
		float rotation =  x_rot * turnspeed ;
		currentcamerarotation -= rotation;
		currentcamerarotation = Mathf.Clamp (currentcamerarotation, -cameraRoatationLimit, cameraRoatationLimit);
		cam.transform.localEulerAngles = new Vector3 (currentcamerarotation, 0, 0);

	}
		
	public void ThrustForce(bool input)
	{
		if (input)
		{
			SetJointSettings (0f);
			Vector3 tf = thrusterforce * Vector3.up;
			rigidbody.AddForce (tf * Time.fixedDeltaTime , ForceMode.Acceleration); 
		} 
		else 
		{
			SetJointSettings (jointSpring);		
		}
	}
	private void SetJointSettings( float _jointSpring)
	{
		joint.yDrive = new JointDrive{positionSpring = _jointSpring, maximumForce = jointMaxForce };

	}
}
