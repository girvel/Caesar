using Code.Common;
using Code.Common.ResourceManaging;
using UnityEngine;

namespace Code.Systems.Sprites
{
    [ResourceDirectory("Sprites")]
    public class SpriteManager : ResourceManager<SpriteManager, Sprite>
    {
        [Resource]
        public Sprite WoodenHouse, Forest;
    }
}