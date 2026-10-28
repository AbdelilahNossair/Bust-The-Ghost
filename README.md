# Bust the Ghost

A Unity/C# probabilistic grid game. A ghost is hidden on a board, sensors return noisy color clues, and the player uses updated location probabilities to choose where to bust.

## Gameplay

1. Start the sample scene in Unity.
2. Click tiles in sensing mode to obtain colored clues.
3. Use the probability display to compare possible ghost locations.
4. Toggle **Bust**, then choose a tile when ready to make a guess.
5. Watch the remaining-bust counter; the interface starts with two attempts.
6. Use the probability toggle to show or hide tile probability text.

Sensor colors are stochastic clues rather than exact distance labels. The ghost script samples x from 0–10 and y from 0–7, corresponding to an 11 × 8 coordinate range.

## Probability model

`Proba.cs` defines color probabilities for distance buckets 0 through 5. Larger distances are clamped to bucket 5. At distance 0, red has probability 0.90; in the farthest bucket green has probability 0.76. Intermediate buckets favor orange or yellow.

`GridManager` updates posterior tile probabilities after a sensed location. `Tile` handles pointer input and bust/sense interactions. These routines are useful for studying Bayesian-style evidence updates, but the implementation should be checked for normalization and edge cases before using it as a numerical reference.

## Open the project

```bash
git clone https://github.com/AbdelilahNossair/Bust-The-Ghost.git
```

The reviewed tree contains `Assets/` and a sample scene but does not include the usual `ProjectSettings/ProjectVersion.txt` or `Packages/manifest.json`. An exact original Unity version therefore cannot be inferred.

If Unity Hub cannot open the checkout as a complete project:

1. Create a local Unity project with Unity UI support.
2. Import the repository's `Assets/` folder while preserving `.meta` files.
3. Open `Assets/Scenes/SampleScene.unity`.
4. Resolve missing UI/TextMesh Pro dependencies and Inspector references.
5. Enter Play mode and inspect the Console.

Treat editor/package selection as reconstruction work; no specific Unity release is claimed as verified.

## Script map

| File | Responsibility |
| --- | --- |
| `Assets/Ghost.cs` | Random hidden position |
| `Assets/GridManager.cs` | Grid creation, counters, distance helper, posterior updates |
| `Assets/Tile.cs` | Tile construction, pointer interaction, visual feedback |
| `Assets/Proba.cs` | Distance-conditioned sensor sampling |
| `Assets/Sensor.cs` | Probability/color result object |
| `Assets/Bust.cs` | Bust-mode toggle and UI listener |
| `Assets/Peep.cs` | Probability-text visibility |

## Scene dependencies

Scripts look up objects named `Bust`, `Toggle`, and `RemainingBusts`, and probability objects tagged `Proba`. Missing names, tags, or component references can cause null-reference errors. Preserve the sample scene wiring when experimenting.

## Build and manual checks

First confirm Play mode works, then add the sample scene to the editor's build configuration and select a supported desktop target. Verify sensing, probability visibility, bust mode, a successful guess, and exhausted attempts. Inspect the Console for failed object lookups and confirm probabilities remain finite.

The ghost's position is logged for debugging, so the Console reveals the answer. Random placement is not seeded, and sensor sampling uses rounded integer percentages. No automated tests, packaged executable, verified Unity version, or standalone license file is included. Third-party fonts and TextMesh Pro assets retain their own notices.
