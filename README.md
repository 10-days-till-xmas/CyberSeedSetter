# Cybergrind Seed Setter
![Version](https://img.shields.io/badge/version-1.1.0-navy) <br>
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
cybergrind-seed set-str <string>
cybergrind-seed copy
cybergrind-seed paste
```
Note that with the `set` command, the seed must be a 32-bit signed integer (between -2,147,483,648 and 2,147,483,647).
The `set-str` command allows you to set the seed using any string;
the mod will try to parse the string and if not, then hash it to produce a valid seed. 
The `paste` command has the same behavior as `set-str`, but it uses the current clipboard contents as the string.
## Roadmap
- [x] Correctly overrides the seed resulting in reproducible runs
- [ ] Start from a specific wave
- [ ] Calculate the seed for desired spawns