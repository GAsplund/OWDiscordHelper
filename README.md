# OWDiscordHelper
A bot for Discord that sets users nicks as Overwatch levels


Info:
At the start of the program it will check for users.txt, config.txt and info.txt. If any of these are missing it will automatically create them empty which you must fill with information specified below.
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
