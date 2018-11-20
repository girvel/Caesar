using Province.Vector;
using UnityEngine;

namespace Code.Systems.Placing 
{
	public class PositionComponent : MonoBehaviour
	{
		public Vector Position { get; set; }



		private void Start()
		{
			PlacingSystem.Current.Register(this);
		}
	}
}

