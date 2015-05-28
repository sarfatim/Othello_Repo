using UnityEngine;
using System.Collections;

public class GeneticAlgorithm : MiniMax1 
{
	public othello_bot[] ai_list = new othello_bot[20];
	// Use this for initialization

	public struct othello_bot
	{
		public Vector4 values;
		public int num_wins;
		public bool am_white;
		//bool turn;
	};

	void Start () 
	{
		white1 = GameObject.Find("white");
		black1 = GameObject.Find("black");
		//initialize 20 random vector4's
		for (int i = 0; i < 20; i++)
		{
			float a = Random.Range(0,100.1f);		//first value
			//Debug.Log("a is: " + a);
			float b = Random.Range(0,100-a);		//second value
			//Debug.Log(b);
			float c = 100 - a - b;					//third value
			//Debug.Log(c);
			int d = Random.Range(1,40);				//depth (doesn't really need to be higher than 40)

			//random order
			Vector4 greg = new Vector4(-1,-1,-1,0);
			int posit = (int)Random.Range(0,3);
			greg[posit] = a;
			bool one = false;

			for (int k = 0; k < 3; k++)
			{
				if (greg[k] == -1 && !one)
				{
					greg[k] = b + .001f;
					one = true;
					//Debug.Log("this is b");
				}
				else if (greg[k] == -1 && one)
				{
					greg[k] = c;
					//Debug.Log("this is c");
				}
			}
			greg[3] = d;
			othello_bot botter = new othello_bot();
			botter.values = greg;
			botter.num_wins = 0;
			ai_list.SetValue(botter,i);

		}
		bot_war (0, 1);
		bot_war (1, 0);
		bot_war(1, 2);
		bot_war(0,2);
		bot_war(2, 1);
		bot_war(2, 0);
		Debug.Log("First Bot: " + ai_list[0].num_wins);
		Debug.Log("Second Bot: " + ai_list[1].num_wins);
		Debug.Log("Third Bot: " + ai_list[2].num_wins);
	}
	
	// Update is called once per frame
	void bot_war (int bot1, int bot2) 
	{
		bool game_is_over = false;
		bool game_maybe_over = false;
		while(!game_is_over)
		{
			if (Possible_Moves (othelloooo, color_color).Count > 0) //player one
			{
				//depth = depth1;
				
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
					current = NaiveMiniMax (board, (int)ai_list[bot1].values[3], color_color, bread_crumbs);
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
				//turn = !turn;
				game_maybe_over = false;
			} 
			else if (Possible_Moves (othelloooo, color_color).Count == 0 && game_maybe_over)
			{
				//turn = !turn;
				color_color = -color_color;
				game_is_over = true;
			}
			else if (Possible_Moves (othelloooo, color_color).Count == 0 && !game_maybe_over)
			{
				game_maybe_over = true;
				color_color = -color_color;
			}
			if (Possible_Moves (othelloooo, color_color).Count > 0) //player two
			{
				//depth = depth1;
				
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
					current = NaiveMiniMax (board, (int)ai_list[bot2].values[3], color_color, bread_crumbs);
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
				//turn = !turn;
				game_maybe_over = false;
			} 
			else if (Possible_Moves (othelloooo, color_color).Count == 0 && game_maybe_over)
			{
				//turn = !turn;
				color_color = -color_color;
				game_is_over = true;
			}
			else if (Possible_Moves (othelloooo, color_color).Count == 0 && !game_maybe_over)
			{
				game_maybe_over = true;
				color_color = -color_color;
			}
		}
		int score = 0;
		for (int i = 0; i < 8; ++i)
		{
			for (int j = 0; j < 8; j++)
			{
				score += othelloooo[i,j];
			}
		}
		if (score > 0)
		{
			ai_list[bot2].num_wins++;
		}
		else if (score < 0)
		{
			ai_list[bot1].num_wins++;
		}
		show_score();
		reset_board();
	}

	public int NaiveMiniMax(int[,] board, int depthy, int new_color, ArrayList bread_crumbs, othello_bot bott)
	{
		if (depthy <= 0 || Possible_Moves(board, color_color).Count == 0) 
		{
			return ScoreBoard (board, bread_crumbs, bott);
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
						score = NaiveMiniMax(new_board, depthy -1, -new_color, bread_crumbs);
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
						//int hold = bread_crumbs.Count - 1;
						score = NaiveMiniMax(new_board, depthy -1, -new_color, bread_crumbs);
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

	public void show_score()
	{
		int black = 0;
		int white = 0;
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if (othelloooo[i,j] == 1)
				{
					white++;
				}
				else if (othelloooo[i,j] == -1)
				{
					black++;
				}
			}
		}
		Debug.Log("White: " + white + " Black: " + black);
	}

	public int ScoreBoard(int[,] board, ArrayList bread_crumbs, othello_bot boot) //0 is simple minimax, 1 is disc-square, 2 is mobility
	{
		float score = 0.0f;
		int score_me = 0;
		int score_you = 0;
		
		float score1 = 0.0f;
		float score2 = 0.0f;
		float score3 = 0.0f;
		for (int i = 0; i < 8; ++i) 
		{
			for (int j= 0; j < 8; ++j)
			{
				score1 += board[i,j]; //a is minimax
			}
		}
		for (int i = 0; i < bread_crumbs.Count; i++)
		{
			Vector3 move = (Vector3)bread_crumbs[i];
			score2 -= disk_square_simple[(int)move.x, (int)move.y];  //b is disc square
		}
		score_me = Possible_Moves(board, color_color).Count;
		score_you = Possible_Moves(board, -color_color).Count;
		score3 = score_me - score_you;  //c is mobility
		
		score = boot.values[1]*score1 + boot.values[2]*score2 + boot.values[3]*score3;
		
		//Debug.Log (score);
		return (int)score;
	}
	//0. ***generate 20 ai's
	//1. play ((20 * 20) - 20) * 2 games, where each ai gets a turn as each color against each other ai
	//2. choose the two ai's with the highest win rates.
	//3. randomly choose the values to keep from the array of combinations
	//4. play ((10 * 10) - 10) * 2 games
	//5. choose the two ai's with the highest win rates
	//6. randomly choose the values from the array of combinations
	//7. repeat steps 4-6 until done

	//struct: ai
	//4 values
	//# wins
	//current color maybe??


	//for ai generation:
	//choose a number between 1 and 3: thats the first variable that gets chosen
	//generate a random value between 0 and 1 for the first variable (a)
	//choose a number between 1 and 2
	//generate a random value between 1 and (1 - a) (b)
	//the third value is 1 - a - b

}

/*
 * 1 2 3 4 5 6 7 8
 * 12 13 14 16 17 18
 * 23 24 25 27 28
 * 34 35 36 38 39
 * 45 46 47
 * 56 57 58
 * 67 68
 * 78
 * 123 124 127 128
 * 523 524 527 528
 * 163 164 167 168
 * 563 564 567 568
 * 234 238 274 278
 * 634 638 674 678
 * */