using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroServer {
    class Client : ClientPeer {

        public Client(InitRequest initRequest) : base(initRequest) {

        }

        //断开连接
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail) {
            ZeroServer.log.Info("A client Disconnected.");
        }

        //处理请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters) {
            
        }
    }
}
