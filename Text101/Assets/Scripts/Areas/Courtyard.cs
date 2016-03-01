using System;

namespace Game.Entities
{
	public class Courtyard : AreaBase
	{
		public Courtyard ()
		{
			Name = this.GetType().Name;
			Text = "You are in a dark "+ Name +".";
		}
	}
}

