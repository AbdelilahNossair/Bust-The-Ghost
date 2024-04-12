# Bust the Ghost Game

## Overview

"Bust the Ghost" is an engaging Unity game where players strategically bust ghosts hidden within a grid. Players must use hints to deduce the ghost's location and successfully bust it. The game offers a mix of strategy, logic, and suspense, utilizing Unity's UI system and custom scripting to create an interactive gaming experience.

## Features

- **Dynamic Grid System:** A grid is dynamically generated where the ghost can hide.
- **Interactive Tiles:** Each tile on the grid can be clicked, triggering game logic to provide hints or bust the ghost.
- **Probability Visualization:** Probabilities of the ghost's location are displayed on each tile, updating as the game progresses.
- **Score Tracking:** The game tracks the player's score based on successful and unsuccessful busts.
- **Bust Management:** Players have a limited number of busts to catch the ghost, requiring careful strategic planning.

## Installation

To set up the "Bust the Ghost" game, follow these steps:

1. **Clone the Repository:**
   ```bash
   git clone https://your-repository-url.com
   ```

2. **Open Unity Hub:**
   Navigate to 'Projects' tab and click on 'Add'. Select the cloned project folder.

3. **Open the Project:**
   Double-click the project in Unity Hub to open it in Unity Editor.

4. **Build and Run:**
   Go to 'File' > 'Build Settings'. Click on 'Build and Run' to compile and execute the game.

## How to Play

- **Start the Game:** Launch the game. The grid will be populated with tiles.
- **Interact with Tiles:** Click on tiles to reveal hints about the ghost’s position.
- **Use Busts Wisely:** Use the bust button when you think a ghost is in a particular tile. Be mindful of the limited number of busts.
- **Track the Score:** Each successful bust increases your score, while wrong busts decrease it.
- **Winning and Losing:** The game ends when you either catch the ghost or run out of busts.

## Code Overview

- **Bust Script:** Handles the logic for the bust action and toggling the bust state.
- **Ghost Script:** Randomly assigns the ghost's position at the start.
- **GridManager Script:** Manages the grid's dimensions and interactions, and initializes the game components.
- **Tile Script:** Represents each tile on the grid and handles interactions, including displaying probabilities and reacting to busts.

## Dependencies

- Unity Editor (Version 2020.3 or later recommended)
- Standard Unity Modules (e.g., UI, 2D Physics)

## Contributing

Interested in contributing? Great! You can help by:

- Reporting bugs
- Suggesting new features
- Improving the codebase or documentation
