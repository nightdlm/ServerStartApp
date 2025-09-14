using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppAi.Model;

namespace WpfAppAi.Common
{
    class ServerOperationUtil
    {

        public static ServerInfoItems Instance { get; set; } = new ServerInfoItems();


        // 添加服务项的方法
        public static void AddServerInfoItem(ServerInfoItem item)
        {
            Instance.ServerInfoItemList.Add(item);
        }

        // 移除服务项的方法
        public void RemoveServerInfoItem(ServerInfoItem item)
        {
            Instance.ServerInfoItemList.Remove(item);
        }

        // 清理所有数据
        public static void ClearAllData()
        {
            Instance.ServerInfoItemList.Clear();
        }

    }
}
