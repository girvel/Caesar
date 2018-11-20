using System.Linq;
using Code.Common.Constants;
using Code.Systems.Placing;
using Code.Systems.Prefabs;
using Code.Systems.Sprites;
using UnityEngine;
using Province.Vector;

namespace Code.Systems.Creating
{
    public static class BuildingManipulator
    {
        public static void CreateBuilding(string name, Vector position)
        {
            var building = Object.Instantiate(
                PrefabManager.Current.Building,
                GameObject.FindWithTag(Tags.GlobalBuildingContainer).transform);

            building.name = name;
            PlacingSystem.Current.Move(building.GetComponent<PositionComponent>(), position); 
            
            if (!SpriteManager.Current.HasResource(name)) return;
            building.transform.GetComponent<SpriteRenderer>().sprite = SpriteManager.Current.GetResource(name);
        }

        public static void DestroyBuilding(string name, Vector position)
        {
            var component = PlacingSystem.Current.Area[position].First(o => o.gameObject.name == name);
            PlacingSystem.Current.Unregister(component);
            Object.Destroy(component.gameObject);
        }
    }
}