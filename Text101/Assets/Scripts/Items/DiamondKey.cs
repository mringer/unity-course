using System;
using UnityEngine;

namespace Game.Entities
{
	public class DiamondKey : ItemBase, IItem
	{
		public DiamondKey ()
		{
			Name = this.GetType().Name;
			Text = "A "+ Name +" shaped like a Diamond.";
			InspectKey = KeyCode.H;
		}
	}
}

