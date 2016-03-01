using System;

namespace Game.Entities
{
	public class Closet : AreaBase
	{
		public Closet ()
		{
			DeadEnd = true;
			Name = "Broom Closet";
			Text = "You are in a dark "+ Name +".";
		}
	}
}

