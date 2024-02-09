# The-City

A procedurally generated city that you can explore, with houses, buildings, and cars.

![The city playthrough](https://github.com/harleyk314/The-City/assets/58278456/3543cfbc-bf31-40cc-a711-8da9fa2d7b01)

### Main Controls
- Use the arrow keys to move.
- Pressing ‘Q’ alternates between walking speed and driving speed.
### Extra Controls
- Holding ‘Z’ shows a second camera perspective.
- Tap ‘O’ repeatedly to fly.

### Development
I generated the city and “cars” using cloning and procedural generation.
The cars use a relatively simple path-finding algorithm:
1. Randomly choose a set of co-ordinates, which will be the target destination
2. Move in a direction that is perpendicular to the city
3. Once the destination is reached, start over.
This algorithm helps to make the cars move in an interesting way, and prevents them from going too far outside of the city.

### Further Improvements
- The game could be made more interesting by using music and sound effects, quests, and menus.
- Ideally, I would like the cars to travel only on roads. This is made slightly trickier by the structure of the city (such as large buildings)
- Sometimes, cars get stuck in a loop. One theory for why this happens is that the cars may struggle to reach their target destination, but it may be to do with another issue entirely.
- Finally, implementing object collisions for all relevant interactions would be required for most games. I would use online tutorials and Unity’s debugging tools as necessary.

