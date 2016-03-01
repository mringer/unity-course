using System;
using UnityEngine;

namespace Game.Entities
{
	public class RustyKey : ItemBase, IItem
	{
		public RustyKey()
		{
			Name = "Rusty Key";
			Text = "An old "+ Name +".";
			InspectKey = KeyCode.R;
		}
	}
}

