using System.Collections.Generic;
using Code.Common;
using Province.Vector;
using UnityEngine;

namespace Code.Systems.Placing 
{
	public class PlacingSystem : System<PlacingSystem, PositionComponent>
	{
		public Area Area { get; set; }

		public double CellSize;



		public void Move(PositionComponent component, Vector newPosition)
		{
			Area[component.Position].Remove(component);
            component.Position = newPosition;
            Area[component.Position].Add(component);
		}
		
		

		public override void Register(PositionComponent subject)
		{
			Move(subject, subject.Position);
			base.Register(subject);
		}

		public override void Unregister(PositionComponent component)
		{
			Area[component.Position].Remove(component);
			base.Unregister(component);
		}


		
		protected override void Awake()
		{
			base.Awake();
			
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

				foreach (var spriteRenderer in subject.transform.GetComponentsInChildren<SpriteRenderer>())
				{
					spriteRenderer.sortingOrder = -(subject.Position.X + subject.Position.Y);
				}
			}
		}
	}
}

