using System;
using UnityEngine;

namespace Game.Entities
{
	public class Hammer : ItemBase, IItem
	{
		public Hammer ()
		{
			Name = this.GetType().Name;
			Text = "An broken "+ Name +".";
			InspectKey = KeyCode.H;
		}
	}
}

