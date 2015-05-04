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
				Calculate_board(dir,vvec);
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

	bool Valid_Move_White_Left(Vector3 move) //checks whether the clicked space is a valid move
	{	//white is 1, black is -1
		bool maybe = false;
		if((int)othello[(int)move.x, (int)move.y] != (int)0) //fails if you click a non-empty space
		{
			return false;
		}
		if (move.x - 2 >= 0)//makes sure it won't go out of bounds
		{
			if(othello[(int)move.x - (int)1, (int)move.y] == (int)-color) //if the tile to the left is black
			{
				for(int i = 2; i < move.x + 1; i++) //for all the spaces to the left of the left space
				{
					if(othello[(int)move.x - i, (int)move.y] == (int)color && on_board(move)) //check if they're white
					{
						return true;
					}
					else if (othello[(int)move.x - i, (int)move.y] == (int)0  && on_board(move)) //invalid if blank
					{
						return false; //need to have 1 for each direction because of this
					}
				}
			}
		}
		return maybe;
	}

	bool Valid_Move_White_Right(Vector3 move) //checks whether the clicked space is a valid move
	{	//white is 1, black is -1
		bool maybe = false;
		if(othello[(int)move.x, (int)move.y] != 0) //fails if you click a non-empty space
		{
			return false;
		}
		if (move.x + 2 <= 7)//makes sure it won't go out of bounds
		{
			if(othello[(int)move.x + (int)1, (int)move.y] == (int)-color) //if the tile to the left is black
			{
				for(int i = 2; i < (8 - move.x); i++) //for all the spaces to the left of the left space
				{
					if(othello[(int)move.x + i, (int)move.y] == (int)color  && on_board(move)) //check if they're white
					{
						return true;
					}
					else if (othello[(int)move.x + i, (int)move.y] == (int)0 && on_board(move)) //invalid if blank
					{
						return false; //need to have 1 for each direction because of this
					}
				}
			}
		}
		return maybe;
	}

	bool Valid_Move_White_Up(Vector3 move) //checks whether the clicked space is a valid move
	{	//white is 1, black is -1
		bool maybe = false;
		if(othello[(int)move.x, (int)move.y] != 0) //fails if you click a non-empty space
		{
			return false;
		}
		if (move.y - 2 >= 0)//makes sure it won't go out of bounds
		{
			if(othello[(int)move.x, (int)move.y - (int)1] == (int)-color) //if the tile to the left is black
			{
				for(int i = 2; i < move.y + 1; i++) //for all the spaces to the left of the left space
				{
					if(othello[(int)move.x, (int)move.y - i] == (int)color  && on_board(move)) //check if they're white
					{
						return true;
					}
					else if (othello[(int)move.x, (int)move.y - i] == (int)0 && on_board(move)) //invalid if blank
					{
						return false; //need to have 1 for each direction because of this
					}
				}
			}
		}
		return maybe;
	}

	bool Valid_Move_White_Down(Vector3 move) //checks whether the clicked space is a valid move
	{	//white is 1, black is -1
		bool maybe = false;
		if(othello[(int)move.x, (int)move.y] != 0) //fails if you click a non-empty space
		{
			return false;
		}
		if (move.y + 2 <= 7)//makes sure it won't go out of bounds
		{
			if(othello[(int)move.x, (int)move.y + 1] == (int)-color) //if the tile to the left is black
			{
				for(int i = 2; i < move.y + 1; i++) //for all the spaces to the left of the left space
				{
					if(othello[(int)move.x, (int)move.y + i] == (int)color && on_board(move)) //check if they're white
					{
						return true;
					}
					else if (othello[(int)move.x, (int)move.y + i] == (int)0 && on_board(move)) //invalid if blank
					{
						return false; //need to have 1 for each direction because of this
					}
				}
			}
		}
		return maybe;
	}

	bool Valid_Move_White_Left_Down(Vector3 move) //checks whether the clicked space is a valid move
	{	//white is 1, black is -1
		bool maybe = false;
		if((int)othello[(int)move.x, (int)move.y] != (int)0) //fails if you click a non-empty space
		{
			return false;
		}
		if (move.x - 2 >= 0 && move.y - 2 >= 0)//makes sure it won't go out of bounds
		{
			if(othello[(int)move.x - (int)1, (int)move.y - 1] == (int)-color) //if the tile to the left is black
			{
				for(int i = 2; i < move.x + 1 && i < move.y + 1; i++) //for all the spaces to the left of the left space
				{
					if(othello[(int)move.x - i, (int)move.y - 1] == (int)color && on_board(move)) //check if they're white
					{
						return true;
					}
					else if (othello[(int)move.x - i, (int)move.y - 1] == (int)0 && on_board(move)) //invalid if blank
					{
						return false; //need to have 1 for each direction because of this
					}
				}
			}
		}
		return maybe;
	}

	bool Valid_Move_White_Left_Up(Vector3 move) //checks whether the clicked space is a valid move
	{	//white is 1, black is -1
		bool maybe = false;
		if((int)othello[(int)move.x, (int)move.y] != (int)0) //fails if you click a non-empty space
		{
			return false;
		}
		if (move.x - 2 >= 0 && move.y + 2 <= 7)//makes sure it won't go out of bounds
		{
			if(othello[(int)move.x - (int)1, (int)move.y + 1] == (int)-color) //if the tile to the left is black
			{
				for(int i = 2; i < move.x + 1 && i < move.y + 1; i++) //for all the spaces to the left of the left space
				{
					if(othello[(int)move.x - i, (int)move.y + 1] == (int)color && on_board(move)) //check if they're white
					{
						return true;
					}
					else if (othello[(int)move.x - i, (int)move.y + 1] == (int)0 && on_board(move)) //invalid if blank
					{
						return false; //need to have 1 for each direction because of this
					}
				}
			}
		}
		return maybe;
	}

	bool Valid_Move_White_Right_Down(Vector3 move) //checks whether the clicked space is a valid move
	{	//white is 1, black is -1
		bool maybe = false;
		if((int)othello[(int)move.x, (int)move.y] != (int)0) //fails if you click a non-empty space
		{
			return false;
		}
		if (move.x + 2 <= 7 && move.y - 2 >= 0)//makes sure it won't go out of bounds
		{
			if(othello[(int)move.x + (int)1, (int)move.y - 1] == (int)-color) //if the tile to the left is black
			{
				for(int i = 2; i < move.x + 1 && i < move.y + 1; i++) //for all the spaces to the left of the left space
				{
					if(othello[(int)move.x + i, (int)move.y - 1] == (int)color) //check if they're white
					{
						return true;
					}
					else if (othello[(int)move.x + i, (int)move.y - 1] == (int)0) //invalid if blank
					{
						return false; //need to have 1 for each direction because of this
					}
				}
			}
		}
		return maybe;
	}

	bool Valid_Move_White_Right_Up(Vector3 move) //checks whether the clicked space is a valid move
	{	//white is 1, black is -1
		bool maybe = false;
		if((int)othello[(int)move.x, (int)move.y] != (int)0) //fails if you click a non-empty space
		{
			return false;
		}
		if (move.x + 2 <= 7 && move.y + 2 <= 7)//makes sure it won't go out of bounds
		{
			if(othello[(int)move.x + (int)1, (int)move.y + 1] == (int)-color) //if the tile to the left is black
			{
				for(int i = 2; i < move.x + 1 && i < move.y + 1; i++) //for all the spaces to the left of the left space
				{
					if(othello[(int)move.x + i, (int)move.y + 1] == (int)color && on_board(move)) //check if they're white
					{
						return true;
					}
					else if (othello[(int)move.x + i, (int)move.y + 1] == (int)0 && on_board(move)) //invalid if blank
					{
						return false; //need to have 1 for each direction because of this
					}
				}
			}
		}
		return maybe;
	}

	directionn Valid_Move(Vector3 move)
	{
		directionn dir = new directionn();
		
		if (Valid_Move_White_Down(move))
		{
			dir.maybe = true;
			dir.down = true;
		}
		if (Valid_Move_White_Left(move))
		{
			dir.maybe = true;
			dir.left = true;
		}
		if (Valid_Move_White_Right(move))
		{
			dir.maybe = true;
			dir.right = true;
		}
		if (Valid_Move_White_Left_Up(move))
		{
			dir.maybe = true;
			dir.left_up = true;
		}
		if (Valid_Move_White_Left_Down(move))
		{
			dir.maybe = true;
			dir.left_down = true;
		}
		if (Valid_Move_White_Right_Up(move))
		{
			dir.maybe = true;
			dir.right_up = true;
		}
		if (Valid_Move_White_Right_Down(move))
		{
			dir.maybe = true;
			dir.right_down = true;
		}
		if (Valid_Move_White_Up(move))
		{
			dir.maybe = true;
			dir.up = true;
		}
		return dir;
	}

	void Calculate_board(directionn dir, Vector3 move)
	{
		if (dir.left)
		{
			if (move.x - 2 >= 0)//makes sure it won't go out of bounds
			{
				if(othello[(int)move.x - (int)1, (int)move.y] == (int)-color) //if the tile to the left is black
				{
					for(int i = 1; i < move.x + 1; i++) //for all the spaces to the left of the space
					{
						if(othello[(int)move.x - i, (int)move.y] == (int)-color  && on_board(move)) //check if they're white
						{
							othello[(int)move.x - i, (int)move.y] = color;
						}
						else if (othello[(int)move.x - i, (int)move.y] == (int)color && on_board(move)) //invalid if blank
						{
							break; //need to have 1 for each direction because of this
						}
					}
				}
			}
		}
		if (dir.down)
		{
			if (move.y + 2 <= 7)//makes sure it won't go out of bounds
			{
				if(othello[(int)move.x, (int)move.y + (int)1] == (int)-color) //if the tile to the left is black
				{
					for(int i = 1; i < move.y + 1; i++) //for all the spaces to the left of the left space
					{
						if(othello[(int)move.x, (int)move.y + i] == (int)-color && on_board(move)) //check if they're white
						{
							othello[(int)move.x, (int)move.y + i] = color;
						}
						else if (othello[(int)move.x, (int)move.y + i] == (int)color && on_board(move)) //invalid if blank
						{
							break; //need to have 1 for each direction because of this
						}
					}
				}
			}
		}
		if (dir.right)
		{
			if (move.x + 2 <= 7)//makes sure it won't go out of bounds
			{
				if(othello[(int)move.x + (int)1, (int)move.y] == (int)-color) //if the tile to the left is black
				{
					for(int i = 1; i < (8 - move.x); i++) //for all the spaces to the left of the left space
					{
						if(othello[(int)move.x + i, (int)move.y] == (int)-color && on_board(move)) //check if they're white
						{
							othello[(int)move.x + i, (int)move.y] = color;
						}
						else if (othello[(int)move.x + i, (int)move.y] == (int)color && on_board(move)) //invalid if blank
						{
							break; //need to have 1 for each direction because of this
						}
					}
				}
			}
		}
		if (dir.up)
		{
			if (move.y - 2 >= 0)//makes sure it won't go out of bounds
			{
				if(othello[(int)move.x, (int)move.y - (int)1] == (int)-color) //if the tile to the left is black
				{
					for(int i = 1; i < move.y + 1; i++) //for all the spaces to the left of the left space
					{
						if(othello[(int)move.x, (int)move.y - i] == (int)-color && on_board(move)) //check if they're white
						{
							othello[(int)move.x, (int)move.y - i] = color;
						}
						else if (othello[(int)move.x, (int)move.y - i] == (int)0 && on_board(move)) //invalid if blank
						{
							break; //need to have 1 for each direction because of this
						}
					}
				}
			}
		}
		if (dir.left_up)
		{
			if (move.x - 2 >= 0 && move.y - 2 >= 0)//makes sure it won't go out of bounds
			{
				if(othello[(int)move.x - 1, (int)move.y - 1] == (int)-color) //if the tile to the left is black
				{
					for(int i = 1; i < (move.x + 1) && (i < move.y + 1); i++) //for all the spaces to the left of the space
					{
						if(othello[(int)move.x - i, (int)move.y - i] == (int)-color  && on_board(move)) //check if they're white
						{
							othello[(int)move.x - i, (int)move.y - i] = color;
						}
						else if (othello[(int)move.x - i, (int)move.y - i] == (int)color && on_board(move)) //invalid if blank
						{
							break; //need to have 1 for each direction because of this
						}
					}
				}
			}
		}
		if (dir.left_down)
		{
			if (move.x - 2 >= 0 && move.y + 2 <= 7)//makes sure it won't go out of bounds
			{
				if(othello[(int)move.x - 1, (int)move.y + 1] == (int)-color) //if the tile to the left is black
				{
					for(int i = 1; i < (move.x + 1) && (i < move.y + 1); i++) //for all the spaces to the left of the space
					{
						if(othello[(int)move.x - i, (int)move.y + i] == (int)-color && on_board(move)) //check if they're white
						{
							othello[(int)move.x - i, (int)move.y + i] = color;
						}
						else if (othello[(int)move.x - i, (int)move.y + i] == (int)color && on_board(move)) //invalid if blank
						{
							break; //need to have 1 for each direction because of this
						}
					}
				}
			}
		}
		if (dir.right_down)
		{
			if (move.x + 2 <= 7 && move.y + 2 <= 7)//makes sure it won't go out of bounds
			{
				if(othello[(int)move.x + 1, (int)move.y + 1] == (int)-color) //if the tile to the left is black
				{
					for(int i = 1; i < move.x + 1 && i < move.y + 1; i++) //for all the spaces to the left of the space
					{
						if(othello[(int)move.x + i, (int)move.y + i] == (int)-color && on_board(move)) //check if they're white
						{
							othello[(int)move.x + i, (int)move.y + i] = color;
						}
						else if (othello[(int)move.x + i, (int)move.y + i] == (int)color && on_board(move)) //invalid if blank
						{
							break; //need to have 1 for each direction because of this
						}
					}
				}
			}
		}
		if (dir.right_up)
		{
			if (move.x + 2 <= 7 && move.y - 2 >= 0)//makes sure it won't go out of bounds
			{
				if(othello[(int)move.x + 1, (int)move.y - 1] == (int)-color) //if the tile to the left is black
				{
					for(int i = 1; i < move.x + 1 && i < move.y + 1; i++) //for all the spaces to the left of the space
					{
						if(othello[(int)move.x + i, (int)move.y - i] == (int)-color && on_board(move)) //check if they're white
						{
							othello[(int)move.x + i, (int)move.y - i] = color;
						}
						else if (othello[(int)move.x + i, (int)move.y - i] == (int)color && on_board(move)) //invalid if blank
						{
							break; //need to have 1 for each direction because of this
						}
					}
				}
			}
		}
	}

	int[,] Matrix_Flip(int[,] matt)
	{
		int[,] matthew = new int[8, 8];
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				matthew[i,j] = matt[i, 7-j];
			}
		}
		return matthew;
	}

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
