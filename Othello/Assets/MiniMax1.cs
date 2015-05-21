using UnityEngine;
using System.Collections;

public class MiniMax1 : Rules 
{
	public int depth;
	public float a, b, c;
	public int[,] disk_square_simple = 
		{{99,-8,8,6,6,8,-8,99}, 
		{-8,-24,-4,-3,-3,-4,-24,-8},
		{8,-4,7,4,4,7,-4,8},
		{6,-3,4,0,0,4,-3,6},
		{6,-3,4,0,0,4,-3,6}, 
		{8,-4,7,4,4,7,-4,8}, 
		{-8,-24,-4,-3,-3,-4,-24,-8},
		{99,-8,8,6,6,8,-8,99}};
	public int heur;
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
				ArrayList bread_crumbs = new ArrayList();
					bread_crumbs.Add(current_move);
				board [(int)current_move.x, (int)current_move.y] =  color_color;
				Calculate_Board (Valid_Move (current_move, board, color_color), current_move, board, color_color);
				current = NaiveMiniMax (board, depth, color_color, bread_crumbs);
				bread_crumbs.Remove(0);
				if (current < best) 
				{
					best = current;
					best_move = current_move;
				}
			}
			Calculate_Board (Valid_Move (best_move, othelloooo, color_color), best_move, othelloooo, color_color);
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

	int NaiveMiniMax(int[,] board, int depthy, int new_color, ArrayList bread_crumbs)
	{
		if (depth <= 0 || Possible_Moves(board, color_color).Count == 0) 
		{
			return ScoreBoard (board, bread_crumbs);
		} 
		else 
		{
			ArrayList moves = new ArrayList();
			moves = Possible_Moves(board, color_color);
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
						Calculate_Board(Valid_Move(move, new_board, color_color), move, new_board, color_color);
						bread_crumbs.Add(move);
						//int hold = bread_crumbs.Count - 1;
						score = NaiveMiniMax(new_board, depth -1, -new_color, bread_crumbs);
						bread_crumbs.Remove(move);
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
						//bread_crumbs.Add(new Vector3(4,4,0)); //has a value of 0
						int hold = bread_crumbs.Count - 1;
						score = NaiveMiniMax(new_board, depth -1, -new_color, bread_crumbs);
						//bread_crumbs.Remove(hold);
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
				return ScoreBoard(board, bread_crumbs);
			}
		}
	}

	int ScoreBoard(int[,] board, ArrayList bread_crumbs) //0 is simple minimax, 1 is disc-square, 2 is mobility
	{
		float score = 0.0f;
		int score_me = 0;
		int score_you = 0;
		if (heur == 0)
		{
			for (int i = 0; i < 8; ++i) 
			{
				for (int j= 0; j < 8; ++j)
				{
					score += board[i,j];
				}
			}
		}
		else if (heur == 1)
		{
			//score = -1 * disk_square_simple[(int)move.x, (int)move.y];
			for (int i = 0; i < bread_crumbs.Count; i++)
			{
				Debug.Log(bread_crumbs.Count);
				Vector3 move = (Vector3)bread_crumbs[i];
				score -= disk_square_simple[(int)move.x, (int)move.y];
			}
		}
		else if (heur == 2)
		{
			score_me = Possible_Moves(board, color_color).Count;
			score_you = Possible_Moves(board, -color_color).Count;
			score = score_me - score_you;
		}
		else if (heur == 4)
		{
			Vector3 move = (Vector3)bread_crumbs[0];
			score -= disk_square_simple[(int)move.x, (int)move.y];
		}
		else if (heur == 5)
		{
			float score1 = 0.0f;
			float score2 = 0.0f;
			float score3 = 0.0f;
			for (int i = 0; i < 8; ++i) 
			{
				for (int j= 0; j < 8; ++j)
				{
					score1 += board[i,j];
				}
			}
			for (int i = 0; i < bread_crumbs.Count; i++)
			{
				Vector3 move = (Vector3)bread_crumbs[i];
				score2 -= disk_square_simple[(int)move.x, (int)move.y];
			}
			score_me = Possible_Moves(board, color_color).Count;
			score_you = Possible_Moves(board, -color_color).Count;
			score3 = score_me - score_you;

			score = a*score1 + b*score2 + c*score3;
		}
		//Debug.Log (score);
		return (int)score;
	}
}
			