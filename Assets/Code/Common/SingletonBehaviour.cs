using UnityEngine;

namespace Code.Common
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        public static T Current;

        protected virtual void Awake()
        {
            Current = (T) this;
        }
    }
}

