using Code.Common.Constants;
using Code.Systems.Placing;
using Code.Systems.Prefabs;
using Code.Systems.Sprites;
using UnityEngine;
using Province.Vector;

namespace Code.Systems.Creating
{
    public static class Creator
    {
        public static void CreateBuilding(string name, Vector position)
        {
            var container = Object.Instantiate(
                PrefabManager.Current.Empty,
                GameObject.FindWithTag(Tags.BuildingContainer).transform);

            container.name = name;
            
            var holder = Object.Instantiate(PrefabManager.Current.Building, container.transform);
            PlacingSystem.Current.Move(holder.GetComponent<PositionComponent>(), position);
            holder.GetComponent<SpriteRenderer>().sortingLayerName = Layer.Holders;
            holder.name = "Holder";

            if (!SpriteManager.Current.HasResource(name)) return;
            
            var building = Object.Instantiate(PrefabManager.Current.Building, container.transform);
            building.GetComponent<SpriteRenderer>().sprite = SpriteManager.Current.GetResource(name);
            building.GetComponent<SpriteRenderer>().sortingLayerName = Layer.Buildings;
            PlacingSystem.Current.Move(building.GetComponent<PositionComponent>(), position); 
            building.name = "Building";
        }
    }
}