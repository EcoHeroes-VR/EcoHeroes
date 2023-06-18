
# Table of Contents

1. [**Menu Manager**](#menu-manager)
    - [Overview](#overview)
    - [Usage](#usage)
        - [Menu Types](#menu-types)
    - [Additional Details](#additional-details)
    - [Author](#author)
---
2. [**Vertical Auto Content Scroller**](#vertical-auto-content-scroller)
    - [Overview](#overview-1)
    - [Usage](#usage-1)
    - [Additional Details](#additional-details-1)
    - [Author](#author-1)
---
# Menu Manager

## Overview

Provides functionality to open and close menus in the game (essentially a script to change states of the menu). It uses a dictionary to store references to the menu GameObjects in the scene.
## Usage

To use the Menu Manager in your Unity project, follow these steps:

1. Copy the `MenuManager.cs` script into your project's scripts folder.

	> *Note: Do not attach the `MenuManager` script to any GameObject in your scene. This is a static class!*

2. Create an empty GameObject named "Menu" with a child UI canvas named "Canvas"

	> *Note: the script searches for these GameObjects in the hierarchy, so the names need to be exactly the same*

3. Create separate GameObjects under the "Canvas" as children for each menu type, containing the content you want to show.

	> *Note: all menu types written in the enum `MenuType` need to match with the children of the "Canvas".*

	> *Example: `MAIN_MENU` as a `MenuType` must have a GameObject with the name "MAIN_MENU" in the scene.*

4. In your game code, you can now open menus using the `MenuManager.OpenMenu(MenuType menuToOpen, GameObject callingMenu)` method. Pass the type of menu you want to open and the GameObject of the currently active menu that is calling to switch you between menus.
	```csharp
	public void SettingsButton()
   {
	   MenuManager.OpenMenu(MenuType.SETTINGS_MENU, gameObject);
   }
	```
   The specified menu will be activated, and the calling menu will be deactivated in the scene.
## Additional Details
### Menu Types

The `MenuType` enum defines following menus:

- `MAIN_MENU`: The main menu of the game.
- `SETTINGS_MENU`: The settings menu.
- `NAME_CHANGE_MENU`: The menu for asking the player's if he wants to change his name.
- `NAME_GENERATOR_MENU`: The menu for generating a random name.
- `CREDITS_MENU`: The menu containing the credits.

	> You can add, remove or modify menu types in the `MenuType` enum to fit
	> your specific game's needs. 
	**Always make sure that you use the same
	> names in the scene, otherwise it will throw an error!**

## Author

This Menu Manager script was created by Marc Fischer.

Feel free to modify and enhance the script to suit your project requirements.


# Vertical Auto Content Scroller

## Overview

Provides functionality to automatically scroll content vertically within a mask area. It supports two scroll styles: "FlipFlop" and "Loop". You can configure the scroll speed, delay before scrolling starts, and the start direction of the scroll. 
It is mainly designed to move "TMPro Text" objects and UI images, but it will probably work with any standard Unity UI Element that has a similar RectTransform as the image.

## Usage

To use the Vertical Auto Content Scroller script in your Unity project, follow these steps:

1. Copy the `VerticalAutoContentScroller.cs` script into your project's scripts folder.

2. Create a UI Image that represents the viewport for the content you want to scroll. Position and size the mask area for your viewport.

> Note: This object should have a RectTransform, Mask, CancasRenderer and
> Image component attached to it. The Image component needs to have the
> "Maskable" property activated.



4. Create a UI Image or a Text(TMPro) for the content you want to scroll. Position the content within the mask area.
> Note: This object should have a RectTransform component attached to it as well.

6. Attach the `VerticalAutoContentScroller` script to the content GameObject.

7. In the Inspector, assign the required references:
   - Assign the RectTransform component of the **content** GameObject to the `_targetRectTransform` field.
   - Assign the RectTransform component of the **mask** GameObject to the `_maskRectTransform` field.
   - Configure the desired **values** for `_delayBeforeScroll`, `_scrollSpeed`, `_startDirection`, and `_scrollStyle`.

8. Run your Unity scene to see the scrolling effect in action.

## Additional Details

### Start Direction

Determines the initial direction of the scroll:
- `Down`: The content will initially scroll downwards.
- `Up`: The content will initially scroll upwards.

### Scroll Style

Determines the behavior of the scrolling:
- `FlipFlop`: The content will scroll back and forth.
- `Loop`: The content will continuously loop.

### Disabling the Script

To stop the scrolling effect, disable the GameObject with the `VerticalAutoContentScroller` script attached. 
>Note: This will reset the values and stop the coroutine.

## Author

This Vertical Auto Content Scroller script was created by Marc Fischer.

Feel free to modify and enhance the script to suit your project requirements.

