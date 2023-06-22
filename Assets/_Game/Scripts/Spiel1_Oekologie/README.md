# Table of Contents

1. [**Level 1 Game Manager**](#level-1-game-manager)
    - [Overview](#overview)
    - [Usage](#usage)
        - [GetTargetPosition](#gettargetposition)
        - [Eat](#eat)
        - [StopEating](#stopeating)
    - [Author](#author)
  ---
2. [**Farmland Manager**](#farmland-manager)
    - [Overview](#overview-1)
    - [Usage](#usage-1)
    - [Additional Details](#additional-details-1)
    - [Author](#author-1)
  ---
3. [**Animal Script**](#animal-script)
    - [Overview](#overview-2)
    - [Usage](#usage-2)
    - [Additional Details](#additional-details-2)
    - [Author](#author-2)
  ---

# Level 1 Game Manager
## Overview
Creates a gameloop, including timer and scoring. Manages the interactions with the farmland and animals.
## Usage
To use the Game Manger in your Unity project, follow these steps:
1.  Add the Level1GameManager script to your project's scripts folder.

2. Attach the Level1GameManager script to a GameObject in your scene.
3. Set up the required references and parameters in the inspector:
   - `FarmlandManager`: Reference to the FarmlandManager.
   - `GameDurationInSeconds`: Duration of the gameloop in seconds.
   - `DialogManager`: Reference to the DialogManager.
   - `TimeText`: Reference to the TextMeshProUGUI component for displaying time.
   - `ScoreText`: Reference to the TextMeshProUGUI component for displaying score.
4. Call the `StartGame` method to start the game and the timer.
	> Note: When the game time is over, the `StopGame` method is called automatically. Calculating the score and updating the UI.
> 
## Additional Details
### GetTargetPosition
-   Description: Retrieves the target position from the `FarmlandManager`.
-   Returns: The target position as a `Vector3` (or `null` if no target position is available).
-   Usage: Call this method when you need to get the position where an animal should move to eat.
	>Note: the null value will be handeld in the animal script.
	
### Eat
-   Description: Notifies the `FarmlandManager` that an animal is eating at the target position.
-   Parameters:
    -   `targetPosition`: The position where the animal is eating.
-   Usage: Call this method when an animal starts eating. 
	> Note: Informs the `FarmlandManager` to handle this. If you want to change the behaivor you need to look there.
### StopEating
-   Description: Notifies the `FarmlandManager` that the animal has finished eating and releases the target position.
-   Parameters:
    -   `targetPosition`: The position to release.
-   Usage: Call this method when an animal has been petted and needs to release the target. This method informs the `FarmlandManager` to make the target position available for other animals to use.
	> Note: Informs the `FarmlandManager` to handle this. If you want to change the behaivor you need to look there.

## Author
This Game Manager script was created by Marc Fischer and Manuel Hagen.

Feel free to modify and enhance the script to suit your project requirements.

# Farmland Manager
## Overview
Holds main game field data (in array representation). Manages and handles interaction for the Farmland.
## Usage
To use the FarmlandManger in your Unity project, follow these steps:
1. Copy the `FarmlandManager.cs` script into your project's scripts folder.

2. Attach the `FarmlandManager` script to a GameObject in your scene.

3. Set the required variables in the inspector:
   - `FloorSquareArray`: The array representation of the game's field.
   - `squareCount`: The number of squares in the field.
   - `areaSize`: The total size of the game's field.
   - `tilePrefab`: The prefab for the ground tiles.
   - `centerPoint`: The center point of the game's field.
   - `seeds`: The particle system for seed particles.
   - `beeParticleSystemPrefab`: The prefab for the bee particle system.
   - `flowers`: An array of growable flowers.
   - `plants`: An array of growable plants.
   - `highGrowthScore`, `midGrowthScore`, `lowGrowthScore`: Score values for different growth states.
   > Note: A gizmos will be drawn in the scene if you change the field count and size properties

4. Run your game and interact with the field.
## Additional Details
This script is very well-structured, so it is recommended to look directly at the code if you need some specific information.
-   **Floor Variables**
	> These store information about the game field, such as the array representing
	the field, square count, area size, square size, collider height, tile
	prefab, and center point.

-   **Particle Variables** 

	> These handle the particle systems used for seed particles (collision events)
	and bee particle systems.

-   **Growth State Thresholds**

	> These define the threshold values for low, mid, and high growth based on
	seed saturation and time in seconds.

-   **Growing Variables** 

	> These control the growth process, including the number of growth ticks
	per second, the time elapsed since the last growth tick, and a
	dictionary to store positions of growing squares.

-   **Animal Variables** 

	> These are for approaching animals, who want to eat, such as a list of
	target squares.

-   **Flower Variables**

	> These store arrays of growable flower and plant objects, along with a
	vertical offset for positioning.

-   **Score Variables** 

	> These variables represent the score values for different levels of
	growth.

-   **Structs and Enums**

	> These define custom data structures and enums used within the script,
	> such as the `Growable` struct representing a growable object, the
	> `FloorSquare` struct representing a square in the game field, and the
	> `GROWTH_STATE` enum representing the growth states of a field

## Author
This Farmland Manager script was created by Marc Fischer and Manuel Hagen.

Feel free to modify and enhance the script to suit your project requirements.

# Animal Script
## Overview

The Animal script controls the behavior of an animal. It manages the movement, interaction, and state transitions of the animal based on its current state.

## Usage
To use the Animal script in your Unity project, follow these steps:
1. Copy the `Animal.cs` script into your project's scripts folder.

2. Attach the script and a Rigidbody component on your animal you want to control.
	> Note: you will need a suited animal model with animations and an animator with transition variables. Also make sure to have a TurnTowardVelocity script on the model so it will always look where it is moving. Adjust the turnSpeed to your liking.
3. Assign the required references in the Inspector:
   - Attach the Animator component to the `_animator` field.
   - Assign the necessary Transform references to `_modelTransform`, `_raycastPoint`, and `_farmlandCenterPoint`.
   - Attach the ParticleSystem component representing the heart particles to `_heartParticles`.
   - Configure the values for `_heartParticleRateLow`, `_heartParticleRateHigh`, and `_petCooldownSeconds`.
   - Attach the necessary CapsuleCollider components to the `petCapsuleCollider` array.
   - Configure the values for `petThresholdSeconds` and `petThresholdSecondsHard`.
   - Configure the movement and behavior variables according to your requirements.

4. Set up the necessary colliders and triggers in your scene to interact with the animal.

5. Run your Unity scene to see the animal's behavior in action.
## Additional Details
### States:
- **Eating Behavior**

	> When the animal reaches the target position, it triggers the eating animation. 
	After a certain duration, it transitions back to the idle
	state and notifies the GameManager.

- **Idle Behavior**

	> The animal performs movement in random direction within a specified range. 
	It avoids obstacles by checking for collisions using raycasts.

- **Approach Behavior**
	> Approaches the target position in a straight line. When it reaches the target,
	it will turn its rigidbody kinematic and trigger the eat
	animation.

- **Leaving Behavior**

	> Moves away from the farmland center point

### Petting Interaction
When a hand collider stays within the trigger area of the animal, it triggers the petting action. The animal emits heart particles, and if the petting duration exceeds a threshold, it transitions to the idle state and notifies the GameManager.


## Author
This Animal script was created by Marc Fischer and Manuel Hagen.

Feel free to modify and enhance the script to suit your project requirements.


