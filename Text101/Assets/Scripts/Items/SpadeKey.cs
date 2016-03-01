using System;
using UnityEngine;

namespace Game.Entities
{
	public class SpadeKey : ItemBase, IItem
	{
		public SpadeKey ()
		{
			Name = this.GetType().Name;
			Text = "A "+ Name +" shaped like a Spade.";
			InspectKey = KeyCode.S;
		}
	}
}