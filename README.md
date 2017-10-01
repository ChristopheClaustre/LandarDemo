# LandarDemo
Source of the future demo for the next SquirrelSoft's game

**Coding rules [here](CodingRules.md)**

## 0. Unity's version

Actual version of Unity to use : 2017.1.0f3

## 1. Folder

 * **bin** : latest working build
 * **Teddy** : a folder for teddy's shitty things
 * **Jeu 0 - Demo** (Never change this folder's name) : Unity's source of the project

## 2. Organisation

 * **Never** have two persons editing the same scene/asset ! NEVER !
 * If you detect an issue with a *Finished* Asset you should warn the other persons who may be using it and then push your modifications. Never push it if you can't warn the other users before.
 * **Always** check for new commits before begining your work.
 * **Always** commit and push your work (if you only modified the file you are allowed to).
 * Push only working (or at least buildable) version of your work.

 * Don't modify the project's settings in unity without warning before @ChristopheClaustre

### A. Example of organisation

 * Teddy working on a scene named "TestAnimation" to test, modify and create his animation
 * Xavier working on a scene named "TestHardFeaturesTeddyWanted" to test or modify existing script and create new one about the fow or the luminosity management
 * Christophe working on a scene named "TestUI" to test or modify existing script and create new one about the UI

## 3. Dictionary

 * Asset : Every files or folders which is under the "Assets" directory