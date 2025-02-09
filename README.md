# HandRoyale

A modern take on the classic Rock, Paper, Scissors game with additional elements - Lizard and Spock. This Unity-based game features quick-paced gameplay, real-time animations, and a scoring system.

## Game Description

HandRoyale is an extended version of Rock, Paper, Scissors that includes two additional moves: Lizard and Spock. The game follows these winning conditions:
- Rock crushes Scissors and Lizard
- Paper covers Rock and disproves Spock
- Scissors cuts Paper and decapitates Lizard
- Lizard poisons Spock and eats Paper
- Spock smashes Scissors and vaporizes Rock

## Features

- Single-player gameplay against AI
- Real-time move animations
- Score tracking system
- High score persistence
- Timer-based rounds
- Clean and intuitive UI
- Sound effects and visual feedback

## Technical Details

### Architecture
- Singleton pattern for managers
- Event-driven communication
- Modular UI system
- Independent timer management

### Technologies Used
- Unity 2022.3.0
- DOTween for animations


## Project Structure

```
Assets/
├── Scripts/
│   ├── Managers/
│   │   ├── GameManager.cs
│   │   ├── TimerManager.cs
│   │   └── UIManager.cs
│   ├── Events/
│   │   └── GameEvents.cs
│   └── UI/
│       └── UIComponents/
├── Scenes/
├── Sprites/
└── Animations/
```

## Setup Instructions

1. Clone the repository
2. Open with Unity 2022.3.0 or later
3. Install required packages:
   - DOTween
   - TextMeshPro
4. Open the HandRoyale scene in `Assets/Scenes/`
5. Press Play to test
