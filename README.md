# SimpleMiningSimulator
A simple and not very efficient mining simulator. It generates a "block template" with a variable length to simulate real blocks with different numbers of transactions in them. It also uses the hash of the previous block in that template in order to link all blocks in a chain, just like in Bitcoin or other cryptocurrencies.

The hash of block #0 is hardcoded (000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f) <-- This is Bitcoin's genesis block hash, used by Satoshi Nakamoto. Every next block which you find with this program will be cryptographically linked to the genesis block. The Mining difficulty is very low and won't make your pc overheat or use a lot of power. Unlike Bitcoin where the difficulty target is a number which the hash must be lower than, this simulator uses a simpler approach: The hash must begin with X amount of zeros for it to be valid. It uses only the cpu for mining and stores the progress on the disk. If you see permission denied errors - run the app as an administrator.

# Building from source
To build this program from source you will need `msbuild`, which you can download here:https://www.microsoft.com/en-us/download/details.aspx?id=48159

Once you have it simply run `BuildFromSource.bat` and that's it! You will find the .exe file in the obj/Release folder.
