using System;
using System.Net;
using Caesar.Net;
using Code.Common;
using Code.Systems.Placing;
using Province.Vector;
using UnityEngine;
using NetData = System.Collections.Generic.Dictionary<string, object>;

namespace Code.Systems.Net
{
    public class ClientBehaviour : MonoBehaviour
    {
        public static NetManager NetManager { get; set; }
        
        private void Start() 
        {
            NetManager = new NetManager();
            NetManager.Connect(new IPEndPoint (IPAddress.Parse ("192.168.0.100"), 7999));
            
            var loginSuccess = NetManager.Login("", "");
            
            Debug.Log(loginSuccess ? "Logged in successfully" : "Authentication error"); 
            
            var areaData = NetManager.GetArea();

            var size = new Vector(areaData.GetLength(0), areaData.GetLength(1));
                
            Area.Current.Initialize(size);
            foreach (var position in size.Range())
            {
                BuildingManipulator.CreateBuilding(areaData[position.X, position.Y], position);
            }
            
            Debug.Log(NetManager.UpgradeBuilding(new Vector(1, 1), "Wooden house"));
            Debug.Log(NetManager.UpgradeBuilding(new Vector(1, 2), "Wooden house"));
        }

        private readonly TimeSpan _newsDelay = TimeSpan.FromSeconds(1);
        private TimeSpan _currentNewsDelay;

        private void Update()
        {
            _currentNewsDelay += TimeSpan.FromSeconds(Time.deltaTime);

            if (_currentNewsDelay >= _newsDelay)
            {
                _currentNewsDelay -= _newsDelay;
                foreach (var news in NetManager.GetNews())
                {
                    var data = (NetData) news["info"];
                    switch (news["type"].ToString())
                    {
                        case "OnEntityCreate":
                            BuildingManipulator.CreateBuilding(data["name"].ToString(), (Vector) data["position"]);
                            break;
                        
                        case "OnEntityDestroy":
                            BuildingManipulator.DestroyBuilding(data["name"].ToString(), (Vector) data["position"]);
                            break;
                    }
                }
            }
        }
    }
}
