using System.Linq;
using Code.Common.Constants;
using Code.Systems.Placing;
using Code.Systems.Prefabs;
using Code.Systems.Sprites;
using Imperium.CommonData;
using Province.Vector;
using UnityEngine;

namespace Code.Common
{
    public static class BuildingManipulator
    {
        public static void CreateBuilding(PlaceDto dto, Vector position)
        {
            var building = Object.Instantiate(
                PrefabManager.Current.Building,
                GameObject.FindWithTag(Tags.GlobalBuildingContainer).transform);

            if (dto == null)
            {
                building.name = "<nothing>";
                return;
            }

            var name = dto.BuildingName + (dto.Temperature < 0 ? " (Snowy)" : "");
            building.name = name;
            PlacingSystem.Current.Move(building.GetComponent<PositionComponent>(), position);

            var sprites = building.GetComponent<BuildingSprites>();

            sprites.BuildingWeather.sprite = Sprites.GetBuildingWeatherSprite(dto.Temperature, dto.BuildingName, dto.TerrainName);
            sprites.Building.sprite = Sprites.GetBuildingSprite(dto.BuildingName, dto.TerrainName);
            sprites.TerrainWeather.sprite = Sprites.GetTerrainWeatherSprite(dto.Temperature, dto.TerrainName);
            sprites.Terrain.sprite = Sprites.GetTerrainSprite(dto.TerrainName);
            sprites.Squad.sprite = Sprites.GetSquadSprite(dto.SquadName);
        }

        public static void DestroyBuilding(Vector position)
        {
            var component = PlacingSystem.Current.Area[position].FirstOrDefault();

            if (component == null) return;
            
            PlacingSystem.Current.Unregister(component);
            Object.Destroy(component.gameObject);
        }
    }
}