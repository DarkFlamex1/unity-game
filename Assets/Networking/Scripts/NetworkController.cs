﻿using UnityEngine;
using System.Collections;

public class NetworkController : Photon.MonoBehaviour {

    public MouseLook mousex;
    public MouseLook mousey;
    public Camera camplayer;
    public TempController controllerScript;

    void Awake()
    {
        mousex = GetComponent<MouseLook>();
        //mousey = GetComponentInChildren<MouseLook>();
        camplayer = GetComponentInChildren<Camera>();
        controllerScript = GetComponent<TempController>();

        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            mousex.enabled = true;
            mousey.enabled = true;
            camplayer.enabled = true;
            controllerScript.enabled = true;
            gameObject.name = PhotonNetwork.playerName;
        }
        else
        {
            mousex.enabled = false;
            mousey.enabled = false;
            camplayer.enabled = false;
            controllerScript.enabled = false;
        }

        
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //Network player, receive data
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
            Debug.Log(correctPlayerPos);
            Debug.Log(correctPlayerRot);
        }
    }

    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this

    void Update()
    {
        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            Debug.Log("Lerping");
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
        }
    }
}
