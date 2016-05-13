using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Connect ();
	}
	
	// Update is called once per frame
	void Connect () {
		Debug.Log ("Connect()");
		PhotonNetwork.ConnectUsingSettings ("1.0.0");
	}

	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}

	void OnJoinedLobby(){
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnConnectedToMaster(){

		Debug.Log ("OnConnectedToMaster");
	}


	void OnPhotonRandomJoinFailed(){
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);

	}

	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");

		SpawnMyPlayer ();
	}

	void SpawnMyPlayer(){
		GameObject MyPlayerGO = (GameObject)PhotonNetwork.Instantiate ("Player", Vector3.zero, Quaternion.identity, 0);
		((MonoBehaviour)MyPlayerGO.GetComponent ("Movement")).enabled = true;

	}

}
