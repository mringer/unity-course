using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using Game;
using Game.Entities;

namespace LogicTests
{
	[TestFixture ()]
	public class GraphLogic
	{
		[Test ()]
		public void GetNewArea_Test()
		{
			//Given a starting node 
			var cell = new Cell();
			var random = new System.Random();

			//When the GetNewArea creates a new area.
			var result = random.GetNewArea(cell);

			//Then the new area will not be null.
			Assert.IsNotNull(result);

			//and is an instance of AreaBase 
			Assert.IsInstanceOf<AreaBase>(result);

			//and is a different room than the last area.
			Assert.IsNotInstanceOf<Cell>(result);
		}

		[Test ()]
		public void GetNewExit_Test()
		{
			//Given a starting node of cell with exits of heart and spade
			var random = new System.Random();
			var cell = new Cell();
			var heartDoor = new HeartDoor();
			var spadeDoor = new SpadeDoor();
			cell.Exits.AddRange(new ExitBase[]{heartDoor, spadeDoor});

			//When the GetNewExit creates a new area.
			var result = random.GetNewExit(cell.Exits);

			//Then the new exit will not be null.
			Assert.IsNotNull(result);

			//and the new exit is an instance of ExitBase 
			Assert.IsInstanceOf<ExitBase>(result);

			//and the new exit is not already a exit to the room.
			Assert.IsNotInstanceOf<HeartDoor>(result);
			Assert.IsNotInstanceOf<SpadeDoor>(result);
		}

		[Test ()]
		public void ExtendGraph_Test ()
		{
			//Given a starting node of cell
			var random = new System.Random ();
			var cell = new Cell ();
			bool solved = false;
			int rooms = 0;

			//When the ExtendGraph builds the level.
			random.ExtendGraph (cell, ref rooms, ref solved, 40);

			//Then the game should be solvable.
			Assert.IsTrue (solved, "The generated graph is not solvable!");

			//walk the tree to check our work.
			int doors = 0, areas = 0;
			int freedom = cell.WalkGraph(ref doors, ref areas);

			//and the game should only contain one exit.
			Assert.AreEqual(1, freedom, "There should be only one Exit from the level!");

			//Debug console output to visually inspect the state of our random levels
			Console.WriteLine("doors:" + doors + " areas:"+ areas + " freedom:"+freedom);
			cell.PrintPretty ("", true, Console.Write);
		}
	}

	[TestFixture]
	public class Input {
		[Test ()]
		public void FindUniqueKey_Test()
		{
			//Given a control set with A in use.
			var list = new List<KeyCode>();
			list.Add(KeyCode.A);
			//When a new option that uses the A key by default is added
			var result = list.FindUniqueKey(KeyCode.A);
			//Then the next available key B should be used
			Assert.AreEqual(KeyCode.B, result);
		}

		[Test ()]
		public void FindUniqueKey_Wrap_Test()
		{
			//Given a control set with Z in use.
			var list = new List<KeyCode>();
			list.Add(KeyCode.Z);

			//When a new option that uses the Z key by default is added
			var result = list.FindUniqueKey(KeyCode.Z);

			//Then the next available key A should be used
			Assert.AreEqual(KeyCode.A, result);
		}
	}
}

