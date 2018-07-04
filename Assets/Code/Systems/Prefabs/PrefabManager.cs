using Code.Common;
using Code.Common.ResourceManaging;
using UnityEngine;

namespace Code.Systems.Prefabs
{
    [ResourceDirectory("Prefabs")]
    public class PrefabManager : ResourceManager<PrefabManager, GameObject>
    {
        [Resource] public GameObject Building;
    }
}