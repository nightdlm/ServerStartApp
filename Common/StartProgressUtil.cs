
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WpfAppAi.Model;

namespace WpfAppAi
{
    public class StartProgressUtil
    {
        private static readonly string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ItemConfig.xml");

        public static Items Instance { get; set; } = new Items();

        static StartProgressUtil(){
            
            // 创建XML序列化器（指定目标类型）
            XmlSerializer serializer = new XmlSerializer(typeof(Items));

            // 读取文件并反序列化
            using (FileStream fs = new FileStream(configPath, FileMode.Open))
            {
                // 将流内容转换为Items对象
                Instance = (Items)serializer.Deserialize(fs);
            }
        }

    }
}