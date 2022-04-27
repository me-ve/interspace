# Interspace

## Summary

The console-based, crossplatform program for calculating shortest paths for user given neighbour matrix of the graph.

## Dependencies

To launch the program requires the .NET Core version 5.0.

## Commands
- create - creates matrix through the standard input
- draw - draws the matrix
- exit - exits program
- history - displays all commands input by user
- load - loads matrix from the file
- shortestpath - returns path with shortest length for specified two vertices (planned)
- help - displays commands

## Format of matrix in the file
Matrix is stored in text file that is created as specified:
```
size
x x ... x
x x ... x
. . ... .
x x ... x
```
where
```
size - 32-bit integer that stores information about how many vertices are present
x - information about direct route length between vertices
```
The user can skip zeros in the right part of matrix during inputting.
