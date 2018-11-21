using Code.Common;
using Code.Systems.Console;
using UnityEngine;

namespace Code.Systems.Ui
{
    public class UiController : SingletonBehaviour<UiController>
    {
        private void Update()
        {
            Terminal.Current.Enabled ^= Input.GetKeyDown(KeyCode.BackQuote);
        }
    }
}