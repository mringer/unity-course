using System;
using UnityEngine;

namespace Game.Entities
{
	public class Hairpin : ItemBase, IItem
	{
		public Hairpin ()
		{
			Name = this.GetType().Name;
			Text = "Someone's old "+ Name +".";
			InspectKey = KeyCode.P;
		}
	}
}

