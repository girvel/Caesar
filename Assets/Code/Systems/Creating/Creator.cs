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
            var currentObject = Object.Instantiate(PrefabManager.Current.Building);
            currentObject.GetComponent<SpriteRenderer>().sprite = SpriteManager.Current.WoodenHouse;
            currentObject.GetComponent<SpriteRenderer>().sortingLayerName = Layer.Buildings;
            PlacingSystem.Current.Move(currentObject.GetComponent<PositionComponent>(), position);
        }
    }
}