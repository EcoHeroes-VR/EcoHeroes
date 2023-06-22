# Dialog Manager

The dialog manager consists of various objects:
- **DialogManager:** Manage the dialogs an start a dialog squence
- **Dialog Package So:** Scriptable Object, contains a list of dialogs
> It is possible to change the "Dialog Package So" during runtime. <br>
> ```_dialogManager.DialogPackage = package```

## Create new Object with Dialog
1. Create a new Dialog package So <br>
    - Add new Dialogs in the list with the name of dialog, the name of 
      speaker and the dialog text.
    - Optional: To fill the NextDialogName, to link to a another dialog in the same Package
2. The Object need a DialogManager with the "Dialog Package So"
   - Otional: Add a script with inherit XrHovering, this adds a XR Grab Interaction, 
      Rigidbody and and a Collider. In the "Hover" and "Over Exit" events add the two method
      "OnHoverEnter" and "OnHoverExit"

## Start a dialog
   - To start a specific dialog call the NextDialog() method. <br>
   ```_dialogManager.NextDialog("dialogname")``` or get the next linked dialog with ```_dialogManager.NextDialog()```

> When a Dialog is open a button is on the left hand visible to go nect dialog or close the subtitle

## Events
> When a dialog sound is finished playing or aborted, an event is triggered. Use
> ```_dialogManager.OnCurrentDialogEnd.AddListener``` to listen the event.
> -  First parameter (string) is the current dialog name
> -  Second parameter (GameObject) is the dialog manager gameobject


You can automate the dialogsystem when a dialog sound is deposited, proceed as follows:

__Attention:__ There is a bug here, if the NextDialog is called in the event, a stackoverflow error occurs!
```
_dialogManager.OnCurrentDialogEnd.AddListener((dialogName, go) => {
   if (go.name == this.name) _dialogManager.NextDialog();
});
 ```
