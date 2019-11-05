# Documentation


## Tools and frameworks:

Considering the kind of software we have been asked to develop, which is a mobile multiplayer game involving actions such as shoting and healing, we have thought that it would be better to build it using Unity (which is a game engine for game development) instead of Android. We expect with Unity to be able to build a far more visually appealing game in a much simpler way with higher growing possibilities for the game, due to all the resources Unity offers for game development.

## Architecture

### Main concerns

The software is build using an MVC (model-view-controller) pattern: the view, the model and the controller have specific tasks and should not be coupled together if we expect this prototype to be the base of a bigger development.

The view has the task of building the interface of the game and asserting the user's inputs, communicating those to the controller. 
The controller manages the game. It’s the link between the actions in the game and the model. It verifies that an action is possible, if the game is over and it manages the variations in the model’s data due to the actions happening in the game.

The model is built around the class Player. Every player has its own instance of Player with all the values associated with it (such as health points, time to act, etc) stored in that instance. The view does not communicate with the model nor the model communicates with the view; as we said, every data and view update is done via the controller.

### The bots

To implement the bot's behavior, a custom made approach would have involved multithreading, shared memory and communication between threads, potentially blocking the main thread which is critical for the interface's behavior. This approach would have been much more error prone for a feature that's only going to be implemented for a prototype and possibly going to be discarded in the future if we implement a client-server approach for multiplayer gaming. In that case it wouldn't be anymore the client who would manage the bot's behaviors but the server, so all the clients have the same information about the bot's actions.

The decision to build the game using Unity is not only for visual reasons but moreover because of the wide amount of tools it offers us. A multithreading behavior is a pretty common need in gaming development and Unity already provides multiple ways to solve that. 

We added a bot behavior to the game dolls so that each doll has its own behavior. 

## Class index

### Model

#### Player

The Player class encapsulates the information related to a player. A
player doesn't act by his own communicating with another player, if it
does it's because the controller has ordered it to do so.

That attributes of a player are:

* `_id`: identification number
* `_hp`: health points
* `_nextActionDateTime`: after which a new action is possible
* `_lastActionDateTime`: date of the last action 
* `_distribution`: probability distribution used for determining the
  value of an action

The main methods associated with a player are:
* `Strike`: receives another player as a target
* `ReceiveStrike`: asserts a strike from another player
* `Heal`: asserts a healing order
* `WaitingTimeToActionIsOver`: returns if the player is allowed to act
  (due to the time constraint between actions)


#### Action: strike and healing

A healing or a strike done by a player is an action, so they extend from
action. The value of these actions is set in the constructor using a
random number and a distribution. Although in these stages of the
development the properties and the behavior of the actions are limited
to bring us the value of the action (i.e. the health points to add due
to healing or to remove due to a strike), it's expected that in the
future new actions and new behaviors with new properties might be
implemented.

#### Probability: Distribution

For determining the amount of damage a strike does to another player or
the amount of healing, we have implemented probability distributions. As
there are multiple known probability distributions (uniform,
exponential, poisson, normal, gamma, etc...) an bastract class
`Distribution` has been implemented in order to allow an easy appending
of new probability distributions via inheritance. For the moment only
two distributions are implemented: the `Uniform` and the `Exponential`
distribution.


### Controller

