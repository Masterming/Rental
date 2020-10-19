using System;
using System.Collections.Generic;
using System.Text;

namespace Serverside
{
    class Responses
    {
        private Dictionary<string, Func<string>> actions;

        public Responses()
        {
            actions = new Dictionary<string, Func<string>>();
        }

        ///<summary>
        /// Add new function to be executed as response to the user.
        /// Returns false if correspondig key is already used.
        ///</summary>
        public bool Add(string s, Func<string> f)
        {
            try
            {
                actions.Add(s, f);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        ///<summary>
        /// Remove existing function.
        /// Returns false if correspondig key is not found.
        ///</summary>
        public bool Remove(string s)
        {
            return actions.Remove(s);
        }


        ///<summary>
        /// Execute the Function and return the string value.
        /// Returns empty string if key is not found.
        ///</summary
        public string ExecuteFunc(string s)
        {
            if (actions.TryGetValue(s, out Func<string> f))
            {
                return f();
            }
            else
            {
                return null;
            }
        }
    }
}
