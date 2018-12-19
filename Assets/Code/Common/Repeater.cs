using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Common
{
    public static class Repeater
    {
        private static readonly Dictionary<object, TimeSpan> Delays = new Dictionary<object, TimeSpan>();
        
        public static void Every(object register, TimeSpan delay, Action action)
        {
            if (!Delays.ContainsKey(register))
            {
                Delays[register] = TimeSpan.Zero;
            }
            
            Delays[register] += TimeSpan.FromSeconds(Time.deltaTime);

            while (Delays[register] > delay)
            {
                Delays[register] -= delay;
                action();
            }
        }

        public static void Every(TimeSpan delay, Action action)
        {
            Every(action, delay, action);
        }
    }
}