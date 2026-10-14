# 🫧 Dishwasher vs Dish

> The dishes are angry, the kitchen is a battlefield, and you have a bubble gun.

**Dishwasher vs Dish** is a fast top-down **roguelike bubble shooter** built in **Unity 6**. Dirty dishes pour in from every direction, in four waves that get meaner as you go. Shoot them with bubbles, collect the **soap points** they drop, and every few points the game freezes and offers you a choice of **three upgrades**, each on its own meme-worthy card. Survive all four waves and wipe out the last dish to win.


<img width="320" height="180" alt="image" src="https://github.com/user-attachments/assets/2f2e3bdf-a4ca-4b95-bec4-cde09b98ce4c" />
<img width="320" height="180" alt="image" src="https://github.com/user-attachments/assets/0f1b395a-0254-4074-bacc-13f7b8bd619a" />
<img width="320" height="180" alt="image" src="https://github.com/user-attachments/assets/2ebf8091-a284-4e32-bf51-efd3ee8d805d" />


---

## ✨ Features

- **Four enemy waves** with a spawn-credit system that adds tougher dishes over time
- **Seven dish types**: chasers, a fast dasher, a shooter, and a puddle-maker
- **Roguelike upgrade picks**: 9 different upgrades offered in random sets of 3 whenever you hit a soap point milestone
- **Mouse-aimed bubble gun** with a cooldown, scalable bubble size, and optional piercing
- **Rotating soap shield**, stun bubbles, a coin magnet, and more
- **Hit feedback**: invincibility frames, sprite blinking, bubble pop animations
- Pixel art made in **Aseprite**, a health bar, an enemy counter, and a soap point counter
- Win and lose screens with automatic restart

---

## 🎮 Controls

| Action | Keyboard & Mouse | Gamepad |
| --- | --- | --- |
| Move | `W` `A` `S` `D` / Arrow keys | Left stick |
| Aim | Mouse cursor | Right stick |
| Shoot | Left click / `Enter` | West button |
| Pick an upgrade | Click a card | n/a |

> Bindings live in `Assets/InputSystem_Actions.inputactions`.

---

## 🧽 How to Play

1. Press **START** on the menu.
2. Move around and **aim with the mouse**. Your character always faces the cursor.
3. Shoot bubbles at the dishes. Each dish has its own health, speed, and tricks.
4. Pick up the **soap points** that dishes sometimes drop.
5. At **1, 3, 7, 12, 18, and 24** soap points, the game pauses and shows **three upgrade cards**. You have **2.5 seconds** to click one before they disappear.
6. Clear all four waves to win. If your health hits zero, the level restarts.

---

## 🍽️ The Dishes

| Dish | Speed | Health | Drop chance | Behaviour |
| --- | --- | --- | --- | --- |
| **enemy1** | 7 | 5 | 50% | Basic chaser |
| **enemy2** | 8 | 4 | 40% | Slightly faster chaser |
| **enemy3** | 10 | 6.6 | 40% | Fast and sturdy |
| **enemy4** | 13 | 3 | 40% | Very fast, fragile |
| **enemy5** | 8 | 5 | 50% | **Dashes** at you from a distance of 2 to 8 (7-second cooldown) |
| **enemy7** | 7 | 7 | 60% | **Shoots** a bullet at you every 3.5 seconds |
| **enemy8** | 9 | 10 | 70% | Drops a **puddle** attack every 6 seconds |

Being touched, shot, or splashed costs **1 health**. After any hit you are invincible for **0.8 seconds** and your sprite blinks.

---

## 🌊 Waves

Each wave has a **credit budget**. Every spawned dish spends credits, and when the budget runs out, the next wave begins. Dishes spawn at random places in the arena, never closer than 4 units to you.

| Wave | Start delay | Spawn every | Credits | Dishes in the pool |
| --- | --- | --- | --- | --- |
| 1 | 2.5 s | 1 s | 10 | enemy1, enemy2 |
| 2 | 8 s | 0.7 s | 30 | + enemy3, enemy4 |
| 3 | 12 s | 0.5 s | 50 | + enemy5, enemy7 |
| 4 | 15 s | 0.2 s | 100 | + enemy8 |

Credit cost per dish: enemy1 = 1, enemy2 = 2, enemy3 = 2, enemy4 = 3, enemy5 = 3, enemy7 = 4, enemy8 = 4.

When wave 4 has finished spawning and the **last dish is gone**, you win.

---

## 🃏 Upgrades

