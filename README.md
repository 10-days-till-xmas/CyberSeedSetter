# Cybergrind Seed Setter
![Version](https://img.shields.io/badge/version-1.0.0-navy) <br>
A mod that allows players to get and set seeds for Cybergrind runs
<br>
<details>
    <summary><b>NOTE TO USERS (PLEASE READ)</b></summary>
    <b>Between major versions (e.g., from v1.0.0 to v2.0.0), the random output of a seed may change,
    making seeds from older versions no longer output the expected result in newer versions. 
    Minor and patch updates should maintain seed compatibility</b>
</details>

## Features
- Commands to get and set the seed (Note: the seed is set to a config file with the set command)
- A cheat to force the next Cybergrind run to use the set seed
## Usage
```
cybergrind-seed get
cybergrind-seed set <seed>
```
## Roadmap
- [x] Correctly overrides the seed resulting in reproducible runs
- [ ] Start from a specific wave
- [ ] Calculate the seed for desired spawns