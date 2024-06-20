# Creating Sound Installations with Unity and Max: Resonance Rift

## About
**Resonance Rift** is an audio-based game developed as a semester project for Virtual Acoustic Reality (Winter Semester 2023/24) at the Audio Communication Group at TU Berlin. The game provides an immersive ambisonic sound experience, where players navigate dark, foggy mountain ridges guided by audio signals, amidst the distraction of wandering boids.

## Aims
1. **Example for a Ambisonics Soundscape in Unity:** To create an affordable alternative for integrating ambisonics in Unity using Cycling '74 Max and the spat5 library. The game communicates with Max via OSC (Open Sound Control), featuring a musical backbone based on boids, soundscapes, and granular synthesis of samples from Gregorian choirs.
2. **Multimodal Audio-centred Input System:** To integrate Supperware's Headtracker into Unity, enabling seamless control with head movements and mouse integration. This system also integrates well with the Input System for [Unity Starter Assets Pack](https://assetstore.unity.com/packages/essentials/starter-assets-third-person-character-controller-196526), making it particularly useful for audio-centered games.
3. **Realtime Measurement Integration:** To develop real-time measurement in Unity for calculating reverb and delay modulations (currently in development).

## Concept
### Audio Games
Audio games rely primarily on sound for gameplay mechanics, offering a unique sensory experience that challenges players' auditory perception and orientation.

### Unity and Max MSP
- **Unity**: A popular game development engine used for creating 3D and 2D games and interactive experiences.
- **Max MSP**: A visual programming language for music and multimedia, used to create interactive audio and visual applications.
- **OSC (Open Sound Control)**: A protocol for networking sound synthesizers, computers, and other multimedia devices for purposes such as musical performance or show control.

### Connecting Unity and Max MSP via extOSC
Unity and Max can be connected using the [extOSC](https://assetstore.unity.com/packages/tools/network/extosc-56657) library, allowing for real-time audio signal processing and control between the two platforms.

### Artistic Concept
Resonance Rift integrates the participant's ability to move freely within a 3D virtual space using a head tracker for orientation, resulting in audio experiences that respond dynamically in real-time to their movements and interactions with boids. Participants can influence the soundscape through their movements and interactions, shaping their auditory journey through the virtual world. The flocking behavior is central to gameplay, requiring players to maneuver through tight spaces and avoid obstacles. The closer the flock gets to an obstacle, the more hectic and crammed everything becomes, challenging the player to maintain control over the sonic environment. The goal is to develop adaptive soundscapes that dynamically respond to the participant's movements and interactions with virtual boids and obstacles. As the participant explores the virtual space, the behavior of the boids and the characteristics of the soundscape evolve in real-time, creating a responsive audio experience. Moving through different areas of the environment triggers changes in the density and distribution of boids, leading to corresponding shifts in the sonic landscape.



- **Boids**: The game leverages the concept of boids—algorithmic models of bird flocking behavior—to create dynamic soundscapes. Boids simulate the natural movement of flocks, creating an evolving and unpredictable auditory experience.
- **Granular Synthesizer**: A granular synthesizer breaks down sound samples into small "grains" and then recombines them in various ways to create new textures and timbres. This method allows for intricate manipulation of sound, producing unique and complex auditory experiences.
- **Gregorian Choirs**: The game incorporates samples from Gregorian chants, which are a form of plainchant used in the medieval church, characterized by monophonic, unaccompanied vocal music. These chants provide a simple musical ground while resulting in complex and still harmonic granular synthesis outputs.


### Integration of Granular Synthesis
The movements of participants control the behavior of virtual boids, which in turn affect the soundscape generated through granular synthesis. Granular synthesis manipulates the sound based on the proximity of the flock to obstacles:

- **Grain Duration**: Adjusting the duration of grains based on the flock's proximity to obstacles influences the perception of tension and calmness. Shorter grains signify tension or danger, while longer grains create a sense of calmness and safety.
  - Far from obstacles: Longer grain durations for smooth and serene sounds.
  - Near obstacles: Shorter grain durations for chaotic and turbulent sounds.
  
- **Grain Pitch**: Modulating the pitch of grains adds tonal variation and emotional depth.
  - Approaching obstacles: Higher pitches signal danger or urgency.
  - Peaceful moments: Lower pitches create a relaxed atmosphere.
  
- **Grain Density**: Manipulating grain density affects the complexity of the sonic texture.
  - Encountering obstacles: Higher grain densities create dense and intricate soundscapes.
  - Tranquil moments: Lower densities provide contrast and relief.

Every grain of sound is represented by a sound particle moving in space. The integration between the characteristics of the particle swarm and the parameters of the granular synthesizer creates a responsive musical instrument, similar to the concept explored by Antonino Modica in the project "Critical Mass" (https://github.com/antoninomodica/critical-mass).

## Contents of the GitHub Repo
- **C# Files for Integrating Headtracker and OSC into Unity**: Already Integrated in the Unity project. These scripts merely demonstrate how to use extOSC to send messages between Unity and Max and to integrate OSC, as well as the Mouse/Headphone Input. 
- **Max/MSP Patcher**: Includes spat5 integration to simulate boids, encode, and decode in the HOA (Higher-Order Ambisonics) domain.
- **Unity Project Folder**: Download the entire folder from [this link]().

## Installation
### Adding Unity Project Folder via Unity Hub
1. Download and unzip the Unity project folder from the provided link.
2. Open Unity Hub and click on the "Add" button.
3. Navigate to the unzipped project folder and select it.
4. Ensure you are using Unity version 2022.3.24f for compatibility.

### Setting Up Max/MSP
1. Open the file `Unity_OSCPatcher.maxpat`.
2. All OSC ports should already be set.
3. For easy use, switch to the presentation mode in Max. If you need to change ports, please exit the presentation mode first.

### Connecting the Headtracker
1. Obtain Supperware's Bridgehead from [Supperware's website](https://supperware.io/bridgehead).
2. Install the software and set the sending frequency to 100Hz.

### Starting the Game
1. Start the Max Patcher
2. Calibrate the Headtracker (by double clicking the Head Symbon in BridgeHead)
3. Starting the Unity Scene

Enjoy your audio journey with **Resonance Rift**!
