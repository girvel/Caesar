using System.Linq;
using Code.Common;
using Province.Vector;
using UnityEngine;

namespace Code.Systems.Net
{
    public static class NewsContainer
    {
        public static void OnEntityCreate(string name, Vector position)
        {
            BuildingManipulator.CreateBuilding(name, position);
        }

        public static void OnEntityDestroy(string name, Vector position)
        {
            BuildingManipulator.DestroyBuilding(name, position);
        }

        public static void OnResourcesChanged(float[] value)
        {
            Debug.Log(value.Aggregate("", (sum, r) => sum + ", " + r).Substring(2));
        }
    }
}