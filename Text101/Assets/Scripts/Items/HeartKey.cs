using System;
using UnityEngine;

namespace Game.Entities
{
	public class HeartKey : ItemBase, IItem
	{
		public HeartKey ()
		{
			Name = this.GetType().Name;
			Text = "A "+ Name +" shaped like a Heart.";
			InspectKey = KeyCode.H;
		}
	}
}

