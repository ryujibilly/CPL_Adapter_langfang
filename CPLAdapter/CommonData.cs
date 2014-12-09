using System;
using System.Collections.Generic;
using System.Text;

namespace GLAS_Adapter
{
    class CommonData
    {
        /// <summary>
        /// 井深
        /// </summary>
        public static float WellDepth = -3.14f;
        /// <summary>
        /// 压力数组队列
        /// </summary>
        private static Queue<byte[]> SppQueue = new Queue<byte[]>();
        private static Queue<byte[]> DepQueue = new Queue<byte[]>();
        private static object QueueObj = new object();
        private static object QueueObjt = new object();

        /// <summary>
        /// 获取列表中一个元素
        /// </summary>
        /// <returns></returns>
        public static byte[] GetQueueItem()
        {
            byte[] bytesRet = null;
            lock (QueueObj)
            {
                if (SppQueue.Count > 0)
                {
                    bytesRet = SppQueue.Dequeue();
                }
            }
            return bytesRet;
        }
        public static byte[] GetDepthItem()
        {
            byte[] bytesRet = null;
            lock (QueueObjt)
            {
                if (DepQueue.Count > 0)
                {
                    bytesRet = DepQueue.Dequeue();
                }
            }
            return bytesRet;
        }

        /// <summary>
        /// 向队列添加Spp元素
        /// </summary>
        /// <param name="item"></param>
        public static void SaveSppQueue(byte[] item)
        {
            lock (QueueObj)
            {
                if (SppQueue.Count > 10)
                {
                    SppQueue.Dequeue();
                }
                SppQueue.Enqueue(item);
            }
        }
        /// <summary>
        /// 向队列添加BPI元素
        /// </summary>
        /// <param name="item"></param>
        public static void SaveDepthQueue(byte[] item)
        {
            lock (QueueObjt)
            {
                if (DepQueue.Count > 4)
                {
                    DepQueue.Dequeue();
                }
                DepQueue.Enqueue(item);
            }
        }

        public static void ClearQueue()
        {
            lock (QueueObj)
            {
                if (SppQueue.Count > 0)
                {
                    SppQueue.Clear();
                }
            }
            lock(QueueObjt)
            {
                if (DepQueue.Count > 0)
                {
                    DepQueue.Clear();
                }
            }
        }
        public static void ClearSpp()
        {
            lock (QueueObj)
            {
                if (SppQueue.Count > 0)
                {
                    SppQueue.Clear();
                }
            }
        }

        public static void ClearDep()
        {
            lock (QueueObj)
            {
                if (DepQueue.Count > 0)
                {
                    DepQueue.Clear();
                }
            }
        }
    }
}
