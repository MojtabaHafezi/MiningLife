using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used for saving shared preferences and checking for tags etc.
public class CONSTANTS
{
	//Persistence
	public const string AUDIO = "Audio";
	public const string SOUND = "Sound";
	public const string CAPACITY = "Capacity";
	public const string MAXSTAMINA = "MaxStamina";
	public const string STAMINA = "Stamina";
	public const string CURRENCY = "Currency";
	public const string EFFICIENCY = "Efficiency";
	public const string CURRENTTOTAL = "CurrentTotal";

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
	public const string BACKPACK = "Backpack: ";
	public const string WEALTH = "Wealth: ";
	public const string NEWWEALTH = "New wealth: ";
	public const int TILESAHEAD = 5;
	public const int TILESTOSPAWN = 5;
	public const string PRICE = "Price: ";
	public const int PICKAXES = 5;
	public const int EXPENSES = 30;

	public const int BRONZE_PICK = 100;
	public const int IRON_PICK = 250;
	public const int SILVER_PICK = 500;
	public const int GOLD_PICK = 1000;
	public const int DIAMOND_PICK = 10000;

	public const int BRONZE_EFF = 2;
	public const int IRON_EFF = 3;
	public const int SILVER_EFF = 4;
	public const int GOLD_EFF = 6;
	public const int DIAMOND_EFF = 8;

	public const int BRONZE_STA = 110;
	public const int IRON_STA = 130;
	public const int SILVER_STA = 150;
	public const int GOLD_STA = 200;
	public const int DIAMOND_STA = 350;




	//Item names
	public const string DIRT = "Dirt";
	public const string STONE = "Stone";
	public const string REDSTONE = "RedStone";
	public const string LAVA = "Lava";
	public const string COAL = "Coal";
	public const string IRON = "Iron";
	public const string SILVER = "Silver";
	public const string GOLD = "Gold";
	public const string EMERALD = "Emerald";
	public const string RUBY = "Ruby";
	public const string DIAMOND = "Diamond";

	//Item ids
	public const int REDSTONE_ID = 96;
	public const int DIRT_ID = 97;
	public const int STONE_ID = 98;
	public const int LAVA_ID = 99;
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
