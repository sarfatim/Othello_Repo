using UnityEngine;
using System.Collections;

public class SuperSimpleAI : Rules 
{

	// Use this for initialization
	void Start () 
	{
		white1 = GameObject.Find("white");
		black1 = GameObject.Find("black");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!turn)
		{
			ArrayList listy = new ArrayList ();
			listy = Possible_Moves (othelloooo, color_color);
			Debug.Log("Available moves: " + listy.Count);
			if (listy.Count != 0)
			{
				int rand = Random.Range (0, listy.Count);
				Vector3 move = (Vector3)listy[rand];
				directionn dire = new directionn();
				dire = Valid_Move(move, othelloooo, color_color);
				if (dire.maybe)
				{
					Debug.Log("Making a move");
					othelloooo[(int)move.x,(int)move.y] = color_color;
					Calculate_Board(dire, move, othelloooo, color_color);
					Place_Stuff(othelloooo);
					turn = !turn;
					color_color = -color_color;
				}
			}
			else
			{
				color_color = -color_color;
				turn = !turn;
			}
		}
	}

}
