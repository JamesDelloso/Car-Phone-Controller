using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTracker : MonoBehaviourPun, IPunObservable
{

    public CarMovement carMovement;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            carMovement = GameObject.Find("Car").GetComponent<CarMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting && !info.Sender.IsMasterClient)
        {
            stream.SendNext(Input.acceleration);
            stream.SendNext(Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width / 2);
            stream.SendNext(Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width / 2);
        }
        else
        {
            carMovement.userAcceleration = (Vector3)stream.ReceiveNext();
            carMovement.isAccelerating = (bool)stream.ReceiveNext();
            carMovement.isBraking = (bool)stream.ReceiveNext();
        }
    }
}
