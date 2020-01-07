using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener {

    public static PhotonEngine Instance { get; private set; }
    public static PhotonPeer Peer { get; private set; }

    public class OpResponseEvent : UnityEvent<OperationResponse> { }
    public class PhotonEvent : UnityEvent<EventData> { }
    public Dictionary<OperationCode, OpResponseEvent> RequestDict { get; } = new Dictionary<OperationCode, OpResponseEvent>();
    public Dictionary<EventCode, PhotonEvent> EventDict { get; } = new Dictionary<EventCode, PhotonEvent>();

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
        PhotonEvent request;
        bool temp = EventDict.TryGetValue((EventCode)eventData.Code, out request);
        if (temp) {
            request.Invoke(eventData);
        } else {
            Debug.Log("未找到处理对象");
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse) {
        OpResponseEvent request;
        bool temp = RequestDict.TryGetValue((OperationCode)operationResponse.OperationCode, out request);
        if (temp) {
            request.Invoke(operationResponse);
        } else {
            Debug.Log("未找到处理对象");
        }
    }

    public void OnStatusChanged(StatusCode statusCode) {
        Debug.Log(statusCode);
    }

    public void AddOpResponse(OperationCode code, UnityAction<OperationResponse> action) {
        if (RequestDict.ContainsKey(code)) {
            RequestDict[code].AddListener(action);
        } else {
            RequestDict[code] = new OpResponseEvent();
            RequestDict[code].AddListener(action);
        }

    }
}

public enum OperationCode : byte {
    Test = 1,
}

public enum EventCode : byte {

}