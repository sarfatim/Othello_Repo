﻿using UnityEngine;
using System.Collections;

public class Rules : MonoBehaviour 
{
	public static bool turn = true; //true is white
	public static int[,] othelloooo = new int[8,8]; //a matrix with 1,-1, and 0 for reference
	public GameObject white1;
	public GameObject black1;
	public static Object[,] othe = new Object[8, 8]; //a matrix with gameobjects as a mirror. 
	//this is just here so that the gameobjects can be destroyed.  Use the other matrix
	public static int color = -1;
	public static int game_counter = 0;
	//ArrayList possible_moves = new ArrayList();

	void Start () //SOME THINGS ARE UPSIDE DOWN, MAY OR MAY NOT NEED FIXING
	{
		white1 = GameObject.Find("white");
		black1 = GameObject.Find("black");
		game_counter = 0;
		for(int i = 0; i < 8; i++)
		{
			for(int j = 0; j < 8; j++)
			{
				othelloooo[i,j] = 0;
			}
		}
		othelloooo [3,3] = -1;
		othelloooo [4,4] = -1;
		othelloooo [3,4] = 1;
		othelloooo [4,3] = 1;
		//othello [0, 0] = 1;


		Place_Stuff (othelloooo);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown("space")) //change the turn
		{
			turn = !turn;
			color = -color;
		}
		if (Input.GetMouseButtonDown(0) && turn) //if its your turn and you click
		{
			ArrayList posss = Possible_Moves(othelloooo);
			if (posss.Count == 0)
			{
				turn = !turn;
				color = -color;
				game_counter++;
			}
			else
			{
				game_counter = 0;
				Vector3 vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 11);
				Vector3 vvec = Culll(Camera.main.ScreenToWorldPoint(vec));
				directionn dir = new directionn();
				dir = Valid_Move(vvec, othelloooo);
				if (dir.maybe)//Valid_Move_White(vvec))
				{
					othelloooo[(int)vvec.x,(int)vvec.y] = color;
					Calculate_Board(dir,vvec, othelloooo);
					Place_Stuff(othelloooo);
					turn = !turn;
					color = -color;
				}
			}
		}
	}

	public bool on_board(Vector3 move)
	{
		if (move.x >= 0 && move.x <= 7 && move.y >= 0 && move.y <= 7)
			return true;
		else
			return false;
	}
	public bool on_board(int movex, int movey)
	{
		if (movex >= 0 && movex <= 7 && movey >= 0 && movey <= 7)
			return true;
		else
			return false;
	}

	public Vector3 Culll(Vector3 orig) //makes the spot clicked align with the grid.
	{
		//Vector3 point5 = new Vector3 (.5f, .5f, 0);
		return new Vector3 (Mathf.Floor (orig.x + .5f), Mathf.Floor (orig.y + .5f), 11); //+ point5;
		//return new Vector3 ((int) (orig.x), (int) (orig.y), 11); //+ point5;
	}

	public struct directionn
	{
		public bool maybe;
		public bool left;
		public bool right;
		public bool up;
		public bool down;
		public bool left_up;
		public bool right_down;
		public bool right_up;
		public bool left_down;

		public void start()
		{
			maybe = false;
			left = false;
			right = false;
			up = false;
			down = false;
			left_up = false;
			right_down = false;
			right_up = false;
			left_down = false;
		}
	}

	public bool Valid_South(Vector3 move, int[,] othello) //down is -y
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move) && othello[(int)move.x, (int)move.y] == 0)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x, (int)move.y-i))
				{
					if (othello[(int)move.x, (int)move.y-i] == -color) //not your color
					{
						maybe = true;
					}
					else if (othello[(int)move.x, (int)move.y-i] == color && maybe) //your color and previous wasn't
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
		}
		return fal;
	}
	public bool Valid_North(Vector3 move, int[,] othello) //up is y
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move) && othello[(int)move.x, (int)move.y] == 0)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x, (int)move.y + i))
				{
					if (othello[(int)move.x, (int)move.y + i] == -color) //not your color
					{
						maybe = true;
					}
					else if (othello[(int)move.x, (int)move.y + i] == color && maybe) //your color and previous wasn't
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
		}
		return fal;
	}
	public bool Valid_West(Vector3 move, int[,] othello) //left is -x
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move) && othello[(int)move.x, (int)move.y] == 0)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x - i, (int)move.y))
				{
					if (othello[(int)move.x - i, (int)move.y] == -color) //not your color
					{
						maybe = true;
					}
					else if (othello[(int)move.x - i, (int)move.y] == color && maybe) //your color and previous wasn't
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
		}
		return fal;
	}
	public bool Valid_East(Vector3 move, int[,] othello) //right is +x
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move) && othello[(int)move.x, (int)move.y] == 0)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x + i, (int)move.y))
				{
					if (othello[(int)move.x + i, (int)move.y] == -color) //not your color
					{
						maybe = true;
					}
					else if (othello[(int)move.x + i, (int)move.y] == color && maybe) //your color and previous wasn't
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
		}
		return fal;
	}
	public bool Valid_NorthEast(Vector3 move, int[,] othello) //up right is +x+y
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move) && othello[(int)move.x, (int)move.y] == 0)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x + i, (int)move.y + i))
				{
					if (othello[(int)move.x + i, (int)move.y + i] == -color) //not your color
					{
						maybe = true;
					}
					else if (othello[(int)move.x + i, (int)move.y + i] == color && maybe) //your color and previous wasn't
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
		}
		return fal;
	}
	public bool Valid_NorthWest(Vector3 move, int[,] othello) //up left is -x+y
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move) && othello[(int)move.x, (int)move.y] == 0)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x - i, (int)move.y + i))
				{
					if (othello[(int)move.x - i, (int)move.y + i] == -color) //not your color
					{
						maybe = true;
					}
					else if (othello[(int)move.x - i, (int)move.y + i] == color && maybe) //your color and previous wasn't
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
		}
		return fal;
	}	
	public bool Valid_SouthEast(Vector3 move, int[,] othello) //down right is +x-y
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move) && othello[(int)move.x, (int)move.y] == 0)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x + i, (int)move.y - i))
				{
					if (othello[(int)move.x + i, (int)move.y - i] == -color) //not your color
					{
						maybe = true;
					}
					else if (othello[(int)move.x + i, (int)move.y - i] == color && maybe) //your color and previous wasn't
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
		}
		return fal;
	}
	public bool Valid_SouthWest(Vector3 move, int[,] othello) //down left is -x-y
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move) && othello[(int)move.x, (int)move.y] == 0)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x - i, (int)move.y - i))
				{
					if (othello[(int)move.x - i, (int)move.y - i] == -color) //not your color
					{
						maybe = true;
					}
					else if (othello[(int)move.x - i, (int)move.y - i] == color && maybe) //your color and previous wasn't
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
		}
		return fal;
	}
	
	public directionn Valid_Move(Vector3 move, int[,] othello)
	{
		directionn dir = new directionn();
		
		if (Valid_South(move, othello))
		{
			dir.maybe = true;
			dir.down = true;
		}
		if (Valid_West(move, othello))
		{
			dir.maybe = true;
			dir.left = true;
		}
		if (Valid_East(move, othello))
		{
			dir.maybe = true;
			dir.right = true;
		}
		if (Valid_North(move, othello))
		{
			dir.maybe = true;
			dir.up = true;
		}
		if (Valid_SouthWest(move, othello))
		{
			dir.maybe = true;
			dir.left_down = true;
		}
		if (Valid_NorthEast(move, othello))
		{
			dir.maybe = true;
			dir.right_up = true;
		}
		if (Valid_SouthEast(move, othello))
		{
			dir.maybe = true;
			dir.right_down = true;
		}
		if (Valid_NorthWest(move, othello))
		{
			dir.maybe = true;
			dir.left_up = true;
		}
		return dir;
	}

	public ArrayList Possible_Moves(int[,] othello)
	{
		ArrayList pos_mov = new ArrayList ();
		for(int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if (Valid_Move(new Vector3(i,j,0), othello).maybe)
				{
					pos_mov.Add(new Vector3(i,j,0));
				}
			}
		}
		Debug.Log("pos_mov count: " +pos_mov.Count);
		return pos_mov;
	}

	public void Calculate_Board(directionn dir, Vector3 move, int[,] othello) //takes a move, flips the tiles necessary
	{
		if (dir.up)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x, (int)move.y + i))
				{
					if (othello[(int)move.x, (int)move.y + i] == -color) //not your color
					{
						othello[(int)move.x, (int)move.y + i] = color;
					}
					else if (othello[(int)move.x, (int)move.y + i] == color) //your color and previous wasn't
					{
						break;
					}
				}
			}
		}
		if (dir.down)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x, (int)move.y - i))
				{
					if (othello[(int)move.x, (int)move.y - i] == -color) //not your color
					{
						othello[(int)move.x, (int)move.y-i] = color;
					}
					else if (othello[(int)move.x, (int)move.y - i] == color) //your color and previous wasn't
					{
						break;
					}
				}
			}
		}
		if (dir.left)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x - i, (int)move.y))
				{
					if (othello[(int)move.x - i, (int)move.y] == -color) //not your color
					{
						othello[(int)move.x - i, (int)move.y] = color;
					}
					else if (othello[(int)move.x - i, (int)move.y] == color) //your color and previous wasn't
					{
						break;
					}
				}
			}
		}
		if (dir.right)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x + i, (int)move.y))
				{
					if (othello[(int)move.x + i, (int)move.y] == -color) //not your color
					{
						othello[(int)move.x + i, (int)move.y] = color;
					}
					else if (othello[(int)move.x + i, (int)move.y] == color) //your color and previous wasn't
					{
						break;
					}
				}
			}
		}
		if (dir.left_down)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x - i, (int)move.y - i))
				{
					if (othello[(int)move.x - i, (int)move.y - i] == -color) //not your color
					{
						othello[(int)move.x - i, (int)move.y - i] = color;
					}
					else if (othello[(int)move.x - i, (int)move.y - i] == color) //your color and previous wasn't
					{
						break;
					}
				}
			}
		}
		if (dir.left_up)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x - i, (int)move.y + i))
				{
					if (othello[(int)move.x - i, (int)move.y + i] == -color) //not your color
					{
						othello[(int)move.x - i, (int)move.y + i] = color;
					}
					else if (othello[(int)move.x - i, (int)move.y + i] == color) //your color and previous wasn't
					{
						break;
					}
				}
			}
		}
		if (dir.right_down)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x + i, (int)move.y - i))
				{
					if (othello[(int)move.x + i, (int)move.y - i] == -color) //not your color
					{
						othello[(int)move.x + i, (int)move.y - i] = color;
					}
					else if (othello[(int)move.x + i, (int)move.y - i] == color) //your color and previous wasn't
					{
						break;
					}
				}
			}
		}
		if (dir.right_up)
		{
			for (int i = 1; i < 8; i++)
			{
				if (on_board((int)move.x + i, (int)move.y + i))
				{
					if (othello[(int)move.x + i, (int)move.y + i] == -color) //not your color
					{
						othello[(int)move.x + i, (int)move.y + i] = color;
					}
					else if (othello[(int)move.x + i, (int)move.y + i] == color) //your color and previous wasn't
					{
						break;
					}
				}
			}
		}
	}

//	int[,] Matrix_Flip(int[,] matt)
//	{
//		int[,] matthew = new int[8, 8];
//		for (int i = 0; i < 8; i++)
//		{
//			for (int j = 0; j < 8; j++)
//			{
//				matthew[i,j] = matt[i, 7-j];
//			}
//		}
//		return matthew;
//	}

	public void Place_Stuff(int[,] board)  //use this after a valid move has been made, and the matrix updated
	{
		//int[,] otho = new int[8, 8];
		//otho = Matrix_Flip (othello);
		othelloooo = board;

		for (int i = 0; i <8; i++) 
		{
			for(int j = 0; j < 8; j++)
			{
				if (othelloooo[i,j] != 0)
				{
					Destroy(othe[i,j]);
				}
				if (othelloooo[i,j] == 1)
				{
					othe[i,j] = Instantiate(white1, new Vector3(i,j,11), Quaternion.identity);
				}
				else if (othelloooo[i,j] == -1)
				{
					othe[i,j] = Instantiate(black1, new Vector3(i,j,11), Quaternion.identity);
				}
			}
		}
	}
}
