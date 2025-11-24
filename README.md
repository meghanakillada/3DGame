# 3D Project "One-Pager"
### Game Name: Maze of Terror

## Authors
| Name | Goals |
| -------- | ------- |
| Maggie | Enemy AI, Locks & Keys |
| Rike | Game Flow/Logic |
| Shine | Game Design, UI, Lighting |
| Alex | Camera/Player Movement, Audio |

## Overview
Maze of Terror is a first-person horror escape game. You play as a player trapped in a dimly lit maze, hunted by a monster. Your goal is to navigate the maze, collect color-coded keys, and use them to unlock rooms holding other keys leading to the final escape door. You have three lives—each time the monster catches you, you lose one, and respawn at the starting point.

## Controls
The controls will be via mouse and keyboard:
- *Move Forward / Back / Left / Right* — W / S / A / D
- *Look Around* — Mouse
- *Sprint* — Left Shift (limited stamina)
- *Pause* — Esc

## Art Assets
- Player: Camera-based first-person view
- Enemy: Monster with chasing AI
- Maze: Modular prefabs forming corridors and small square rooms inside the maze
- Key: Glowing collectible keys, touching them automatically unlocks their corresponding door type
- Doors: Small hole-like openings that act as locked passages
- Lighting: Player has ambient local light around them (no full-maze illumination)
- UI Elements:
  - Key icons (color-coded) in the corner of the screen
  - Stamina bar
  - Lives counter (3 total)
  - Pause menu

## Audio Assets
Footstep Sounds: Player and enemy footsteps echoing in the maze
Monster Sounds: Distant growls/breathing that gets louder when nearby
Key Pickup Sound: A “ding” or “chime”
Background Music: Ambient tension music
Jump Scare Cue: Plays when caught

## Game Flow
Title screen:
- Buttons: “Start Game” and “View Controls” buttons
- Background: flickering lights and faint monster noises
- Game screen:
  - Player spawns at the maze start with visible stamina, lives, and key inventory UI
  - Explore the maze and small rooms to find colored keys (auto-collected)
  - Unlock corresponding doors to progress
  - The monster patrols the maze using NavMesh; it begins chasing upon spotting or hearing the player
  - Flickering lights and sound cues hint when the monster is close
  - Getting caught costs one life and respawns you at the start
  - Lose all lives = Game Over
  - Collect all keys and reach the final door to escape
- End screen:
  - If escaped: “You Survived! (Barely)” with an option to replay or quit
  - If caught: “You’ve Been Caught!” with the monster laughing and a retry option

## Potential Challenges
- AI Pathfinding: Ensuring the enemy navigates tight corridors using NavMesh effectively
- Lighting: Maintaining tension with limited lighting while keeping performance smooth
- Game Balance: Making the chase thrilling without being frustrating

## Extra Features (if time)
- Randomized maze layout
- Flashlight to navigate dimly lit maze
- Enemy AI predict player moves
- Multiple enemy types with unique chase behaviors
