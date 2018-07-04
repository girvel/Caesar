using System.Linq;
using UnityEngine;

namespace Code.Common.ResourceManaging
{
    public abstract class ResourceManager<T, TResource> : SingletonBehaviour<T> where T : ResourceManager<T, TResource>
    {
        protected override void Awake()
        {
            base.Awake();

            var subclass = typeof(T);
            var directoryAttribute =
                subclass
                    .GetCustomAttributes(false)
                    .OfType<ResourceDirectoryAttribute>()
                    .FirstOrDefault();

            var prefix = directoryAttribute == null ? "" : directoryAttribute.Directory + "/";

            foreach (
                var field 
                in subclass.GetFields().Where(f => f.GetCustomAttributes(false).OfType<ResourceAttribute>().Any()))
            {
                field.SetValue((T) this, Resources.Load(prefix + ToResourceName(field.Name), field.FieldType));
            }
        }

        public TResource GetResource(string resourceName)
        {
            return (TResource) typeof(T).GetField(ToResourceName(resourceName)).GetValue(this);
        }

        private static string ToResourceName(string fieldName)
        {
            return 
                fieldName
                    .Substring(1)
                    .Aggregate(
                        fieldName[0].ToString(), 
                        (current, character) 
                            => current + (char.IsUpper(character) 
                                   ? " " + char.ToLower(character) 
                                   : character.ToString()));
        }
    }
}