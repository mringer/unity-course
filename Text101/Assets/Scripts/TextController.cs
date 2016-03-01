using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Entities;

public class TextController : MonoBehaviour 
{
	//Current game state
	private AreaBase myState;

	//Our inventory that persists between rooms
	private List<IItem> _inventory = new List<IItem>();

	//Handle to the screen text
	public Text text;

	// Use this for initialization
	void Start () {
		myState = RandomGraph();
	}
	
	// Update is called once per frame
	void Update ()
	{
		myState = myState.UpdateState(text, ref _inventory);

		if(myState == null) {
			_inventory = new List<IItem>();
			myState = RandomGraph();

		}
	}

	/// <summary>
	/// Retuerns a randomized game tree starting in the cell.
	/// </summary>
	/// <returns>The graph.</returns>
	private AreaBase RandomGraph()
	{
		var random = new System.Random ();
		var cell = new Cell();
		bool solved = false;
		int rooms = 0;
		//Build the game tree
		random.ExtendGraph(cell, ref rooms, ref solved, 10);
		var start = new Start(cell);
		int areas = 0;
		int exits = 0;
		int freedom = cell.WalkGraph(ref exits, ref areas);
		Debug.Log("exits:" + exits + " areas:"+ areas + " freedom:"+freedom);

		var sb = new System.Text.StringBuilder();
		Action<string> append = (s)=>{sb.Append(s);};
		cell.PrintPretty("",true, append);
		Debug.Log(sb.ToString());
		return start;
	}

}








