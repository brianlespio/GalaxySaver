# GalaxySaver Game Prompt

## Game Overview
GalaxySaver is a classic arcade-style space shooter inspired by the iconic Galaga and Galaxian games. Players control a spaceship at the bottom of the screen, defending Earth from waves of alien invaders that move in formation patterns and occasionally break ranks to dive-bomb the player.

## Core Gameplay

### Player Controls
- **Left/Right Arrow Keys**: Move the player's ship horizontally across the bottom of the screen
- **Space Bar**: Fire bullets upward to destroy enemy ships
- **Enter Key**: Restart game after Game Over
- **Escape Key**: Exit the game

### Game Mechanics

#### Enemy Formations
- Enemies appear in organized grid formations at the top of the screen
- Current implementation: 5 columns × 3 rows of enemies (15 total)
- Future enhancement: Different enemy types arranged in more complex patterns

#### Enemy Behavior
- **Basic Movement**: Enemies move downward slowly
- **Formation Movement**: In future versions, enemies should move in synchronized patterns (left-right sweeping motion)
- **Dive Bombing**: Individual enemies should occasionally break from formation to dive toward the player in attack runs
- **Return to Formation**: After a dive bombing run, surviving enemies should return to their position in the formation

#### Shooting Mechanics
- Player can fire bullets upward to destroy enemy ships
- Enemies should occasionally fire bullets downward toward the player
- Collision detection between bullets and ships determines hits

### Scoring System
- **Regular Enemies**: 100 points per enemy destroyed
- **Diving Enemies**: Should award bonus points (200-300) when hit during a diving attack
- **Formation Completion**: Bonus points for clearing entire formations
- **Level Completion**: Bonus points for completing a level

## Visual and Audio Elements

### Visual Design
- **Player Ship**: Currently represented by a blue rectangle (40×40 pixels)
- **Enemy Ships**: Currently represented by red rectangles (30×30 pixels)
- **Bullets**: Currently represented by yellow rectangles (5×15 pixels)
- **Background**: Black space background

### Future Visual Enhancements
- Replace placeholder rectangles with proper sprite graphics
- Add explosion animations when ships are destroyed
- Add starfield background with parallax scrolling effect
- Add visual effects for player movement and shooting

### Audio Elements
- **Sound Effects**:
  - Shooting sound
  - Explosion sound
  - Game over sound
- **Future Audio Enhancements**:
  - Background music
  - Enemy movement sounds
  - Power-up sounds
  - Level completion jingles

## Game Progression

### Difficulty Scaling
- Increase enemy movement speed in later levels
- Increase frequency of enemy dive bombing attacks
- Introduce new enemy types with different movement patterns
- Increase enemy bullet frequency

### Level Design
- **Level 1**: Basic enemy formation with slow movement
- **Level 2-5**: Gradually introduce more complex formations and behaviors
- **Level 6+**: Full implementation of all enemy types and attack patterns

### Boss Battles
- Introduce boss ships after completing certain levels
- Bosses should have multiple hit points and special attack patterns
- Defeating a boss awards significant bonus points

## Power-ups and Special Features

### Power-up Types
- **Double Shot**: Allow player to fire two bullets simultaneously
- **Speed Boost**: Temporarily increase player movement speed
- **Shield**: Temporary invulnerability
- **Smart Bomb**: Clear all enemies on screen

### Special Features
- **Capture Beam**: Like in Galaga, special enemies could capture the player's ship and later be rescued for dual-ship gameplay
- **Challenge Stages**: Special bonus rounds with unique enemy patterns
- **High Score Table**: Track and display top player scores

## Technical Implementation Notes

### Current Implementation
- Basic player movement (left/right)
- Basic enemy movement (downward)
- Basic shooting and collision detection
- Simple scoring system
- Game over detection

### Future Implementation Priorities
1. Implement proper enemy formation movement patterns
2. Add enemy dive bombing behavior
3. Add enemy shooting capability
4. Implement proper sprite graphics and animations
5. Add power-up system
6. Implement level progression
7. Add boss battles
8. Enhance audio-visual elements

## Game Feel and Polish
- Responsive controls with appropriate movement speed
- Satisfying feedback for shooting and destroying enemies
- Screen shake effects for explosions and impacts
- Particle effects for explosions and engine trails
- Dynamic difficulty adjustment based on player performance

---

This game prompt provides a comprehensive blueprint for developing GalaxySaver as a modern interpretation of the classic Galaga/Galaxian arcade games, while maintaining the core gameplay elements that made these games timeless classics.