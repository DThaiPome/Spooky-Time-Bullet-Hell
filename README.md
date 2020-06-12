*The art for this game is still being made, so in the meantime all screenshots from the game will include my placeholder art assets.*

![room_action](/PagesAssets/room_action.png)

*The player, the red arrow, skirmishes with a group of bat enemies.*

# Overview - Time Bullet Hell (6/11/2020)

This project does not yet have a name, and right now it is very early in development. I am working on this project alongside [Kat Minor](https://www.katminor.com/) and Elena Kosowski, but all of the code and other Unity assetts found in this repository thus far are all mine. The scope of the project is fairly small, and as of now it is uncertain what exactly we will do with the finished product.

## Concept

This unnamed project is a bullet-hell game with time mechanics inspired by the popular VR game *Superhot*. The player battles against swarms of enemies, which fire large clusters of bullets at the player that they must dodge. In the game, time will only flow normally as long as the player is moving. This means that if the player stops moving entirely, enemies, bullets, and other objects will nearly cease to move. Slowing down the flow of time can allow the player to better navigate through extremely narrow gaps or other tough obstacles.

![boss_action](/PagesAssets/boss_action.png)

*The player fights the large bat boss.*

The player will navigate through several individual levels, each one a large network of rooms that each contain enemies to deal with. The goal of each level of is to explore each room until the player finds and defeats the boss enemy, allowing them to leave. The difficulty of this comes with the unforgiving consequences of taking a hit. The player can only be hit by a few bullets before they lose all of their lives and their game is ended.

The enemies defeated by the player may drop power-ups that can make the player stronger or give them an extra life. They also can drop coins, which can be used to purchase some of these items in the shop later on. The player can only hold so many items, though, so it is important that they effectively plan what items they buy and how and when they will use them.

# Code Design & Implementation

## Spawning Objects

To make a bullet hell, I needed a way to create a lot of bullets really fast during runtime. In Unity, I am able to create a reference object, then have a script create a clone of that object whenever necessary. However, making a clone is not a very memory-light process, and using this to spawn, say, one-thousand bullets on-screen all at once would certainly cause the game to run at snails pace. I instead opted for a different solution: I created all of the bullet-clones I would ever use before the game started, and then pull from this pool of objects whenever a bullet had to be fired. 

The BulletManager, along with the Pool object, serve this purpose. The Pool contains the reference object that it uses to then create a certain amount of clones of that object (in the bullet's case, this is one thousand clones). It keeps all of the clones inactive so they are not visible in game, and has them lined up in a Queue. The BulletManager holds this pool, and when a bullet needs to be fired, it simply requests a clone from the Pool. The Pool then activates the object, and gives it to the BulletManager to initialize and send on its way. The downside of this design is that the Pool only has a finite amount of clones. However, if a Pool runs out of clones, it is designed to create the missing clones on the fly as a fail-safe.

I later realized this system would be useful for a number of objects in the game, such as with enemies and pickups, so the BulletManager was generalized to the ObjectManager that can complete this process with any type of object. All I have to do to add more bullets, enemies, etc. is to add their object to the list of reference objects in its respective manager. It will automatically create a Pool for that object, and create the necessary clones.

When firing bullets, extra information is needed, such as where the bullet is fired from, and what direction it should travel. All of this information is taken as input to the ObjectManager via the ISpawnProperties interface, which will initialize a clone taken from the Pool with whatever information the bullet needs. So, even as different bullets or enemies require different types of information for their initialization, this process will always be almost entirely decoupled from the ObjectManager.

### Improvements

As with some of the other classes in this project, I for whatever reason thought that adding an interface to a class that extends Unity's MonoBehaviour class was bad practice. A lack of an interface for the ObjectManager and Pool objects makes the code somewhat unreadable, and difficult to modify as well. I will soon be fixing this.

## Designing Behaviours

When designing the various bullets and enemies, I figured that similar objects would have similar behaviours. Bullets would probably be moving until they hit something in some form, enemies would probably be doing something every frame, and so on. I generalize all of their behaviours using the respective BulletBehaviour, MobBehaviour, and APickup (for pickup items) abstract classes. Each class defines the general behaviours of each type of object. Then, if a particular object needs to add to or modify those behaviours in any way, I can simply override the respective method in the new class. This saves a lot of potential code duplication.

### Improvements

Every Unity class that extends MonoBehaviour can define methods such as Start, Update, OnDestroy, and so on, each of which is called along with a specific Unity process. Start gets called after an object is loaded, Update is called each frame, etc. In my abstract classes, in order to allow the subclasses to override these methods, I implement each of them so that they call an identically-named virtual method that can be overridden. I do this a lot, and it has produced a lot of repeated code. So, I plan on creating one abstract class that implements this pattern for every Unity event method. Then, any other abstract classes I make can simply extend this abstract class, saving the work of setting up this pattern separately again and again.

I also, once again, did not design any of these behaviour classes with interfaces. The code in the abstract classes is less readable as a result; I plan on adding interfaces to facilitate readability and make debugging much easier.

