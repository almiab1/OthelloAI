# Othello AI

A complete implementation of the classic Othello (Reversi) board game built with Unity, featuring an intelligent AI opponent powered by the Minimax algorithm with alpha-beta pruning.

![Unity](https://img.shields.io/badge/Unity-2018.3.6f1-blue)
![C#](https://img.shields.io/badge/C%23-Script-green)
![License](https://img.shields.io/badge/License-GPL--3.0-yellow)

## 📋 Table of Contents

- [About](#about)
- [Features](#features)
- [Getting Started](#getting-started)
- [Game Rules](#game-rules)
- [Configuration](#configuration)
- [AI Implementation](#ai-implementation)
- [Project Structure](#project-structure)
- [License](#license)

## 🎮 About

Othello (also known as Reversi) is a classic strategy board game for two players. This project implements the complete game with an AI opponent that uses the Minimax algorithm with alpha-beta pruning to provide a challenging gameplay experience.

The game is built entirely in Unity and includes support for three game modes:
- Human vs Human
- Human vs AI
- AI vs AI (watch the AI play against itself!)

## ✨ Features

- **Intelligent AI Opponent**: Implements Minimax algorithm with alpha-beta pruning at depth 4
- **Multiple Game Modes**: Play against a friend, challenge the AI, or watch two AIs compete
- **Visual Feedback**: 
  - Valid moves are highlighted in real-time
  - Last move indicator
  - Piece count display for both players
- **Full Game Rules Implementation**:
  - Automatic piece flipping in all 8 directions
  - Pass detection when no valid moves available
  - Game end detection (board full, no pieces, or double pass)
  - Winner determination
- **User-Friendly Interface**:
  - Clean 3D board representation
  - Easy-to-use mouse controls
  - Play again functionality without restarting

## 🚀 Getting Started

### Prerequisites

- Unity 2018.3.6f1 or compatible version
- Basic understanding of Unity interface

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/almiab1/OthelloAI.git
   ```

2. Open the project in Unity:
   - Launch Unity Hub
   - Click "Add" and select the cloned project folder
   - Open the project with Unity 2018.3.6f1

3. Load the game scene:
   - Navigate to `Assets/Scenes/`
   - Open `SampleScene.unity`

4. Run the game:
   - Press the Play button in Unity Editor
   - Or build the project for your target platform (File → Build Settings)

## 📖 Game Rules

Othello is played on an 8×8 board with pieces that are black on one side and white on the other:

1. The game starts with 4 pieces in the center (2 black, 2 white)
2. Black always moves first
3. Players must place a piece to flank one or more opponent pieces
4. All flanked opponent pieces are flipped to the current player's color
5. Flanking can occur in any of the 8 directions (horizontal, vertical, diagonal)
6. If a player has no valid moves, they must pass
7. The game ends when:
   - The board is full
   - One player has no pieces remaining
   - Both players pass consecutively
8. The player with the most pieces wins

## ⚙️ Configuration

You can configure game modes by editing the `Constants.cs` file:

```csharp
public const string Player1 = "HUMAN";  // Options: "HUMAN" or "AI"
public const string Player2 = "AI";     // Options: "HUMAN" or "AI"
public const int Start = 1;             // 1 for Black, -1 for White
```

### Game Modes:

- **Human vs AI**: `Player1 = "HUMAN"`, `Player2 = "AI"`
- **Human vs Human**: `Player1 = "HUMAN"`, `Player2 = "HUMAN"`
- **AI vs AI**: `Player1 = "AI"`, `Player2 = "AI"`

### Other Constants:

```csharp
public const int Black = 1;
public const int White = -1;
public const int PassTime = 2;  // Seconds to wait when a player passes
```

## 🤖 AI Implementation

The AI uses a **Minimax algorithm with alpha-beta pruning** to make intelligent decisions:

### Algorithm Details:

- **Search Depth**: 4 levels deep
- **Pruning**: Alpha-beta pruning for optimization
- **Evaluation Function**: Based on the number of pieces that would be flipped by each move
- **Node Structure**: Game tree with MIN and MAX nodes alternating by level

### How It Works:

1. **Tree Generation**: The AI generates a game tree of possible moves up to depth 4
2. **Evaluation**: Each leaf node is evaluated based on potential piece captures
3. **Minimax**: 
   - MAX nodes try to maximize the score (AI's moves)
   - MIN nodes try to minimize the score (opponent's moves)
4. **Alpha-Beta Pruning**: Cuts off branches that won't affect the final decision
5. **Move Selection**: The AI selects the move with the best evaluated outcome

### Key Classes:

- **Player.cs**: Contains the AI logic and Minimax implementation
- **Node class**: Represents nodes in the game tree with board state and utility values
- **BoardManager.cs**: Handles board logic, move validation, and piece flipping

## 📁 Project Structure

```
OthelloAI/
├── Assets/
│   ├── Materials/          # Materials for pieces and board
│   │   ├── Black.mat       # Black piece material
│   │   ├── White.mat       # White piece material
│   │   └── Tile_*.mat      # Board tile materials
│   ├── Prefabs/            # Game object prefabs
│   │   ├── Piece.prefab    # Othello piece prefab
│   │   └── LastMovement Variant.prefab
│   ├── Scenes/             # Unity scenes
│   │   └── SampleScene.unity
│   └── Scripts/            # C# game scripts
│       ├── Constants.cs        # Game constants and configuration
│       ├── Controller.cs       # Main game controller
│       ├── Player.cs           # AI logic (Minimax)
│       ├── BoardManager.cs     # Board state management
│       ├── Tile.cs             # Tile data structure
│       └── TileBehaviour.cs    # Tile interaction handling
├── ProjectSettings/        # Unity project settings
├── LICENSE                 # GPL-3.0 License
└── README.md              # This file
```

### Script Responsibilities:

- **Constants.cs**: Defines game constants (board size, player types, colors)
- **Controller.cs**: Manages game flow, UI updates, and player turns
- **Player.cs**: Implements AI decision-making with Minimax algorithm
- **BoardManager.cs**: Core game logic (valid moves, piece flipping, board state)
- **Tile.cs**: Data structure representing a board tile
- **TileBehaviour.cs**: Handles mouse input on tiles

## 📄 License

This project is licensed under the GNU General Public License v3.0 - see the [LICENSE](LICENSE) file for details.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! Feel free to check the issues page.

## 📧 Contact

Project Link: [https://github.com/almiab1/OthelloAI](https://github.com/almiab1/OthelloAI)

---

Made with ❤️ and Unity
