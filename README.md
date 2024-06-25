# Resonance Rift: Creating 3D surround sound Installations with Unity and Max

![Screenshot of the Max Patcher](https://raw.githubusercontent.com/Farcyy/UnityMaxOSC_SoundInstallation/main/IMAGES/MAX_GUI.png)

## About
**Resonance Rift** is an audio-based game developed as a semester project for Virtual Acoustic Reality (Winter Semester 2023/24) at the Audio Communication Group at TU Berlin. The game provides an immersive ambisonic sound experience, where players navigate dark, foggy mountain ridges guided by audio signals, amidst the distraction of wandering boids. For Installation, please see the end of this README

## Aims
1. **Example for a Ambisonics Soundscape in Unity:** To create an affordable alternative for integrating ambisonics in Unity using Cycling '74 Max and the spat5 library. The game communicates with Max via OSC (Open Sound Control), featuring a musical backbone based on boids, soundscapes, and granular synthesis of samples from Gregorian choirs.
2. **Multimodal Audio-centred Input System:** To integrate Supperware's Headtracker into Unity, enabling seamless control with head movements and mouse integration. This system also integrates well with the Input System for [Unity Starter Assets Pack](https://assetstore.unity.com/packages/essentials/starter-assets-third-person-character-controller-196526), making it particularly useful for audio-centered games.
3. **Realtime Measurement Integration:** To develop real-time measurement in Unity for calculating reverb and delay modulations (currently in development).

## Contents of the GitHub Repo
- **C# Files for Integrating Headtracker and OSC into Unity**: These scripts demonstrate how to use extOSC to send messages between Unity and Max and to integrate OSC, as well as the combined Mouse/Headphone Input. 
- **Max/MSP Patcher**: Includes spat5 integration to simulate boids, encode, and decode in the HOA (Higher-Order Ambisonics) domain.
- **Unity Project Folder**: Download the entire folder from [this link](https://drive.google.com/drive/folders/17RPBBuXkqqB8YIoX0OZ1CD__UXu3OOF_?usp=sharing).

### Creative Concept
Resonance Rift integrates the participant's ability to move freely within a 3D virtual space using a head tracker for orientation, resulting in audio experiences that respond dynamically in real-time to their movements and interactions with boids. Participants can influence the soundscape through their movements and interactions, shaping their auditory journey through the virtual world. The flocking behavior is central to gameplay, requiring players to maneuver through tight spaces and avoid obstacles. The closer the flock gets to an obstacle, the more hectic and crammed everything becomes, challenging the player to maintain control over the sonic environment. The goal is to develop adaptive soundscapes that dynamically respond to the participant's movements and interactions with virtual boids and obstacles. As the participant explores the virtual space, the behavior of the boids and the characteristics of the soundscape evolve in real-time, creating a responsive audio experience. Moving through different areas of the environment triggers changes in the density and distribution of boids, leading to corresponding shifts in the sonic landscape. 

![Screenshot of In-Game Scenes](https://raw.githubusercontent.com/Farcyy/UnityMaxOSC_SoundInstallation/main/IMAGES/InGameTakes_NEW.png)

- **Boids**: The game leverages the concept of boids—algorithmic models of bird flocking behavior—to create dynamic soundscapes. Boids simulate the natural movement of flocks, creating an evolving and unpredictable auditory experience. By opening the GUI in the Granulator section you can see the BOIDS position in the room section.
- **Granular Synthesizer**: A granular synthesizer implemented from the Spat Library breaks down sound samples into small "grains" and then recombines them in various ways to create new textures and timbres. This method allows for intricate manipulation of sound, producing unique and complex auditory experiences.
- **Gregorian Choirs**: The game incorporates samples from Gregorian chants, which are a form of plainchant used in the medieval church, characterized by monophonic, unaccompanied vocal music. These chants provide a simple musical ground while resulting in complex and still harmonic granular synthesis outputs.

### Integration of Granular Synthesis
The movements of participants control the behavior of virtual boids, which in turn affect the soundscape generated through granular synthesis. For this, the mono sample is split into 16 grains. Each grain acts as an independent sound source. Control parameters such as pitch, start, and duration as well as the refresh rate are controlled by the player's movement. No further adjustements are therefore in Max/MSP required. 

- **Grain Duration**: Adjusting the duration of grains based on the flock's proximity to obstacles influences the perception of tension and calmness. Shorter grains signify tension or danger, while longer grains create a sense of calmness and safety.
  - Far from obstacles: Longer grain durations for smooth and serene sounds.
  - Near obstacles: Shorter grain durations for chaotic and turbulent sounds.
  
- **Grain Pitch**: Modulating the pitch of grains adds tonal variation and emotional depth.
  - Approaching obstacles: Higher pitches signal danger or urgency.
  - Peaceful moments: Lower pitches create a relaxed atmosphere.
  
- **Grain Density**: Manipulating grain density affects the complexity of the sonic texture.
  - Encountering obstacles: Higher grain densities create dense and intricate soundscapes.
  - Tranquil moments: Lower densities provide contrast and relief.

Every grain of sound is represented by a sound particle moving in space. The integration between the characteristics of the particle swarm and the parameters of the granular synthesizer creates a responsive musical instrument, similar to the concept explored by Antonino Modica in the project ["Critical Mass"](https://github.com/antoninomodica/critical-mass). 

## HOA Processing in MAX: 

HOA Encoding: The 16 mono tracks are encoded with spat5.spat into HOA format based on their respective 3D positions received by the BOIDS. 2nd order HOA seems to work for a balance between spatial accuracy and computational efficiency. Spat5 supports up to the 7th order. The order can be changed by giving the right number of channels ((N+1)^2, where N is the HOA order) to the spat5.spat object. Be aware that the number of channels need to be changed for the following objects as well.

Reverb processing: is applied within the HOA domain using Spat5.oper, which includes an FDN (Feedback Delay Network) reverb. The reverb parameters such as reverberation time, early reflections, late reflections, and diffusion can be adjusted in the GUI of the Granulator section. The main reason for implementing FDN Reverb is real-time adjustment of parameters such as reverberation time, early reflections, late reflections, and diffusion. This flexibility is essential for dynamic environments and will be controlled via OSC by raytracing data computed in unity in the future. Furthermore FDN reverb is computationally efficient and suitable for real-time applications, especially when processing multiple sound sources in an HOA context. An optional convolution reverb is available in the Max patch outside of presentation mode. It requires modification to use. This reverb takes a first-order impulse response (B-format) and convolves it with the W, X, Y, Z channels of your signal. Using the Convolution reverb can provide highly realistic reverberation by using recorded impulse responses from real acoustic spaces as long as the control for the FDN reverb is not updated. Be aware that Convolution reverb can be computationally intensive, making it less suitable for real-time applications with multiple sources. 

Good to know:
- The listeners orientation received by unity is applied in the HOA-domain by the spat5.hoa.rotate object.
- The energy density is displayed in the HOA Scope within the HOA encoder section.
- HOA Decoding: Decode the HOA signals to prepare them for binaural output with spat5.hoa.binaraul object. Apply Head-Related Transfer Functions (HRTFs) using spat5 to process the virtual loudspeaker signals for headphone output, ensuring accurate spatial audio representation. If nothing applied built-in data is loaded.

## Installation
### Adding Unity Project Folder via Unity Hub
1. Download and unzip the Unity project folder from the provided link.
2. Open Unity Hub and click on the "Add" button.
3. Navigate to the unzipped project folder and select it.
4. Ensure you are using Unity version 2022.3.24f for compatibility.

### Setting Up Max/MSP
1. Open the file `Unity_OSCPatcher.maxpat`.
2. Drag and Drop a Sample into Max/MSP. For the best possible outcome, use an omnidirectional anechoic sample. This ensures no pre-existing directional information. Any other sample will be processed in mono.
3. After dropping the sample, click "Start Granulator" in Granulator section to begin the granular synthesis process.
4. Move in Unity to change Granulator parameters.
5. If BOIDs take too long to find back to the player, a quick reset of BOIDs can be initiated in the HOA encoder section.
6. Have Fun.
   
### Connecting the Headtracker
1. Obtain Supperware's Bridgehead from [Supperware's website](https://supperware.io/bridgehead).
2. Install the software and set the sending frequency to 100Hz.

### Starting the Game
1. Start the Max Patcher
2. Calibrate the Headtracker (by double clicking the Head Symbol in BridgeHead)
3. Starting the Unity Scene

Enjoy your audio journey with **Resonance Rift**!
