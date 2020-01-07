using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroServer {
    class Client : ClientPeer {
        public delegate void OpRequest<T1, T2>(T1 t1, T2 t2);

        public Dictionary<OperationCode, OpRequest<OperationRequest, SendParameters>> OpRequestDict = new Dictionary<OperationCode, OpRequest<OperationRequest, SendParameters>>();

        public Client(InitRequest initRequest) : base(initRequest) {
            OpRequestDict.Add(OperationCode.Test, TestPhoton);
        }

        //断开连接
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail) {
            ZeroServer.log.Info("A client Disconnected.");
        }

        //处理请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters) {
            OpRequest<OperationRequest, SendParameters> request;
            bool temp = OpRequestDict.TryGetValue((OperationCode)operationRequest.OperationCode, out request);
            if (temp) {
                request.Invoke(operationRequest, sendParameters);
            } else {
                Console.WriteLine("未找到处理对象");
            }
        }
        public Dictionary<byte, object> dic = new Dictionary<byte, object>();
        public void TestPhoton(OperationRequest operationRequest, SendParameters sendParameters) {
            ZeroServer.log.Info(operationRequest.Parameters[1]);
            dic[1] = "tytytyty";
            SendOperationResponse(new OperationResponse() { OperationCode = 1,  Parameters = dic }, sendParameters);
        }
    }
}