using System.Collections.Generic;
using Code.Common;
using Imperium.CommonData;
using UnityEngine;

namespace Code.Systems.Placing 
{
	public class PlacingSystem : SingletonBehaviour<PlacingSystem>
	{
		public List<PositionComponent> Subjects { get; set; }
		
		public Area Area { get; set; }

		public double CellSize;



		public void Move(PositionComponent component, Vector newPosition)
		{
            Area[component.Position].Remove(component);
            component.Position = newPosition;
            Area[component.Position].Add(component);
		}


		protected override void Awake()
		{
			base.Awake();
			
			Subjects = new List<PositionComponent>();
			Area = Area.Current;
		}
		
		private void Update()
		{
			foreach (var subject in Subjects)
			{
				subject.transform.position
					= (float) CellSize
					  * new Vector2(
						  subject.Position.X - subject.Position.Y,
						  (float) (subject.Position.X + subject.Position.Y) / 2);

				subject.GetComponent<SpriteRenderer>().sortingOrder = -(subject.Position.X + subject.Position.Y);
			}
		}
	}
}

