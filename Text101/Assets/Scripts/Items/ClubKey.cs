using System;
using UnityEngine;

namespace Game.Entities
{
	public class ClubKey : ItemBase, IItem
	{
		public ClubKey ()
		{
			Name = this.GetType().Name;
			Text = "A "+ Name +" shaped like a Club.";
			InspectKey = KeyCode.C;
		}
	}
}

