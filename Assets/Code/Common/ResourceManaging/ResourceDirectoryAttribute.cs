using System;

namespace Code.Common.ResourceManaging
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourceDirectoryAttribute : Attribute
    {
        public string Directory { get; private set; }
        
        public ResourceDirectoryAttribute(string directory)
        {
            Directory = directory;
        }
    }
}