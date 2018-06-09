# Homu-Editor
Collection of utilities for the Unity Editor.  

[MIT License](LICENSE)  
You may use these freely provided you give credit to "Teh Lemon".  
I make no promises over the quality or up-to-date-ness of the code. Nor will I provide support or take any responsibility if something goes wrong.  

# Install
Copy the contents of the Assets folder into your Assets folder.  
Anything in the Assets/WakabaGames folder can be moved as you wish but any scripts found in an "Editor" folder must also be in a folder named Editor.  
The "Gizmos" and "Editor Default Resources" must be placed in the root of the "Assets" folder. These can't be moved.  
Some of these may require setting Unity to use .net 4.6/C# 6.0.  
These are only tested against whichever Unity version I happen to be using (usually the latest).  
Any extra instructions will listed in the comments at the top of the file or functions.  

# Contents

## Extra Hotkeys (EditorShortcutHotkeys.cs)
**Alt+D** in the Editor to deselect all game objects in the scene.  
**F5** to Play/Stop the game.  

## Generate Scripts from Template (ScriptGenerator.cs and the Editor Default Resources folder)
Create new scripts based on the template provided in the Editor Default Resources folder.  
Use this if you're tired of Unity constantly overwriting your custom script templates every update.  
Found in the **Create/Wakaba Games/New Script** menu when you right-click in the Project hierarchy.  
Keywords and customization can be found inside ScriptGenerator.cs file.

## Mascot Panel (MascotPanel.cs and the Editor Default Resources folder)
A panel for the Editor that displays an image for that boost in motivation.  
Access from the **Windows/Mascot** menu.  
Name your image "mascot" and place it in the folder "**Assets/Editor Default Resources/**".  
Supports .jpg, .png and static .gif. Though you can easily add more formats into MascotPanel.cs yourself. 
![Mascot screenshot](Doc/Images/Mascot.png)

## Revert to Prefab (RevertAllPrefabs.cs)
Reverts all the selected gameobjects in the Editor back to their prefab state.  
Found in the **Tools** menu.

## Simulate Physics (SimulatePhysicsEditor.cs)
Manually simulates the physics on selected game objects in the editor until they've come to rest.  
Use this to scatter a bunch of objects around the scene in a natural way without having to carefully place each one on the ground.  
Only select game objects that have colliders but no rigidbodies. It will automatically add the rigidbodies and remove them afterwards.  
Found in the **Tools** menu.


## Sticky Notes (StickyNote.cs and the Gizmos folder)  
In-Scene view sticky notes represented with gizmos. Useful for long-term or team projects.  
Just add the **WakabaGames/StickyNote component** onto any gameobject.  
Add it onto an empty (child) game object if you want to move it around.  
![StickyNotes screenshot](Doc/Images/StickyNote.png)