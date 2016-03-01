using System;
using UnityEngine;

namespace Game.Entities
{
	public class Bucket : ItemBase, IItem
	{
		public Bucket ()
		{
			Name = this.GetType().Name;
			Text = "A "+ Name +" filled with poop.";
			InspectKey = KeyCode.B;
		}
	}
}

