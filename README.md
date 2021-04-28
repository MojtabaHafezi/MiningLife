# MiningLife
This unity project was created for my module "Computer Games Development" in MSc. Computer Games Systems at the Nottingham Trent University.
The code provided is by no means perfect. The main task was to create an aspect of a game within a specific time frame
and I just had to implement the procedural generated level even though it meant more complexity.

Tasks:
1.	You are required to identify, research and plan a game prototype, and document this process in a Specification Document.
As part of the process you need to produce a Specification Document. 
2.	You are required to create and demonstrate a game prototype in the final week of the course.


Youtube: 
<a href="http://www.youtube.com/watch?feature=player_embedded&v=0u7H_4k5UlY&t=31s" target="_blank"><img src="http://img.youtube.com/vi/0u7H_4k5UlY&t=31s/0.jpg" 
alt="Mining Life Video" width="240" height="180" border="10" /></a>

Gameplay description:

The game starts with a typical menu with options to choose from e.g. “Load Game”, “Options” etc.
After choosing a new or loading a saved game the user is presented with the start screen. This screen
represents the city the player lives in and provides different choices. The user can visit the shop, a
guild or enter an instance of “caves”. Each instance is procedurally generated and thus random every
time. The shop provides static improvements to the player attributes. The guild on the other hand
requests specific quests and rewards the player upon return to the city after an instance.
The main game starts with the created instance and focuses on the character upon entry. The
mechanics are simplified for a prototype, the main user input consist of the arrow keys which let the
player mine in the specified direction and the game camera always follows the player. The game
world is restricted to the left, right and down so that the user has no way to go up again. To get out
of the cave instance the player can use a home button, which is available at the top of the screen.
Alternatively, the player returns to the city when  the stamina is reduced to
zero.
During the instance the user just moves in the direction of the tile and triggers a mining animation.
This action costs the player endurance and takes a specific amount of time depending on the current
stats. Buying upgrades in the shop speeds up the action and reduces the amount of energy
consumed. Furthermore, the player has a limited amount of space within his bag to hold the
minerals.
Should the player return to the city the daily maintain costs are displayed, which include the costs for inn, food etc. These
costs remain the same throughout the game and can lead to poverty if the player does not collect
enough resources. Collected resources can be sold to the shop at a fixed price.
The generation of the world/cave happens procedurally and depends on the player’s location in
vertical direction. After a specific amount of movement downwards a new set of tiles is generated.
Based on the current position and a random factor, various kinds of tiles with different mining
difficulty will be instantiated. This results in a higher difficulty the deeper the player gets to. The
game is not meant to end at a specific level, but this can be changed depending on the final
playability. Mining Life is targeting the PC only but due to the simple input control this can be altered
to any platform easily.

