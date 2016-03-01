using System;
using UnityEngine;

namespace Game.Entities
{
	public class Mirror : ItemBase, IItem
	{
		public Mirror()
		{
			Name = this.GetType().Name;
			Text = "A dusty cracked  "+ Name +".";
			InspectKey = KeyCode.M;
		}
	}
}