| Upgrade | Effect |
| --- | --- |
| **Fire rate** | Cooldown between shots is halved |
| **Movement speed** | Move 40% faster |
| **Damage increase** | +0.5 bubble damage |
| **Health increase** | +3 max health and a full heal |
| **Bubble radius** | Bubbles are 40% bigger |
| **Bubble piercing** | Bubbles pass through enemies |
| **Rotating shields** | A soap shield orbits you, damaging and knocking back dishes |
| **Coins follow** | Soap points fly toward you |
| **Stun bullets** | Hit dishes move at half speed for 1 to 2 seconds (bubbles turn orange) |

Upgrades that can only be taken once (piercing, shield, coin magnet, stun) stop appearing after you have them.

---

## 🧠 How It Works

### Player (`PlayerController`)
Movement is applied to a `Rigidbody2D` in `FixedUpdate`. Every frame the player rotates to face the cursor using `atan2`. Shooting instantiates a bullet at the muzzle, gives it a velocity along the aim angle, scales it by the current bubble size, and destroys it after 2 seconds. A coroutine enforces the shoot cooldown. Health, points, invincibility frames, blinking, and the win and lose coroutines also live here.

### Aiming (`followmouse`)
An invisible target follows the mouse in world space. The player looks at it.

### Enemies (`enemy1script`)
One script drives all dish types, branching on the object's name for special behaviours. Enemies chase the player with `MovePosition`, can be stunned, knocked back by the shield, or pushed away by puddles. When health reaches 0, the enemy is destroyed, the spawner count goes down, and a drop roll is made.

### Spawner (`enemyspawner`)
Four coroutines, one per wave. Each waits its start delay, then spawns random dishes from the wave's pool while spending credits. Finishing the budget starts the next wave. The last coroutine flips a `checkdead` flag, and as soon as the enemy counter hits zero, the player wins.

### Power-ups (`powerupmanager`)
Watches the soap point counter. At each milestone it sets `Time.timeScale = 0`, picks three distinct random cards (skipping upgrades you already have), shows them, and waits 2.5 real seconds. Each card's button calls its upgrade method, which also resumes the game.

### Other scripts
- `bullet_behaviour`: plays the pop animation and stops the bullet on hit, unless piercing is on
- `soap_behaviour`: keeps the rotating shield on the player and spins it
- `scenechanger`: the START button

---

## 🚀 Getting Started

### Requirements

- **Unity 6000.3.9f1** (Unity 6.3). Using the exact version avoids upgrade prompts.
- Universal Render Pipeline 17.3
- Packages (pinned in `Packages/manifest.json`): Input System 1.18, Cinemachine 3.1.6, 2D Animation, 2D Aseprite Importer, 2D Tilemap Extras, TextMeshPro

### Run it

```bash
git clone https://github.com/Mehran579/DishWasherVsDish.git
```

1. Open **Unity Hub** → **Add** → select the cloned folder.
2. Let Unity import the project.
3. Open `Assets/Scenes/Menu.unity` and press **Play**.

Build Settings scene order:

| Index | Scene |
| --- | --- |
| 0 | `Menu` |
| 1 | `SampleScene` (the game) |

---

## 📁 Project Structure

```
Assets/
├── Scripts/                 # Player, enemies, spawner, power-ups
├── Scenes/                  # Menu and the game scene
├── Prefab/                  # Enemies (enemy1 to enemy8), bullets, puddle, collectable points
├── Sprites/                 # Pixel art (.ase), power-up card images, UI art
├── animations/              # Bullet burst and enemy animations
├── Materials/               # Materials
├── _Recovery/               # Backup scenes
└── InputSystem_Actions.inputactions
```

### Script cheat sheet

| Script | What it does |
| --- | --- |
| `PlayerController` | Movement, aiming, shooting, health, points, win and lose |
| `enemy1script` | All dish behaviour (chase, dash, shoot, puddle, drops) |
| `enemyspawner` | Wave and credit system, win check |
| `powerupmanager` | Upgrade cards and effects |
| `bullet_behaviour` | Bubble damage and pop |
| `soap_behaviour` | Orbiting shield |
| `followmouse` | Mouse aim target |
| `scenechanger` | Menu START button |

---

## 🙏 Credits

- **Pixel art and animation:** made in Aseprite
- **Bubble burst effect:** Pngtree
- **Power-up card images:** assorted stock photos and meme images (including a photo by Chamal Prasanna on Unsplash)
- Everything else by **Mehran**

---

## 📌 Status

Playable from the menu through four waves to the win screen. Ideas for new dishes, more upgrades, and a high score list are welcome. Open an issue or a pull request!
