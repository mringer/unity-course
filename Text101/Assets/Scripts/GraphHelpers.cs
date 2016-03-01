using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Game.Entities;

namespace Game
{
	public static class GraphHelpers
	{
		private static IList<Type> _exitTypes;
		private static IList<Type> _areaTypes;

		static GraphHelpers(){
			//Cache the reflected types once per runtime.
			_exitTypes = typeof(ExitBase).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(ExitBase)) && !t.IsAbstract && t != typeof(BackDoor)).ToList();

			_areaTypes = typeof(AreaBase).Assembly.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(AreaBase)) && !t.IsAbstract && t != typeof(Freedom) && t != typeof(Start)).ToList();
		}

		/// <summary>
		/// Recursive method to extend the game tree.
		/// </summary>
		/// <returns><c>true</c>, if graph was extended, <c>false</c> otherwise.</returns>
		/// <param name="random">Random.</param>
		/// <param name="area">Area.</param>
		/// <param name="maxC">Max c.</param>
		/// <param name="maxD">Max d.</param>
		/// <param name="door_keys">Door keys.</param>
		/// <param name="solved">If set to <c>true</c> solved.</param>
		public static void ExtendGraph (this System.Random random, AreaBase area, ref int rooms, ref bool solved, int maxRooms = 20, int maxC = 10, int maxD = 10)
		{
			//Random enough..
			var area_complexity = random.Next (1, maxC); //maxC; //
			for (int i = 0; i < area_complexity; i++) {
				var allowed_depth = maxD - 1;
				if (maxRooms > rooms && allowed_depth > 0) { //we can add more rooms
					ExitBase newExit = random.GetNewExit(area.Exits);
					area.Exits.Add (newExit);
					if (newExit.KeyType != null) {
						var key = Activator.CreateInstance (newExit.KeyType) as IItem;
						area.Items.Add (key);
					}
					newExit.Fwd = random.GetNewArea(area);
					rooms++;
					ExtendGraph (random, newExit.Fwd, ref rooms, ref solved, maxRooms, area_complexity, allowed_depth);
				} 

				if(!solved) { //we hit the bottom for the first time, let's not leave it to chance and make a way out.
					ExitBase newExit = random.GetNewExit(area.Exits);
					area.Exits.Add(newExit);
					newExit.Fwd = new Freedom();
					if (newExit.KeyType != null) {
						var key = Activator.CreateInstance (newExit.KeyType) as IItem;
						area.Items.Add (key);
					}
					solved = true;
				}
			}
		}

		/// <summary>
		/// Extension method for Random to Get a new Exit.
		/// </summary>
		/// <returns>The new door.</returns>
		/// <param name="random">Random.</param>
		/// <param name="back">Prevous Area in the Graph</param>
		public static ExitBase GetNewExit(this System.Random random, IList<ExitBase> exits)
		{
			IList<Type> exitTypes = _exitTypes.Where(t => !exits.Any(x=>x.GetType() == t)).ToList();
			var exitType = exitTypes[random.Next(exitTypes.Count)];
			var newExit = (ExitBase)Activator.CreateInstance(exitType);
			return newExit;
		}

		/// <summary>
		/// Extension method for Random to Get a new Area.
		/// </summary>
		/// <returns>The new area.</returns>
		/// <param name="random">Random.</param>
		/// <param name="back">Prevous Area in the Graph</param>
		public static AreaBase GetNewArea(this System.Random random, AreaBase back)
		{
			IList<Type> areaTypes = _areaTypes.Where(t => !t.IsAbstract && back.GetType() != t).ToList();
			var areaType = areaTypes[random.Next(areaTypes.Count)];
			var newArea = (AreaBase)Activator.CreateInstance(areaType);
			//Add a door back to the previous area.
			newArea.Exits.Add(new BackDoor(){Fwd = back});
			return newArea;
		}

		/// <summary>
		/// Prints the pretty.
		/// </summary>
		/// <param name="area">Area.</param>
		/// <param name="indent">Indent.</param>
		/// <param name="last">If set to <c>true</c> last.</param>
		public static void PrintPretty (this AreaBase area, string indent, bool last, Action<string> printf)
		{
			printf (indent);
			if (last) {
				Console.Write ("\\-");
				indent += "  ";
			} else {
				printf ("|-");
				indent += "| ";

			}
			var nameLine = (area == null) ? "<Null>" : area.Name + "[" + string.Join ("|", area.Items.Select (x => x.Name).ToArray ()) + "]";

			printf (nameLine + "\n");

			for (int i = 0; i < area.Exits.Count; i++) {
				if(area.Exits[i].GetType() != typeof(BackDoor)){
					area.Exits[i].PrintPretty (indent, i == area.Exits.Count - 1, printf);
				}
			}
		}

		/// <summary>
		/// Prints the pretty.
		/// </summary>
		/// <param name="exit">Exit.</param>
		/// <param name="indent">Indent.</param>
		/// <param name="last">If set to <c>true</c> last.</param>
		public static void PrintPretty (this ExitBase exit, string indent, bool last, Action<string> printf)
		{
			printf (indent);
			if (last) {
				printf ("\\-");
				indent += "  ";
			} else {
				printf ("|-");
				indent += "| ";

			}
			printf ( ((exit != null) ? exit.Name : "<Null>") + "\n");
			if (exit.Fwd != null) {
				exit.Fwd.PrintPretty (indent, true, printf);
			}
		}

		public static int WalkGraph(this AreaBase area, ref int doors, ref int areas )
		{	int freedomCount = 0;
			areas++;
			doors += area.Exits.Count;
			foreach (var exit in area.Exits) {
				if(exit.GetType() == typeof(BackDoor) || exit.Fwd == null) continue;
				if(exit.Fwd.GetType() == typeof(Freedom)) {
					freedomCount++;
				}

				freedomCount += WalkGraph(exit.Fwd, ref doors, ref areas);
			}
			return freedomCount;
		}
	}
}

