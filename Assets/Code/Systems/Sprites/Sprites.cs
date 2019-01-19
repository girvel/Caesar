using Code.Common;
using UnityEditor;
using UnityEngine;

namespace Code.Systems.Sprites
{
    public static class Sprites
    {
        public static Sprite GetBuildingSprite(string buildingName, string terrainName)
        {
            return Resources.Load<Sprite>(string.Format(@"Sprites\Buildings\{0} ({1})", buildingName, terrainName)) 
                   ?? Resources.Load<Sprite>(@"Sprites\Buildings\" + buildingName);
        }

        public static Sprite GetBuildingWeatherSprite(float temperature, string buildingName, string terrainName)
        {
            return temperature < 0 
                ? Resources.Load<Sprite>(string.Format(@"Sprites\Buildings\Weather\Snow - {0} ({1})", buildingName, terrainName)) 
                    ?? Resources.Load<Sprite>(@"Sprites\Buildings\Weather\Snow - " + buildingName) 
                : null;
        }

        public static Sprite GetTerrainSprite(string terrainName)
        {
            return Resources.Load<Sprite>(@"Sprites\Terrains\" + terrainName);
        }

        public static Sprite GetTerrainWeatherSprite(float temperature, string terrainName)
        {
            return temperature < 0 ? Resources.Load<Sprite>(@"Sprites\Terrains\Weather\Snow - " + terrainName) : null;
        }

        public static Sprite GetSquadSprite(string squadName)
        {
            return Resources.Load<Sprite>(@"Sprites\Squads\" + squadName);
        }
    }
}