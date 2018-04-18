using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour{

	[SerializeField]
	Behaviour[] componentstoDisable;

	[SerializeField]
	string remoteLayerName = "RemotePlayer";

	Camera Scenecamera;

	void Start()
	{

		if (!isLocalPlayer) 
			{
				DisableComponents ();
				AssignRemoteLayer ();
			}
		else
		{
			Scenecamera = Camera.main;
			if (Scenecamera != null) 
			{
				Scenecamera.gameObject.SetActive (false);

			}
				
		}

		GetComponent<Player> ().Setup();
	
	}
	public override void OnStartClient()
	{

		string _netID = GetComponent<NetworkIdentity>().netId.ToString();
		Player _player = GetComponent<Player>();

		base.OnStartClient ();

		GameManager.RegisterPlayer(_netID, _player);
	}

	void AssignRemoteLayer()
	{
		gameObject.layer = LayerMask.NameToLayer (remoteLayerName);
	}

	void DisableComponents()
	{
		for (int i = 0; i < componentstoDisable.Length; i++) 
		{
			componentstoDisable [i].enabled = false;
		}
	}

	void OnDisable()
	{

		if (Scenecamera != null) 
		{
			Scenecamera.gameObject.SetActive (true);
		}

		GameManager.UnRegisterPlayer (transform.name);
	}
}