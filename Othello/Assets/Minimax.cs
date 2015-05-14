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
		ref_mat = othelloooo;
		white1 = GameObject.Find("white");
		black1 = GameObject.Find("black");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!turn) 
		{
			ref_mat = othelloooo;
			ref_color = color;
			//ref_turn = turn;
			int depth = 5;
			int best_score = 0;
			Vector3 best_move = new Vector3 ();
		
			ArrayList listy = new ArrayList ();
			listy = Possible_Moves (othelloooo);
			int [,] save = new int[8, 8];
			save = ref_mat;
			if (listy.Count > 0) 
			{
				for (int i = 0; i < listy.Count; i++) 
				{
					int better = find_move (ref_mat, depth, ref_color);
					if (better > best_score) 
					{
						best_score = better;
						best_move = (Vector3)listy [i];
					}
					ref_mat = save;
				}
				othelloooo [(int)best_move.x, (int)best_move.y] = color;
				Calculate_Board (Valid_Move (best_move, othelloooo), best_move, othelloooo);
				Place_Stuff (othelloooo);
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
	
	int find_move(int[,] othello, int dept, int colo)
	{

		dept--;
		Debug.Log ("depth = " + dept);
		int best_score = -100;
		int [,] starting = new int[8, 8];
		starting = othello;
		int [,] reference = new int[8, 8];
		reference = othello;
		Vector3 move = new Vector3 ();

		//Vector3 best_move;
		if (dept == 0) 
		{
			if (colo == -1)
			{
				return -Score (othello);
			}
			else
				return Score (othello);
		} 
		else 
		{
			ArrayList listy = new ArrayList ();
			listy = Possible_Moves (starting);
			if (listy.Count != 0) 
			{
				for (int i = 0; i < listy.Count; i++) 
				{
					move = (Vector3)listy [i];
					Debug.Log ("iteration: " + i);
					starting [(int)move.x, (int)move.y] = colo;
					Calculate_Board (Valid_Move (move, starting), move, starting);
					int better = find_move (starting, dept, -colo);
					if (better > best_score) 
					{
						best_score = better;
						Debug.Log ("new best move!");
					}
					starting = reference;
				}
				return best_score;
			} 
			else 
			{
				return Score (othello);
			}
		}
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
		return scor;
	}
	
}
