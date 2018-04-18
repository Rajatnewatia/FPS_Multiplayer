using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

	public PlayerWeapon weapon;

	private const string PLAYERTAG = "Player";

	[SerializeField]
	private Camera Cam;

	[SerializeField]
	private LayerMask mask ;

	void Start()
	{
		if (Cam == null) 
		{
			Debug.LogError (" PlayerShoot : No camera referenced");
			this.enabled = false;
		}
	}

	void Update()
	{
		if(Input.GetButtonDown("Fire1"))
			{
			   Shoot ();
			}
	}

	[Client]
	void Shoot()
	{
		RaycastHit hit ;

		if (Physics.Raycast (Cam.transform.position, Cam.transform.forward, out hit, weapon.range, mask)) 
		{
			if (hit.collider.tag == PLAYERTAG) 
			{
				CmdPlayerShot (hit.collider.name , weapon.damage);
			}
		}
	}

	[Command]
	void CmdPlayerShot ( string _playerID , int damage)
	{
		Debug.Log (_playerID + " has been shot");
		Player _player = GameManager.GetPlayer (_playerID);
		_player. RpcTakeDamage(damage);
	}

}
