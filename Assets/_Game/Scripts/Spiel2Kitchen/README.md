# Table of Contents

1. [**Game Manager**](#game-manager)
    - Usage
    - Important Methods
    - Author
  ---
2. [**High Score Mechanic**](#high-score-mechanic)
    - Usage
    - Important Methods
    - Author
  ---
3. [**Slicing Objects**](#slicing-objects)
    - Usage
    - Important Methods
    - Author
  ---
4. [**Dough Manger**](#dough-manager)
   - Usage
   - Important Methods
   - Author
---
5. [**Inventory for VR**](#inventory-for-vr)
   - Usage
   - Important Methods
   - Author
---





# Game Manager
This class creates a game loop and holds all necessary details.
## Usage
To use the Game Manger in your Unity project, follow these steps:
1.  Add the KitchenGameManger script to your project's scripts folder.
2. Attach the KitchenGameManager script to an empty GameObject in your scene.
3. Set up the required references and parameters in the inspector:
   - `DialogManager`: Reference to the DialogManager.
   - `TimeText`: Reference to the TextMeshProUGUI component for displaying time.
   - `ScoreText`: Reference to the TextMeshProUGUI component for displaying score.
4. We start the game when you pick up the Backpack. So to start the game call the _OnBackpackPickUp()_ method
   
## Important Methods
### OnBackpackPickUp()
-   Description: Is called when the backpack is picked up to start a timer.
-   Usage: Call this method when you want to start the game. If you want to choose a different starting point, just rename the method.

## Author
This Game Manager script was created by Theresa Mayer and Lukas Konietzka.

---




# High Score Mechanic
This class allows you to implement a high score mechanic.
## Usage
To use this class, create an instance where ever you need it.
For example: __score = gameObject.AddComponent<HighScoreKitchen>();_
>note: We do this in the KitchenGameManager.

## Important Methods
### GetCurrentScore()
- Description: Return the current value of the field __currentScore_
- Usage: Whenever you need to show the current Score, call this method.

### CalculateCurrentScore()
- Description: Algorithm for calculating the score
- Usage: When the game is finished and you've got the basicScore and the timeSpan, call this method.
- Args: This method needs the basicScore and the elapsed time since the game started.

### UpdateHighScore()
- Description: Set the high score, if the current score is higher than the high score.
- Usage: When the new score is higher than the current High Score, call this method.

## Author
This High Score Mechanic was created by Theresa Mayer and Lukas Konieztka.

---




# Slicing Objects
With this class it's possible to cut any object in the layer _Sliceable_ into two parts.
For this job we use the package EzySlice from David Arayan.
For more information about the EzySLice package check out Davids github repository:
https://github.com/DavidArayan/ezy-slice

## Usage
To use this feature you need an object with which you want to cut, e.g. a knife.
Add the script _SlicingObject_ to this object and
configure your inspector as shown:
- `tartSlicePoint`: cutting start point (creat this as child of the cutting object)
- `endSlicePoint`: cutting end Point with _velocityEstimator_ (creat this as child of the cutting object)
- `sliceable`: Layer that should mark which objects will be cut
- `crossSectionMaterial`: the color of your cut surface

## Important Methods
### Slice()
- Description: This method uses the EzySlice package and cuts a given GameObject into two parts by adding two new objects and destroying the old one.
- Usage: To use this method call it with the object that has to be sliced in the args.
- Args: Object that has to be sliced

### SetupSliceComponent()
- Description: Add all necessary Components to the given GameObject.
- Usage: You should call this method when you slice an object into two parts. With this method you can add all necessary components to the created objects
- Args: GameObject that gets the components

## Author
This feature was created and added by Theresa Mayer and Lukas Konietzka.

---



# Dough Manager
This class handles all interactions with the dough GameObject to build a pizza object.

## Usage
To use this class attach the Script to a GameObject in the scene and specify the objects that should be created. Note that these objects should be the base 3D models and not a Unity Prefab.
This class generates an object on a calculated point in the scene.
To generate these objects, call the method CreateToppingAt()
> note: We use this to spawn sauce and cheese on the pizza

- `toppings`: List of objects that can be created.

## Important Methods
### CreateToppingAt()
- Description: Method to spawn a pizza topping object.
- Usage: For example you can call this method in an event-function to spawn some objects
- Args: Tag of an Object, so we can distinguish between objects.

### GenerateToppingSpawnPos()
- Description:   Method to create the specific coordinates to spawn a object.
- Usage:         Call this method in the _creatToppingAt()_ methode to generate an spawn point.

## Author
this feature was created and added by Theresa Mayer and Lukas Konietzka.

---



# Inventory for VR
This class handles the inventory for the Player.
For this job we use the Unity-XRSockets.
## Usage
To use an inventory in a VR-Game you have to create a GameObject with the number of slots you need.
Then attach the InventoryVR script to your EventSystem and the slot script to each slot.
In the inspector you can specify the button to open the inventory and the object with which the inventory will be activated.
>note: We use the backpack object for activation.

- `inventoryVR`:Object with the inventory script
- `openInventoryOnButton`: Button on which you would like to open the inventory
- `activateInventoryOnObject`: Object on which you want to activate the inventory

## Important Methods
### OnButtonPress()
- Description: Activate the Inventory on pressing a specific button.
- Usage: It is a good idea to call this method in the _Update()_ method so it's possible to open the inventory at any point. 

## Author
This feature was created and added by Theresa Mayer und Lukas Konietzka.

---