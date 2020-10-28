using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using SerializeLib;

namespace Serverside
{
    enum State : short
    {
        uninitialized = 0,
        initialized = 1,
        handled = 2,
        ready = 3
    }
    class PromiseMapElement
    {
        private Mutex elementMutex = new Mutex();
        private State state = State.uninitialized;
        public readonly TcpClient client;
        public readonly Request request;
        private Response response;

        public PromiseMapElement(TcpClient c, Request r)
        {
            client = c;
            request = r;
            state = State.initialized;
        }
        public bool Acquire(int duration = -1)
        {
            return elementMutex.WaitOne(duration);
        }

        public void Release()
        {
            elementMutex.ReleaseMutex();
        }

        public void ToggleState()
        {
            if ((int)state < 3)
                state++;
            else
                throw new Exception("stateOverflow in PromisMapElement");
        }

        public Response getResponse()
        {
            return response;
        }

        public void setResponse(Response _res)
        {
            if(response == null)
            {
                response = _res;
            }
        }

        public State GetState()
        {
            return state;
        }
    }
}
