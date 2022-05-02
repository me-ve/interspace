# Interspace

## Summary

The console-based, crossplatform program for calculating shortest paths for user given neighbour matrix of the graph.

## Dependencies

To launch the program requires the .NET Core version 5.0.

## Commands

### create
creates the neighbour matrix through the standard input
### draw
draws the neighbour matrix
### edit
edits specified edge length
### editcol
edits first values in neighbour matrix column
### editrow
edits first values in neighbour matrix row
### help
displays available commands
### history
displays all commands input by user
### load
loads the neighbour matrix from the text file
### shortest
returns matrix of shortest paths for every pair of vertices
### exit
closes the program

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
size - 32-bit unsigned integer that stores information about how many vertices are present
x - 32-bit signed integer edge length between vertices
```
The user can skip zeros in the right part of matrix during inputting.
