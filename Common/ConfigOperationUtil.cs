
using System.IO;
using System.Xml.Serialization;
using WpfAppAi.Model;

namespace WpfAppAi
{
    public class ConfigOperationUtil
    {
        private static readonly string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ItemConfig.xml");

        public static Items Instance { get; set; } = new Items();

        static ConfigOperationUtil(){
            
            // 判断文件是否存在
            if (!File.Exists(configPath))
            {
                // 如果文件不存在，创建一个空的Items对象并保存到文件中
                Items emptyItems = new Items();
                XmlSerializer serializer = new XmlSerializer(typeof(Items));
                using (FileStream fs = new FileStream(configPath, FileMode.Create))
                {
                    serializer.Serialize(fs, emptyItems);
                }
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
        public static void AddItem()
        {
            if (Instance == null)
            {
                Instance = new Items();
            }

            Instance.ItemList.Add(new Item());
            
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

        /// <summary>
        /// 将指定项上移一位
        /// </summary>
        /// <param name="item">要移动的项</param>
        /// <returns>如果移动成功则返回true，否则返回false</returns>
        public static bool MoveItemUp(Item item)
        {
            if (Instance == null || Instance.ItemList == null)
                return false;

            int currentIndex = Instance.ItemList.IndexOf(item);
            // 检查是否已经在最顶部
            if (currentIndex <= 0)
                return false;

            // 移除当前项并插入到上一个位置
            Instance.ItemList.RemoveAt(currentIndex);
            Instance.ItemList.Insert(currentIndex - 1, item);
            return true;
        }

        /// <summary>
        /// 将指定项下移一位
        /// </summary>
        /// <param name="item">要移动的项</param>
        /// <returns>如果移动成功则返回true，否则返回false</returns>
        public static bool MoveItemDown(Item item)
        {
            if (Instance == null || Instance.ItemList == null)
                return false;

            int currentIndex = Instance.ItemList.IndexOf(item);
            // 检查是否已经在最底部
            if (currentIndex == -1 || currentIndex >= Instance.ItemList.Count - 1)
                return false;

            // 移除当前项并插入到下一个位置
            Instance.ItemList.RemoveAt(currentIndex);
            Instance.ItemList.Insert(currentIndex + 1, item);
            return true;
        }
    }
}