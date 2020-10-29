using System.Collections.Concurrent;
using System.Threading;

namespace Serverside
{
    /// <summary>
    /// Global, synchronized queue for server calls.
    /// </summary>
    static class Promisemap
    {
        private static Mutex mapMutex = new Mutex();
        private static ConcurrentDictionary<int, PromiseMapElement> map = new ConcurrentDictionary<int, PromiseMapElement>();
        private static int nextID = 0;

        //returns id of added element
        public static int Add(PromiseMapElement elem)
        {
            mapMutex.WaitOne(-1);
            map.TryAdd(nextID, elem);
            mapMutex.ReleaseMutex();
            nextID += 1;
            return nextID - 1;
        }

        public static bool Remove(int id)
        {
            mapMutex.WaitOne(-1);

            bool ret = false;

            if (map[id].Acquire(100))
            {
                ret = map.TryRemove(id, out _);
            }

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
