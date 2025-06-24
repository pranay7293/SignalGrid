# 🎯 Signal Grid – Pattern Logic Puzzle Game (Unity 2D)

Signal Grid is a 5x5 grid-based logic puzzle game where each tile toggles itself and other tiles based on hidden signal rules. Your mission: deduce the hidden rules and turn **all tiles OFF**.

This project was developed as part of a logic-based Unity interview task, with clean OOP architecture, state management, and no brute-force solution.

---

## 🧠 Game Concept

- 5x5 grid of **ON/OFF** tiles (lights)
- Each tile has a **hidden toggle rule**
- Clicking a tile toggles itself and other tiles according to that rule
- Your goal: **turn off all lights**
- Use deduction, logic, and visual feedback — not trial and error

---

## ▶️ How to Play

1. Start the game — a randomized 5x5 puzzle is generated
2. Click any tile to toggle it and related tiles
3. Observe which tiles change
4. Deduce each tile's hidden logic
5. Use your one-time Peek Tool wisely
6. Turn all tiles OFF to win

---
## ▶️ Try It Now

[![Play on WebGL](https://img.shields.io/badge/Play-WebGL-green?style=for-the-badge)](https://pranaygamedev.itch.io/signal-grid)

> *Runs in browser. No download needed.*

---

## 📦 Download APK

[![Download APK](https://img.shields.io/badge/Download-APK-blue?style=for-the-badge)](https://your-apk-download-link.com)

> *For Android devices. Allow installation from unknown sources.*

---

## 🎮 Toggle Rules Implemented

Each tile is randomly assigned one of the following:

| Rule Type        | Description                                  |
|------------------|----------------------------------------------|
| SelfOnly         | Toggles only itself                          |
| Diagonal         | Toggles 4 diagonals (↖︎ ↗︎ ↙︎ ↘︎)              |
| AdjacentMirrors  | Toggles 4 adjacent (↑ ↓ ← →)                 |
| RowMirrors       | Toggles only ← and →                         |
| ColumnMirrors    | Toggles only ↑ and ↓                         |
| KnightMoveA      | 1st random knight move pattern (chess-style) |
| KnightMoveB      | 2nd random knight move pattern               |

> Knight moves are randomly picked from 8 custom combinations each game session.

---

## 🎁 Bonus Features

- ✅ **Move Counter** — Tracks number of user moves
- ✅ **Timer** — Tracks gameplay duration
- ✅ **Toggled Tiles Feedback** — Shows how many tiles changed each move
- ✅ **One-Time Peek Tool** — Reveals toggle pattern for 1 tile only, shown in a separate 3x3 panel
- ✅ **Win Detection** — Triggers UI when all lights are off
- ✅ **Quit Button** — Cleanly exits game/editor
- ✅ **Peek Panel** — Always visible; lights up briefly when peek is used

---

## 🛠️ Project Setup

### Requirements:
- Unity 2021+ (2D URP or standard 2D)
- TextMeshPro package
- Assign all `UIManager` references in the Inspector

### Folder Structure:
Assets/ <br>
├── Audio/ <br>
├── Prefabs/ <br>
├── Scenes/ <br>
├── Scripts/ <br>
├── UI/

---
