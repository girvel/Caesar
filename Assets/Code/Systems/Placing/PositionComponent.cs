using Imperium.CommonData;
using UnityEngine;

namespace Code.Systems.Placing 
{
	public class PositionComponent : MonoBehaviour
	{
		public Vector Position { get; set; }



		private void Start()
		{
			var placingSystem = FindObjectOfType<PlacingSystem>();
			placingSystem.Subjects.Add(this);
			placingSystem.Move(this, Position);
		}
	}
}

