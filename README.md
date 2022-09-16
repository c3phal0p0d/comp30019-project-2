

**The University of Melbourne**
# COMP30019 – Graphics and Interaction

## Teamwork plan/summary

<!-- [[StartTeamworkPlan]] PLEASE LEAVE THIS LINE UNTOUCHED -->

<!-- Fill this section by Milestone 1 (see specification for details) -->


11/09/2022 - 16/09/2022

Summary

We met as a team on 11/09 and 12/09  where we discussed our gameplay plan and how the game should look like.We discussed about the gameplay and mechanics which includes but were not limited to :
Randomly generated maze
Monsters 
Items around (weapons, stat increases) 
Goal: get from start to finish
Health bar
Stamina
Stats 
Droppable beacons / torches / checkpoint
Levels (each level is a new maze)
Can choose a starting difficulty
Gets harder with each level
Time trial
Lore pickups (permanent, may or may not be in a maze)
left and right walls have different textures 
3d UI ?
Camera Orientation - FP with Free look 
Vines and plants on walls
Door between sections
Having render distance 
Walls and atmosphere changes when close to enemies 
tasked to kill the  minotaur
sound effects


Teamwork plan 

Start mostly by implementing mechanics with minimal visuals.
These are the jobs we have allocated so far:
Have frequent meetings with the team 
Work in separate branches
Ask for help when not sure

TASK ALLOCATION 
Labyrinth layout generation - Andrew
Player controller - Rajneesh 
Game pickups - Meaghan :)
3D models - Natasha


<!-- [[EndTeamworkPlan]] PLEASE LEAVE THIS LINE UNTOUCHED -->

## Final report

Read the specification for details on what needs to be covered in this report... 

Remember that _"this document"_ should be `well written` and formatted **appropriately**. 
Below are examples of markdown features available on GitHub that might be useful in your report. 
For more details you can find a guide [here](https://docs.github.com/en/github/writing-on-github).

### Table of contents
* [Game Summary](#game-summary)
* [Technologies](#technologies)
* [Using Images](#using-images)
* [Code Snipets](#code-snippets)

### Game Summary
_Exciting title_ is a first-person shooter (FPS) set in...

### Technologies
Project is created with:
* Unity 2022.1.9f1 
* Ipsum version: 2.33
* Ament library version: 999

### Using Images

You can include images/gifs by adding them to a folder in your repo, currently `Gifs/*`:

<p align="center">
  <img src="Gifs/sample.gif" width="300">
</p>

To create a gif from a video you can follow this [link](https://ezgif.com/video-to-gif/ezgif-6-55f4b3b086d4.mov).

### Code Snippets 

You may wish to include code snippets, but be sure to explain them properly, and don't go overboard copying
every line of code in your project!

```c#
public class CameraController : MonoBehaviour
{
    void Start ()
    {
        // Do something...
    }
}
```
