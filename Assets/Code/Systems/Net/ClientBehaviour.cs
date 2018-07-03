using System.Net;
using Caesar.Net;
using Code.Common.Constants;
using Code.Systems.Placing;
using Code.Systems.Prefabs;
using Code.Systems.Sprites;
using Imperium.Client;
using Imperium.CommonData;
using UnityEngine;
using NetData = System.Collections.Generic.Dictionary<string, object>;

namespace Code.Systems.Net
{
    public class ClientBehaviour : MonoBehaviour 
    {
        public NetManager NetManager { get; set; }
        
        private void Start() 
        {
            NetManager = new NetManager();
            NetManager.Connect(new IPEndPoint (IPAddress.Parse ("192.168.0.100"), 7999));
            
            var loginSuccess = NetManager.Login("", "");
            
            Debug.Log(loginSuccess ? "Logged in successfully" : "Authentication error"); 
            
            var areaData = NetManager.GetArea();

            var size = new Vector(areaData.GetLength(0), areaData.GetLength(1));
                
            Area.Current.Initialize(size);
            foreach (var position in size)
            {
                var holder = Instantiate(PrefabManager.Current.Building);
                PlacingSystem.Current.Move(holder.GetComponent<PositionComponent>(), position);
                holder.GetComponent<SpriteRenderer>().sortingLayerName = Layer.Holders;

                if (areaData[position.X, position.Y] == "House")
                {
                    var currentObject = Instantiate(PrefabManager.Current.Building);
                    currentObject.GetComponent<SpriteRenderer>().sprite = SpriteManager.Current.WoodenHouse;
                    currentObject.GetComponent<SpriteRenderer>().sortingLayerName = Layer.Buildings;
                    PlacingSystem.Current.Move(currentObject.GetComponent<PositionComponent>(), position);
                }
                else
                {
                    var currentObject = Instantiate(PrefabManager.Current.Building);
                    currentObject.GetComponent<SpriteRenderer>().sprite = SpriteManager.Current.Forest;
                    currentObject.GetComponent<SpriteRenderer>().sortingLayerName = Layer.Buildings;
                    PlacingSystem.Current.Move(currentObject.GetComponent<PositionComponent>(), position);
                }
            }
        }
    }
}
