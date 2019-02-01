using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using Caesar.Net;
using Code.Common;
using Code.Systems.Interface.Elements;
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
            MainPanel.Current.Text = "Hello,\n\n\n\n\nworld!";
            
            NetManager = new NetManager();
            NetManager.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.100"), 7999));
            
            var loginSuccess = NetManager.Login("", "");
            
            Debug.Log(loginSuccess ? "Logged in successfully" : "Authentication error"); 
            
            var vision = NetManager.GetVision();

            var size = vision.Grid.Size();
            
            Area.Current.Initialize(size);
            BuildingManipulator.InitializeBuildingsGrid(size);
            
            NewsContainer.OnVisionChanged(vision);

            NetManager.AddResources();
        }

        private void Update()
        {
            Repeater.Every(TimeSpan.FromSeconds(1), () => new Thread(GetNews).Start());
            ExecuteNews();
        }

        private NetData[] _currentNews = new NetData[0];
        
        private void GetNews()
        {
            var news = NetManager.GetNews();
            lock (_currentNews) _currentNews = news;
        }

        private void ExecuteNews()
        {
            NetData[] allNews;
            lock (_currentNews) allNews = _currentNews;
            
            foreach (var news in allNews)
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
            _currentNews = new NetData[0];
        }
    }
}
