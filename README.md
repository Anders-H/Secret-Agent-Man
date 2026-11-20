# Secret Agent Man

A computerized video game in which you play a secret agent tasked with eliminating foreign spies.

Website: [https://secretagentman.80tal.se/](https://secretagentman.80tal.se/)

Engine: [RetroGame](https://github.com/Anders-H/RetroGame)

## Development and tests

Build the current RetroGame library in Release configuration before building this solution; the game references its Release DLL in the sibling RetroGame repository. Its standard font is embedded in that DLL.

Run the deterministic highscore tests with:

`dotnet test SecretAgentMan/sam_online_highscore_toolkit_tests/sam_online_highscore_toolkit_tests.csproj`

Run gameplay regressions on Windows with:

`dotnet test SecretAgentMan/SecretAgentMan.Tests/SecretAgentMan.Tests.csproj --configuration Release`

These tests use simulated HTTP responses and do not contact the live highscore server. Gameplay tests construct rooms without loading graphics assets.

Scene activation resumes the existing player and ammunition. Starting a new level resets room position, mission progress, level ticks and ammunition while retaining score, lives and accumulated civilian faults. Highscore requests run asynchronously; saves complete before refreshing the global list.
