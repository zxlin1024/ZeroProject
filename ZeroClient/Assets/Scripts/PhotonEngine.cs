using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener {

    public static PhotonEngine Instance { get; private set; }
    public static PhotonPeer Peer { get; private set; }

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else if (Instance != this) {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start() {
        Peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        Peer.Connect("127.0.0.1:5055", "ZeroServer");
    }

    private void Update() {
        Peer.Service();
    }

    private void OnDestroy() {
        if(Peer != null && Peer.PeerState == PeerStateValue.Connected) {
            Peer.Disconnect();
        }
    }

    public void DebugReturn(DebugLevel level, string message) {
        Debug.Log(level + ":" + message);
    }

    public void OnEvent(EventData eventData) {
        
    }

    public void OnOperationResponse(OperationResponse operationResponse) {
        Peer.OpCustom(1, null, true);
    }

    public void OnStatusChanged(StatusCode statusCode) {
        Debug.Log(statusCode);
    }
}