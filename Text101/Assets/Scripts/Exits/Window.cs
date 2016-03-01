using System;
using UnityEngine;

namespace Game.Entities
{
	public class Window : ExitBase
	{
		public Window()
		{ 
			KeyType = typeof(Hammer);
			KeyCode = KeyCode.W;
			Name = "Window";
			LockedText = "A locked window that won't budge!";
			UnlockedText = "An unlocked window that you can open!";
		}
	}
}

