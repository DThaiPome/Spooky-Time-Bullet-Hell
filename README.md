![room_action](/PagesAssets/room_action.png)

*The art for this game is still being made, so in the meantime all screenshots from the game will include my placeholder art assets.*

## Overview - Time Bullet Hell (6/11/2020)

This project does not yet have a name, and right now it is very early in development. I am working on this project alongside [Kat Minor](https://www.katminor.com/) and Elena Kosowski, but all of the code and other Unity assetts found in this repository thus far are all mine. The scope of the project is fairly small, and as of now it is uncertain what exactly we will do with the finished product.

### Concept

This unnamed project is a bullet-hell game with time mechanics inspired by the popular VR game *Superhot*. The player battles against swarms of enemies, which fire large clusters of bullets at the player that they must dodge. In the game, time will only flow normally as long as the player is moving. This means that if the player stops moving entirely, enemies, bullets, and other objects will nearly cease to move. Slowing down the flow of time can allow the player to better navigate through extremely narrow gaps or other tough obstacles.

![boss_action](/PagesAssets/boss_action)

The player will navigate through several individual levels, each one a large network of rooms that each contain enemies to deal with. The goal of each level of is to explore each room until the player finds and defeats the boss enemy, allowing them to leave. The difficulty of this comes with the unforgiving consequences of taking a hit. The player can only be hit by a few bullets before they lose all of their lives and their game is ended.

The enemies defeated by the player may drop power-ups that can make the player stronger or give them an extra life. They also can drop coins, which can be used to purchase some of these items in the shop later on. The player can only hold so many items, though, so it is important that they effectively plan what items they buy and how and when they will use them.

## Execution so far

### The bullets

To make a bullet hell, I needed a way to create a lot of bullets really fast during runtime. In Unity, I am able to create a reference object, then have a script create a clone of that object whenever necessary. However, making a clone is not a very memory-light process, and using this to spawn, say, one-thousand bullets on-screen all at once would certainly cause the game to run at snails pace. I instead opted for a different solution: I created all of the bullet-clones I would ever use before the game started, and then pull from this pool of objects whenever a bullet had to be fired. 

The BulletManager, along with the Pool object, serve this purpose. The Pool contains the reference object that it uses to then create a certain amount of clones of that object (in the bullet's case, this is one thousand clones). It keeps all of the clones inactive so they are not visible in game, and has them lined up in a Queue. The BulletManager holds this pool, and when a bullet needs to be fired, it simply requests a clone from the Pool. The Pool then activates the object, and gives it to the BulletManager to initialize and send on its way.

I later realized this system would be useful for a number of objects in the game, such as with enemies and pickups, so the BulletManager was generalized to the ObjectManager that can complete this process with any type of object.
