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
        public static void OnVisionChanged(VisionDto vision)
        {
            Debug.Log("Vision is changed");

            var grid = vision.Grid;
            
            foreach (var position in new Vector(grid.GetLength(0), grid.GetLength(1)).Range())
            {
                BuildingManipulator.DestroyBuilding(position);
                BuildingManipulator.CreateBuilding(grid.At(position), position);
            }
        }

        public static void OnResourcesChanged(float[] value)
        {
            Ui.ShowResources(value);
        }
    }
}