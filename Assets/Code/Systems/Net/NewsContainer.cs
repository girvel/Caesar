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
            
            foreach (var position in grid.Size().Range())
            {
                if (!vision.Visibility.GetAt(position))
                {
                    BuildingManipulator.HideBuilding(position);
                    continue;
                }
                
                if (grid.GetAt(position) == null)
                {
                    continue;
                }
                
                BuildingManipulator.SetBuilding(grid.GetAt(position), position);
            }
        }

        public static void OnResourcesChanged(float[] value)
        {
            Ui.ShowResources(value);
        }
    }
}