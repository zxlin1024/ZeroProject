using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroServer {
    //所有的Server端主类都要继承自ApplicationBase
    class ZeroServer : ApplicationBase {

        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public static ZeroServer _Instance {
            get;
            private set;
        }

        //当一个客户端请求连接时调用，我们使用一个PeerBase表示Server端和一个客户端的连接，用来管理Server端与客户端请求的发送与接收
        protected override PeerBase CreatePeer(InitRequest initRequest) {
            log.Info("A client connected。。。。");
            Client client = new Client(initRequest);
            return client;
        }

        //Seerver端启动的时候调用， 作初始化
        protected override void Setup() {
            _Instance = this;

            //日志的初始化
            //this.ApplicationRootPath获取PhotonServer应用的根目录（deploy）,Path.Combine()会屏蔽平台差异
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(Path.Combine(this.ApplicationRootPath, "bin_Win64"), "log");
            //this.BinaryPath 获取输出路径（deploy\ZeroServer\bin）, FileInfo 需using System.IO;
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (configFileInfo.Exists) {
                //让Photon知道使用的是Log4Net的日志插件，需using ExitGames.Logging.Log4Net;
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                //让log4net这个插件读取配置文件，需using log4net.Config;
                XmlConfigurator.ConfigureAndWatch(configFileInfo);
            }
            //日志输出
            log.Info("Setup Completed！");
        }

        //Server端关闭的时候调用
        protected override void TearDown() {
            log.Info("TearDown Completed！");
        }
    }
}