using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPhoton : MonoBehaviour 
{
    public Dictionary<byte, object> dic = new Dictionary<byte, object>();

    private void Start() {
        PhotonEngine.Instance.AddOpResponse(OperationCode.Test, PhotonTest);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            dic[1] = Time.time.ToString();
            PhotonEngine.Peer.OpCustom(1, dic, true);
        }
    }

    void PhotonTest(OperationResponse response) {
        Debug.Log(response.Parameters[1]);
    }
}
