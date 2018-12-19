using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
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

            NetManager.AddResources();
            
            Debug.Log(NetManager.UpgradeBuilding(new Vector(1, 1), "Wooden house"));
        }

        private void Update()
        {
            Repeater.Every(TimeSpan.FromSeconds(1), ExecuteNews);
        }

        private static void ExecuteNews()
        {
            foreach (var news in NetManager.GetNews())
            {
                var method = typeof(NewsContainer).GetMethod(news["type"].ToString());
                var data = (NetData) news["info"];

                try
                {
                    method.Invoke(
                        null,
                        method.GetParameters().Select(p => data[p.Name]).ToArray());
                }
                catch (TargetException ex)
                {
                    Debug.LogError("News invokation error\n" + ex);
                }
                catch (KeyNotFoundException ex)
                {
                    Debug.LogError("Wrong arguments in method NewsContainer." + method.Name + "\n" + ex);
                }
            }
        }
    }
}
