using UnityEngine;
using System.Collections;

public class MiniMax1 : Rules 
{
	public int depth;
	// Use this for initialization
	void Start () 
	{
		white1 = GameObject.Find("white");
		black1 = GameObject.Find("black");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!turn && Possible_Moves (othelloooo, color_color).Count > 0) 
		{

			int [,] board = new int[8, 8];
			for (int l = 0; l < 8; ++l) 
			{
				for (int n = 0; n < 8; ++n) 
				{
					board [l, n] = othelloooo [l, n];
				}
			}
			ArrayList move_list = new ArrayList ();
			move_list = Possible_Moves (board, color_color);
			Vector3 best_move = new Vector3 ();
			best_move = (Vector3)move_list [0];
			int best = 0;
			int current = 0;

			Vector3 current_move = new Vector3 ();
			for (int i = 0; i < move_list.Count; ++i) 
			{
				for (int l = 0; l < 8; ++l) 
				{
					for (int n = 0; n < 8; ++n) 
					{
						board [l, n] = othelloooo [l, n];
					}
				}
				current_move = (Vector3)move_list [i];
				board [(int)current_move.x, (int)current_move.y] =  color_color;
				Calculate_Board (Valid_Move (current_move, board, color_color), current_move, board, color_color);
				current = NaiveMiniMax (board, depth, color_color);
				if (current < best) 
				{
					best = current;
					best_move = current_move;
				}
			}
			Calculate_Board (Valid_Move (best_move, othelloooo), best_move, othelloooo);
			othelloooo [(int)best_move.x, (int)best_move.y] = color_color;
			Place_Stuff (othelloooo);
			color_color = -color_color;
			turn = !turn;
		} 
		else if (!turn && Possible_Moves (othelloooo, color_color).Count == 0)
		{
			turn = !turn;
			color_color = -color_color;
		}
	}

	int NaiveMiniMax(int[,] board, int depth, int new_color)
	{
		if (depth <= 0 || Possible_Moves(board).Count == 0) 
		{
			return ScoreBoard (board);
		} 
		else 
		{
			ArrayList moves = new ArrayList();
			moves = Possible_Moves(board);
			int infinity = 9999;
			int best_score = 0;
			int score = 0;
			int [,] new_board = board;
			Vector3 move = new Vector3();
			if (moves.Count > 0)
			{
				if (new_color == 1) // white == max
				{
					for (int i = 0; i < moves.Count; ++i)
					{
						move = (Vector3)moves[i];
						best_score = infinity;
						new_board = board;
						new_board[(int)move.x,(int)move.y] = new_color;
						Calculate_Board(Valid_Move(move, new_board), move, new_board);
						score = NaiveMiniMax(new_board, depth -1, -new_color);
						if (score < best_score)
						{
							best_score = score;
						}
					}
					return best_score;
					// return min(minimax(node, depth -1)
				}
				else 			// black == min
				{
					for (int i = 0; i < moves.Count; ++i)
					{
						move = (Vector3)moves[i];
						best_score = -infinity;
						new_board = board;
						new_board[(int)move.x,(int)move.y] = new_color;
						Calculate_Board(Valid_Move(move, new_board, new_color), move, new_board, new_color);
						score = NaiveMiniMax(new_board, depth -1, -new_color);
						if (score > best_score)
						{
							best_score = score;
						}
					}
					return best_score;
					// return max(minimax(node, depth -1)
				}
			}
			else
			{
				return ScoreBoard(board);
			}
		}
	}

	int ScoreBoard(int[,] board)
	{
		int score = 0;
		for (int i = 0; i < 8; ++i) 
		{
			for (int j= 0; j < 8; ++j)
			{
				score += board[i,j];
			}
		}
		return score;
	}
}
