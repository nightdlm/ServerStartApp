
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WpfAppAi.Model;

namespace WpfAppAi
{
    public class ConfigOperationUtil
    {
        private static readonly string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ItemConfig.xml");

        public static Items Instance { get; set; } = new Items();

        static ConfigOperationUtil(){
            
            // 创建XML序列化器（指定目标类型）
            XmlSerializer serializer = new XmlSerializer(typeof(Items));

            // 读取文件并反序列化
            using (FileStream fs = new FileStream(configPath, FileMode.Open))
            {
                // 将流内容转换为Items对象
                Instance = (Items)serializer.Deserialize(fs);
               
            }
        }


        /// <summary>
        /// 保存配置到文件
        /// </summary>
        public static void SaveConfig()
        {
            try
            {
                if (Instance == null)
                {
                    Instance = new Items();
                }

                // 创建XML序列化器
                XmlSerializer serializer = new XmlSerializer(typeof(Items));

                // 创建命名空间，避免XML中出现默认命名空间
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                // 写入文件
                using (FileStream fs = new FileStream(configPath, FileMode.Create))
                {
                    serializer.Serialize(fs, Instance, namespaces);
                }
            }
            catch (Exception ex)
            {
                // 处理异常，记录日志等
                System.Diagnostics.Debug.WriteLine($"保存配置失败: {ex.Message}");
                throw; // 抛出异常让调用者处理
            }
        }

        /// <summary>
        /// 添加新配置项
        /// </summary>
        public static void AddItem(Item item)
        {
            if (Instance == null)
            {
                Instance = new Items();
            }

            Instance.ItemList.Add(item);
        }

        /// <summary>
        /// 移除配置项
        /// </summary>
        public static bool RemoveItem(Item item)
        {
            if (Instance == null || Instance.ItemList == null)
            {
                return false;
            }

            return Instance.ItemList.Remove(item);
        }
    }
}