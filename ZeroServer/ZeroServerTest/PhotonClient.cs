using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroServerTest {
    class PhotonClient : IPhotonPeerListener {
        public void DebugReturn(DebugLevel level, string message) {
            throw new NotImplementedException();
        }

        public void OnEvent(EventData eventData) {
            throw new NotImplementedException();
        }

        public void OnMessage(object messages) {
            throw new NotImplementedException();
        }

        public void OnOperationResponse(OperationResponse operationResponse) {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(StatusCode statusCode) {
            throw new NotImplementedException();
        }
    }
}
