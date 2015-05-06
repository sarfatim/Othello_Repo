using UnityEngine;
using System.Collections;

public class SuperSimpleAI : Rules 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!turn)
		{
			ArrayList listy = new ArrayList ();
			listy = Possible_Moves (othelloooo);
			int rand = Random.Range (0, listy.Count);
			Vector3 move = (Vector3)listy[rand];
			directionn dire = new directionn();
			dire = Valid_Move(move, othelloooo);
			if (dire.maybe)
			{
				Debug.Log("Making a move");
				othelloooo[(int)move.x,(int)move.y] = color;
				Calculate_Board(dire,move, othelloooo);
				Place_Stuff(othelloooo);
				turn = !turn;
				color = -color;
			}
		}
	}

}
