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
        public const string EmptyBuildingName = "<nothing>";
        
        public static void InitializeBuildingsGrid(Vector size)
        {
            foreach (var position in size.Range())
            {
                _initializeBuilding(position);
            }
        }

        private static void _initializeBuilding(Vector position)
        {
            var building = Object.Instantiate(
                PrefabManager.Current.Building,
                GameObject.FindWithTag(Tags.GlobalBuildingContainer).transform);

            building.name = EmptyBuildingName;
            
            PlacingSystem.Current.Move(building.GetComponent<PositionComponent>(), position);
        }

        public static void HideBuilding(Vector position)
        {
            var sprites = PlacingSystem.Current.Area[position].First().GetComponent<BuildingSprites>();
            
            foreach (var renderer in sprites.AllRenderers)
            {
                renderer.color = new Color(0.8f, 0.8f, 0.8f);
            }
            sprites.Squad.sprite = null;
        }
        
        public static void SetBuilding(PlaceDto dto, Vector position)
        {
            var building = PlacingSystem.Current.Area[position].First();
            
            building.name = dto.BuildingName + (dto.Temperature < 0 ? " (Snowy)" : "");

            var sprites = building.GetComponent<BuildingSprites>();

            foreach (var renderer in sprites.AllRenderers)
            {
                renderer.color = Color.white;
            }
            
            sprites.BuildingWeather.sprite = Sprites.GetBuildingWeatherSprite(dto.Temperature, dto.BuildingName, dto.TerrainName);
            sprites.Building.sprite = Sprites.GetBuildingSprite(dto.BuildingName, dto.TerrainName);
            sprites.TerrainWeather.sprite = Sprites.GetTerrainWeatherSprite(dto.Temperature, dto.TerrainName);
            sprites.Terrain.sprite = Sprites.GetTerrainSprite(dto.TerrainName);
            sprites.Squad.sprite = Sprites.GetSquadSprite(dto.SquadName);
        }
    }
}