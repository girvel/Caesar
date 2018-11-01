using Imperium.CommonData;
using Province.Vector;
using UnityEngine;

namespace Code.Systems.Placing 
{
	public class PositionComponent : MonoBehaviour
	{
		public Vector Position { get; set; }



		private void Start()
		{
			var placingSystem = PlacingSystem.Current;
			placingSystem.Subjects.Add(this);
			placingSystem.Move(this, Position);
		}
	}
}

