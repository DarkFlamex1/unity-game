using UnityEngine;
using System.Collections;

public class RoomLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Sel-Explanotory
        PhotonView photonView = PhotonView.Get(this);
        //We use All Buffered to replay for new clients
        photonView.RPC("SpawnPlayer", PhotonTargets.AllBuffered);
    }
	
    //We use this to show it is RPC
    [PunRPC]
    public void SpawnPlayer() {
        Debug.Log("Player Connected");
        GameObject player = PhotonNetwork.Instantiate("testPlayer", Vector3.zero, Quaternion.identity, 0);
        //GameObject player = PhotonNetwork.Instantiate("testPlayer", Vector3.zero, Quaternion.identity, 0);
    }

}
