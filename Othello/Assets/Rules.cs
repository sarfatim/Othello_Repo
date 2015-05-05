using UnityEngine;
using System.Collections;

public class Rules : MonoBehaviour 
{
	bool turn = true; //true is white
	int[,] othello = new int[8,8]; //a matrix with 1,-1, and 0 for reference
	GameObject white1;
	GameObject black1;
	Object[,] othe = new Object[8, 8]; //a matrix with gameobjects as a mirror. 
	//this is just here so that the gameobjects can be destroyed.  Use the other matrix
	int color = 1;
	ArrayList possible_moves = new ArrayList();

	void Start () //SOME THINGS ARE UPSIDE DOWN, MAY OR MAY NOT NEED FIXING
	{
		white1 = GameObject.Find("white");
		black1 = GameObject.Find("black");
		for(int i = 0; i < 8; i++)
		{
			for(int j = 0; j < 8; j++)
			{
				othello[i,j] = 0;
			}
		}
		othello [3,3] = -1;
		othello [4,4] = -1;
		othello [3,4] = 1;
		othello [4,3] = 1;
		//othello [0, 0] = 1;


		Place_Stuff ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown("space")) //change the turn
		{
			turn = !turn;
		}
		if (Input.GetMouseButtonDown(0) && turn) //if its your turn and you click
		{
			Vector3 vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 11);
			Vector3 vvec = Culll(Camera.main.ScreenToWorldPoint(vec));
			directionn dir = new directionn();
			dir = Valid_Move(vvec);
			if (dir.maybe)//Valid_Move_White(vvec))
			{
				othello[(int)vvec.x,(int)vvec.y] = 1;
				Calculate_Board(dir,vvec);
				Place_Stuff();
				turn = !turn;
			}
		}
	}

	bool on_board(Vector3 move)
	{
		if (move.x >= 0 && move.x <= 7 && move.y >= 0 && move.y <= 7)
			return true;
		else
			return false;
	}
	bool on_board(int movex, int movey)
	{
		if (movex >= 0 && movex <= 7 && movey >= 0 && movey <= 7)
			return true;
		else
			return false;
	}

	Vector3 Culll(Vector3 orig) //makes the spot clicked align with the grid.
	{
		//Vector3 point5 = new Vector3 (.5f, .5f, 0);
		return new Vector3 (Mathf.Floor (orig.x + .5f), Mathf.Floor (orig.y + .5f), 11); //+ point5;
		//return new Vector3 ((int) (orig.x), (int) (orig.y), 11); //+ point5;
	}

	struct directionn
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

	bool Valid_South(Vector3 move) //down is -y
	{
		Debug.Log ("enter valid south");
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move))
		{
			Debug.Log("on board");
			for (int i = 1; i < 8; i++)
			{
				Debug.Log("in for loop");
				if (on_board((int)move.x, (int)move.y-i))
				{
					Debug.Log("one lower");
					if (othello[(int)move.x, (int)move.y-i] == -color) //not your color
					{
						maybe = true;
						Debug.Log("found the black");
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
	bool Valid_North(Vector3 move) //up is y
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move))
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
	bool Valid_West(Vector3 move) //left is -x
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move))
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
	bool Valid_East(Vector3 move) //right is +x
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move))
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
	bool Valid_NorthEast(Vector3 move) //up right is +x+y
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move))
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
	bool Valid_NorthWest(Vector3 move) //up left is -x+y
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move))
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
	bool Valid_SouthEast(Vector3 move) //down right is +x-y
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move))
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
	bool Valid_SouthWest(Vector3 move) //down left is -x-y
	{
		bool maybe = false; //for checking
		bool fal = false; //default return
		if (on_board(move))
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
	
	directionn Valid_Move(Vector3 move)
	{
		directionn dir = new directionn();
		
		if (Valid_South(move))
		{
			dir.maybe = true;
			dir.down = true;
		}
		if (Valid_West(move))
		{
			dir.maybe = true;
			dir.left = true;
		}
		if (Valid_East(move))
		{
			dir.maybe = true;
			dir.right = true;
		}
		if (Valid_North(move))
		{
			dir.maybe = true;
			dir.left_up = true;
		}
		if (Valid_SouthWest(move))
		{
			dir.maybe = true;
			dir.left_down = true;
		}
		if (Valid_NorthEast(move))
		{
			dir.maybe = true;
			dir.right_up = true;
		}
		if (Valid_SouthEast(move))
		{
			dir.maybe = true;
			dir.right_down = true;
		}
		if (Valid_NorthWest(move))
		{
			dir.maybe = true;
			dir.left_up = true;
		}
		return dir;
	}

	ArrayList Possible_Moves()
	{
		ArrayList pos_mov = new ArrayList ();
		for(int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if (Valid_Move(new Vector3(i,j,0)).maybe)
				{
					pos_mov.Add(new Vector3(i,j,0));
				}
			}
		}
		return pos_mov;
	}

	void Calculate_Board(directionn dir, Vector3 move) //takes a move, flips the tiles necessary
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

	void Place_Stuff()  //use this after a valid move has been made, and the matrix updated
	{
		//int[,] otho = new int[8, 8];
		//otho = Matrix_Flip (othello);
		for (int i = 0; i <8; i++) 
		{
			for(int j = 0; j < 8; j++)
			{
				if (othello[i,j] != 0)
				{
					Destroy(othe[i,j]);
				}
				if (othello[i,j] == 1)
				{
					othe[i,j] = Instantiate(white1, new Vector3(i,j,11), Quaternion.identity);
				}
				else if (othello[i,j] == -1)
				{
					othe[i,j] = Instantiate(black1, new Vector3(i,j,11), Quaternion.identity);
				}
			}
		}
	}
}
