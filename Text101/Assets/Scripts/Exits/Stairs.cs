using System;
using UnityEngine;

namespace Game.Entities
{
	public class Stairs : ExitBase
	{
		public Stairs ()
		{ 
			KeyType = typeof(Hairpin);
			KeyCode = KeyCode.S;
			Name = "Stairs";
			LockedText = "A dark set of stairs with a locked door at the top.";
			UnlockedText = "A set of stairs with an unlocked door at the top that might be a way out!";
		}
	}
}

