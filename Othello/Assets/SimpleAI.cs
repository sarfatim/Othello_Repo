﻿using UnityEngine;
using System.Collections;

public class SimpleAI : Rules 
{
	int[,] board_clone = new int[8, 8];
	// Use this for initialization
	void Start () 
	{
		board_clone = othelloooo;
		white1 = GameObject.Find("white");
		black1 = GameObject.Find("black");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!turn) 
		{
			if (game_counter >= 2)
			{
				// Gameover at this point
			}
			else if (Possible_Moves(board_clone).Count == 0)
			{
				game_counter++;
				turn = !turn;
				color = -color;
			}
			else
			{
				Vector3 best = BestMove (board_clone);
				directionn d= Valid_Move(best, board_clone);
				Debug.Log ("best move: " + best.x + ", " + best.y);

				othelloooo[(int)best.x,(int)best.y] = color;
				Calculate_Board(d,best, othelloooo);

				string board = "";
				for (int i = 0; i < 8; ++i)
				{
					for (int j = 0; j < 8; ++j)
					{
						board += board_clone[i, j];
					}
				}
				Debug.Log("board: " + board);
				Place_Stuff(board_clone);
				turn = !turn;
				color = -color;
			}
		}
	}

	int NumPiecesTaken(Vector3 move,int[,] o)
	{
		int[,] old = new int[8,8];
		old = o;
		int old_total = 0;
		int new_total = 0;
		for (int i = 0; i < 8; ++i) 
		{
			for (int j = 0; j < 8; ++j) 
			{
				old_total += old[i,j];
			}
		}

		directionn d = Valid_Move (move, board_clone);
		Calculate_Board (d, move, board_clone);
		for (int i = 0; i < 8; ++i) 
		{
			for (int j = 0; j < 8; ++j)
			{
				new_total += board_clone[i,j];
			}
		}
		Debug.Log ("before change back");
		for (int i = 0; i < 8; ++i) 
		{
			for (int j = 0; j < 8; ++j)
			{
				board_clone[i,j] = old[i,j];
			}
		}
		Debug.Log ("after change back");
		return old_total - new_total;
	}

	Vector3 BestMove(int[,] old)
	{
		int[,] old1 = new int[8,8];
		old1 = old;
		int best_count = 0;
		Vector3 best = new Vector3(0,0,0);
		int current = 0;
		int best_index = 0;
		int poss = Possible_Moves (board_clone).Count;
		Debug.Log ("(bestmove) Possible Moves Count: " + poss);
		for (int i = 0; i < poss; ++i) 
		{
			Debug.Log("-------------For loop, index: " + i);
			Vector3 current_move = (Vector3)(Possible_Moves(board_clone)[i]);
			current = NumPiecesTaken(current_move, old1);
			if (current > best_count)
			{
				best_count = current;
				best_index = i;
			}
		}
		best = (Vector3)(Possible_Moves(board_clone)[best_index]);
		return best;
	}
}
