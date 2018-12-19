using System.Linq;
using Code.Common;
using Code.Systems.Interface;
using Imperium.CommonData;
using Province.Vector;
using UnityEngine;

namespace Code.Systems.Net
{
    public static class NewsContainer
    {
        public static void OnEntityCreate(BuildingDto dto, Vector position)
        {
            BuildingManipulator.CreateBuilding(dto, position);
        }

        public static void OnEntityDestroy(string name, Vector position)
        {
            BuildingManipulator.DestroyBuilding(name, position);
        }

        public static void OnResourcesChanged(float[] value)
        {
            Ui.ShowResources(value);
        }
    }
}