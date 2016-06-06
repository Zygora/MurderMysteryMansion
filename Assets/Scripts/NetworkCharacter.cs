using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {

    public Vector3 realPosition = Vector3.zero;
    public Vector3 positionAtLastPacket = Vector3.zero;
    public double currentTime = 0.0;
    public double currentPacketTime = 0.0;
    public double lastPacketTime = 0.0;
    public double timeToReachGoal = 0.0;
    public Camera maincam;
    
    Animator anim;
    SpriteRenderer spRend;
    string playerName;
    string playerClass;

    // Use this for initialization
    void Start () {
        //  realPosition = transform.position;
        PhotonNetwork.sendRate = 20;
        PhotonNetwork.sendRateOnSerialize = 10;
        anim = GetComponent<Animator>();
        spRend = GetComponent<SpriteRenderer>();
        playerName = GetComponentInParent<PlayerManager>().playerName.ToString();
        playerClass = GetComponentInParent<PlayerManager>().playerClass.ToString();
        maincam = (Camera)GameObject.FindObjectOfType(typeof(Camera));
    }
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine)
        {
            //do nothing
            maincam.transform.position = new Vector3(transform.position.x,transform.position.y,-10);

        }
        else
        {
            timeToReachGoal = currentPacketTime - lastPacketTime;
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(positionAtLastPacket, realPosition, (float)(currentTime / timeToReachGoal));
        }
       
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting) // Our player. We need to send our actual position to the network.
        {
            stream.SendNext(transform.position);
            stream.SendNext(anim.GetBool("Running"));
            stream.SendNext(anim.GetBool("Idle"));
            stream.SendNext(spRend.flipX);
            stream.SendNext(playerName);
            stream.SendNext(playerClass);
        }
        else
        {
            //this is someone else's player. We need to receive their position (as of a few milliseconds ago, and update our version of that player.
            currentTime = 0.0;
            positionAtLastPacket = transform.position;
            realPosition = (Vector3)stream.ReceiveNext();
            lastPacketTime = currentPacketTime;
            currentPacketTime = info.timestamp;
            anim.SetBool("Running",(bool)stream.ReceiveNext());
            anim.SetBool("Idle",(bool)stream.ReceiveNext());
            spRend.flipX = (bool)stream.ReceiveNext();
            //Add player 
            playerName = (string)stream.ReceiveNext();
            playerClass = (string)stream.ReceiveNext();

        }

    }

    void OnGUI()
    {

            GUI.Label(new Rect(50,50,500,500), "Player:" + PhotonNetwork.player.ID);
        
    }

    void SerializeState(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {

        }
        else
        {

        }
    }
}
