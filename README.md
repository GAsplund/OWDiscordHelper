# OWDiscordHelper
A bot for Discord that sets users nicks as Overwatch levels


Info:
The directory of the program must have a users.txt file and a config.txt file or else it won't work (WIP to automatically make this)
Each line must be spaced out by semicolons on both the config file and users file.

Config file:
First line is your bot token, 
Second line is your guild/server ID, 
Third line is the debug setting.

Example of config file:
```
(Very secret bot token);
176689618194792448;
false;
```

Users file:
Each line is one user, and looks like this:
```
<BATTLETAG NAME>-<BATTLETAG ID>,<DISCORD USER ID>,<DISCORD NAME/NICK>;
```

Example of users file:
```
L33tHax0r-1337,233892072606662658,Good Username;
Bert-65536,233892072606662658,Unnamed;
```
