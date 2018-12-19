using System.Collections.Generic;
using Code.Common;
using Code.Systems.Interface;
using UnityEngine;

namespace Code.Systems.Input
{
    public class Keyboard : SingletonBehaviour<Keyboard>
    {
        public List<IKeyboardReader> Readers { get; set; }

        

        protected override void Awake()
        {
            base.Awake();
            
            Readers = new List<IKeyboardReader>();
        }

        private void Update()
        {
            if (UnityEngine.Input.anyKeyDown && Readers != null)
            {
                for (var i = 0; i < Readers.Count; i++)
                {
                    if (Readers[i].KeyIsPressed()) break;
                }
            }
        }
    }
}