using System.Collections.Generic;

namespace Code.Common
{
    public class System<TSystem, TComponent> : SingletonBehaviour<TSystem> where TSystem : System<TSystem, TComponent>
    {
        public List<TComponent> Subjects { get; set; }
        
        protected override void Awake()
        {
            base.Awake();
			
            Subjects = new List<TComponent>();
        }

        public virtual void Register(TComponent subject)
        {
            Subjects.Add(subject);
        }

        public virtual void Unregister(TComponent subject)
        {
            Subjects.Remove(subject);
        }
    }
}