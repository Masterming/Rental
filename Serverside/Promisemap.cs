using System.Collections.Generic;
using System.Threading;

namespace Serverside
{
    static class Promisemap
    {
        private static readonly Mutex mapMutex = new Mutex();
        private static readonly Dictionary<int, PromiseMapElement> map = new Dictionary<int, PromiseMapElement>();
        private static int nextID = 0;

        //returns id of added element
        public static int Add(PromiseMapElement elem)
        {
            mapMutex.WaitOne(-1);
            map.Add(nextID, elem);
            mapMutex.ReleaseMutex();
            nextID += 1;
            return nextID - 1;
        }

        public static bool Remove(int id)
        {
            mapMutex.WaitOne(-1);
            bool ret = map.Remove(id);
            mapMutex.ReleaseMutex();
            return ret;
        }

        public static int Count()
        {
            mapMutex.WaitOne(-1);
            int ret = map.Count;
            mapMutex.ReleaseMutex();
            return ret;
        }

        public static PromiseMapElement AcquireElement(int id)
        {
            mapMutex.WaitOne(-1);
            PromiseMapElement elem = map[id];
            elem.Acquire(); //implement spinlock
            mapMutex.ReleaseMutex();
            return elem;
        }

        public static void ReleaseElement(PromiseMapElement elem)
        {
            elem.Release();
        }
    }
}
