# Dumb Waiters
A competitive multiplayer game where players try to make the most money as a server in a restaurant. Teams can boost or sabotage other teams, providing more tips and better satisfied customers.

Players can use their hard earned money to buy items in game, that can help them and sabotage the other team. 

The Winning team is the one that gets the most money after the alotted time. 

Reputation is awarded for winning games, completing achievements and for donating money to the group. Players reputation will dictate their level, the higher the players level, the more items they can buy in the shop.

Before each game, players can also take money from the group to help lead them to victory. There are leaderboards for the players group so that they can see who has donated the most money and also who has taken the most money from the group.

## Key Structure
The unity project follows the following structure
- **Animations**: Animations in game, currently using Legacy animations for simplicity
- **Art**
    - **Materials**: Base folder for 3d Art
    - **Sprites**: Base folder for 2d Art
- **Networking**: Base folder for all networking code, does not include code that inherits from NetworkBehaviour
- **Prefabs**
    - **Characters**: Players/Customers
    - **Food**
    - **Interactables**: Interactable objects, all have the Interactable class attached
    - **Modifiers**: Modifiers that can be bought from the shop
    - **Scene**: Our level prefab (table layout, environments, placements)
    - **UI**
- **Scenes**
- **Scripts**
    - **Characters**: Our movable characters
    - **Groups**: Group management and setup 
    - **Managers**: Our core managers that handle the progression and setup of the game
    - **Mechanics**: The core mechanics of the game and intractions that players can have
    - **UI**
    - The scripts folder also contains the HACK code that will be optimised and placed in the correct location later
- **SUGAR**: From [Unity Asset Store](https://assetstore.unity.com/packages/tools/network/sugar-social-gamification-107078)

### Groups
The game contains 3 groups, these are interacted with similar to clans, a group of people working together to reach a common goal and aiding each other where possible, but without a user to user experience. Upon starting the game as a new user, the game checks if the player is in a group, and if not prompts them to join one. A player should only ever be in one group and should not be able to change. (similar to [pokemon go](http://pokemongo.wikia.com/wiki/Teams)).

Matchmaking will never take into account which group a player is part of, but players will be able to contribute to their group to obtain greater reputation and level up to get access to better items in the shop.

### Server/Client Behaviour
The server for Dumb Waiters contains all the game logic, with clients being 'dumb' objects that are only able to send commands. Clients must log into SUGAR when they start the game, allowing for them to save their progress and resources, needed for making purchases and progressing in the game. During the main gameplay, the server acts as a shared screen, with all clients seeing a controller and inventory system that they can interact with on their mobile device. 

The Server and Client implement UNET [Command](https://docs.unity3d.com/ScriptReference/Networking.CommandAttribute.html), [ClientRPC](https://docs.unity3d.com/ScriptReference/Networking.ClientRpcAttribute.html) and [TargetRPC](https://docs.unity3d.com/ScriptReference/Networking.TargetRpcAttribute.html) Attributes, when players connect to the server, they notify the server that they have connected, the server will then assign the player an Id that maps to a player created in the game. Then all successive commands only need to contain the Id of the player object for the Server to make changes to the correct object.

As the server acts as a shared screen, the clients will not load any of the level in to the scene, to save on performance.

### Build Process
The game does not require any specific data to be specified prior to build and follows the standard unity build process per platform.

The game is intended to run on mobile devices with a server running on a PC. The server can be run from the Unity Editor or from an executable. For the best experiences, clients should connect via mobile devices, therefore an .apk must be built and installed on device, if running a local version of sugar, see [details below](#running-sugar-locally).

### Playing the game
Currently there needs to be a server instance of the game running, preferrably on a PC, but could probably work on a mobile device, and clients should connect 

### Running SUGAR Locally
In order to run SUGAR locally and connect to the local version on a device, use [ngrok](https://ngrok.com/) for a simple integration, without need to open ports on the firewall, ngrok will open a tunnel to your SUGAR service and provide both a http and https url to connect to. 

The SUGAR.config in streaming assets will need to be updated to use the provided url before building a new client.

## SUGAR Usage
Dumb waiters is currently using SUGAR version 1.0.2 taken directly from the Asset Store.

# Roadmap
## Planned 
### Features
- Ability to take resources from the group
- addition of more items to buy
- Full implementation of reputation and levelling up system
- 2 stage request process, request a drink, then a sugar cube
- Audio Implementation
- Player Characters
- Handle Player Disconnection

### Updates
- Update SUGAR unity to the latest version (1.1.0 includes remember login)

## Known Bugs
- Currently players spawn on both client and server, this does not need to be the case
- Clients collide with newly placed items, causing them to be propelled in one direction.
- Leaderboards for most donated and taken could easily be exploited by repeated transactions between player and group and vice verse, this should be a net value.

## Stretch Goals
- More diverse environments with more hazards
- Different table layouts
- More food types
- Different Customer types, whales and cheap customers
- Cosmetic bonuses for leveling up