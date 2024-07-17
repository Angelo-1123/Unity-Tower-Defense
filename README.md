# Unity Tower Defense

This project of a tower defense game using unity started as a study following [Brackeys' TD Game Tutorial](https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&ab_channel=Brackeys).

Along with the tutorial mentioned above, free models from [this](https://free-game-assets.itch.io/free-defence-tower-3d-low-poly-models) page on [itch.io](https://itch.io/) were imported to take the game closer to the style intended.

## Getting Started

To edit the game, Unity 2022.3.17f1 is needed. It can be downloaded by trying to open the project via UnityHub. However, to play the game you only need to open the Builds folder. There you will find not only the latest version, but all the previous ones.

## About the Game

Going by the ingame name of Doom TD, this game is a endless minimalistic Tower Defense with a gameplay loop of 19 unique waves of enemies with different traits. Once the 19th wave is cleared, a new loop begins with the enemies strengthened. The same happens after the 38th wave and so on.

## Mechanics

At the top left corner of the screen, the players' gold is displayed. it can be used to purchase and upgrade towers. At top right, the ammount of lives remaining. If an enemy manages to reach the end of the path, it vanishes and the player loses an ammount of lives depending on the kind of enemy.

Towers can be placed on nodes around the path that the enemies walk. There are currently three types of towers:

### Archer Tower

The most basic tower, shoots arrows at one enemy at a time. Upgrades increase the range, damage and attack speed greatly.

### Cannon Tower

A slower tower that shoots cannonballs that deal damage in an area of effect. It is slower than the archer tower, but has greater range. Upgrades increase the range and damage greatly, while increasing the attack speed by a small ammount.

### Ice Tower

A Special tower that deals damage per second to a unit within range while slowing it. Upgrades increase the damage per second and the slow greatly, while increasing the range by a small ammount.

## Enemies

Each of the 19 unique waves contain a different enemy. Most of the odd waves contain basic units while even waves contain bosses that follow the previous rounds theme.

While in appearance the enemies don't change much (all of them colorful geometric forms), a collection of traits were implemented to make them feel different:

### Health

The ammount of damage needed for an enemy to be destroyed.

### Armor

Armor reduces damage from attacks, but does nothing against damage per second from the ice tower. Greater armor scores encourage players to upgrade their towers instead of winning the game by just filling the map with level one towers (a strategy that worked wonders in the early builds, but was detrimental to the puzzle solving aspect of the game).

### Speed

Greater speed shortens the time an enemy stays inside the range of a tower, encouraging the player to spread its defenses. The difference in speed also helps give personality to each enemy, making some stand apart for being very fast and others for being tremendously slow.

### Prowl and Crumbling

Enemies with prowl start the round with their health bars turned blue. While prowling, enemies have different speed and can't take damage. A prowling enemy loses prowling once they're hit a certain number of times. Once an enemy loses prowl, their health bar turns green and they can be damaged. Losing prowl triggers a change in enemy behaviour, making some faster, some slower (one enemy even shortcuts the path when leaving prowl).

While multiple enemies have Prowl, Crumbling is unique to one kind of enemy, the Golem. It spawns with an insane ammount of armor, losing some with each hit it takes. Once the armor score hits zero, the health bar turns red, the unit's speed decreases and their armor is set to amplify damage taken.

### Flying

Flying enemies don't need to follow the winding path, going instead in a straight line to the end of the path.

## Acknowledgments

A huge thanks to everyone that tested the game through each iteration. Especially the early ones where it was easily breakable and barely entertaining.
