﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Serverside
{
    static class Promisemap
    {
        private static Mutex mapMutex = new Mutex();
        private static Dictionary<int, PromiseMapElement> map = new Dictionary<int, PromiseMapElement>();
        private static int nextID = 0;

        public static void Add(PromiseMapElement elem)
        {
            mapMutex.WaitOne(-1);
            map.Add(nextID++, elem);
            mapMutex.ReleaseMutex();
        }

        public static bool Remove(int id)
        {
            mapMutex.WaitOne(-1);
            bool ret = map.Remove(id);
            mapMutex.ReleaseMutex();
            return ret;
        }

        public static int count()
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
            elem.Acquire();
            mapMutex.ReleaseMutex();
            return elem;
        }

        public static void ReleaseElement(PromiseMapElement elem)
        {
            elem.Release();
        }
    }
}
