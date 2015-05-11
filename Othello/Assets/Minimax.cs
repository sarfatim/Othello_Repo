using UnityEngine;
using System.Collections;

public class Minimax : Rules 
{
	int[,] ref_mat = new int[8,8];
	int ref_color;
	//bool ref_turn;
	// Use this for initialization
	void Start () 
	{
		ref_mat = Rules.othelloooo;
		white1 = GameObject.Find("white");
		black1 = GameObject.Find("black");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!turn)
		{
			ref_color = color;
			//ref_turn = turn;
			int depth = 5;
			int best_score = 0;
			Vector3 best_move = new Vector3();
			
			ArrayList listy = new ArrayList ();
			listy = Possible_Moves (othelloooo);
			if (listy.Count > 0)
			{
				for (int i = 0; i < listy.Count; i++)
				{
					int better = find_move(ref_mat, (Vector3)listy[i], depth, ref_color);
					if (better > best_score)
					{
						best_score = better;
						best_move = (Vector3)listy[i];
					}
				}
				othelloooo[(int)best_move.x, (int)best_move.y] = color;
				Calculate_Board(Valid_Move(best_move,othelloooo), best_move, othelloooo);
				Place_Stuff(othelloooo);
				turn = !turn;
				color = -color;
			}
			else
			{
				turn = !turn;
				color = -color;
			}
		}
	}
	
	int find_move(int[,] othello, Vector3 move, int dept, int colo)
	{
		dept--;
		Debug.Log ("depth = " + dept);
		int best_score = -100;
		//Vector3 best_move;
		if (dept == 0)
		{
			return Score (othello);
		}
		else
		{
			ArrayList listy = new ArrayList ();
			listy = Possible_Moves (othello);
			if (listy.Count != 0)
			{
				for (int i = 0; i < listy.Count; i++)
				{
					Debug.Log("iteration: " + i);
					Calculate_Board (Valid_Move (move, othello), move, othello);
					int better = find_move (othello, (Vector3)listy[i], dept, -colo);
					if (better > best_score)
					{
						best_score = better;
						Debug.Log("new best move!");
					}
				}
			}
		}
		return best_score;
	}
	
	int Score(int[,] board)
	{
		int scor = 0;
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				scor = scor + board[i,j];
			}
		}
		if (color > 0)
		{
			return scor;
		}
		else 
			return -scor;
	}
	
}
