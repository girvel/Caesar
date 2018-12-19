using Code.Common;
using Code.Systems.Console;
using Code.Systems.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Systems.Interface
{
    public class UiController : SingletonBehaviour<UiController>, IKeyboardReader
    {
        public Text ResourcesText;

        private void Start()
        {
            Keyboard.Current.Readers.Add(this);
        }

        bool IKeyboardReader.KeyIsPressed()
        {
            var d = UnityEngine.Input.GetKeyDown(KeyCode.BackQuote);
            Terminal.Current.Enabled ^= d;
            return d;
        }
    }
}