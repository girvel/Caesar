using System.Reflection;
using UnityEngine;

namespace Code.Systems.Sprites
{
    public class BuildingSprites : MonoBehaviour
    {
        public SpriteRenderer[] AllRenderers 
        {
            get
            {
                return new[] {BuildingWeather, Building, TerrainWeather, Terrain};
            }
        }
        
        public SpriteRenderer BuildingWeather, Building, TerrainWeather, Terrain, Squad;
    }
}