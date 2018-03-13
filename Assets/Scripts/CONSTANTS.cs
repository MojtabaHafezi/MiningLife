using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used for saving shared preferences and checking for tags etc.
public class CONSTANTS
{


	//Scenes
	public const string MAINSCENE = "scene_main";
	public const string MENUSCENE = "scene_menu";
	public const string GAMEOVERSCENE = "scene_gameover";
	public const string STARTSCENE = "scene_start";
	public const string SHOPSCENE = "scene_shop";
	public const string GUILDSCENE = "scene_guild";

	//Game objects
	public const string BOARD = "Board";
	public const string MAINCAMERA = "MainCamera";
	public const string PLAYER = "Player";

	//Game attributes
	public const string SLIDE = "slide";
	public const string RESOURCE = "Resource";
	public const string TILE = "Tile";
	public const int MAXITEMS = 7;

	//Item names
	public const string COAL = "Coal";
	public const string IRON = "Iron";
	public const string SILVER = "Silver";
	public const string GOLD = "Gold";
	public const string EMERALD = "Emerald";
	public const string RUBY = "Ruby";
	public const string DIAMOND = "Diamond";

	//Item ids
	public const int COAL_ID = 0;
	public const int IRON_ID = 1;
	public const int SILVER_ID = 2;
	public const int GOLD_ID = 3;
	public const int EMERALD_ID = 4;
	public const int RUBY_ID = 5;
	public const int DIAMOND_ID = 6;

	public const string ERROR = "ERROR";

	public static string ReturnNameForId (int id)
	{
		switch (id) {
		case COAL_ID:
			return COAL;
		case IRON_ID:
			return IRON;
		case SILVER_ID:
			return SILVER; 
		case GOLD_ID:
			return GOLD;
		case EMERALD_ID:
			return EMERALD;
		case RUBY_ID:
			return RUBY;
		case DIAMOND_ID:
			return DIAMOND;
		default:
			return ERROR;
		}
	}


}
