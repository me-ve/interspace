# Interspace

## Summary

The console-based, crossplatform program for calculating shortest paths for user given neighbour matrix of the graph.

## Dependencies

To launch the program requires the .NET Core version 5.0.

## Commands

### create
creates the neighbour matrix through the standard input
- `-v/--vertices` - specify how many vertices will be set
### distance
print distance between two specified vertices
- `-f/--from` - vertex from which the distance will be printed
- `-t/--to` - vertex to which the distance will be printed
### draw
draws the neighbour matrix
### edit
edits specified edge length
- `-r/--row` - vertex from which the edge is set
- `-c/--col` - vertex to which the edge is set
- `-v/--value` - new value of specified edge
### editcol
edits first values in neighbour matrix column
- `-c/--col` - the column that will be edited
### editrow
edits first values in neighbour matrix row
- `-r/--row` - the row that will be edited
### export
exports neighbour matrix and shortest paths matrix in comma-separated format
- `-o/--out` - the file where those matrices will be saved
### help
displays available commands
### history
displays all commands input by user
### load
loads the neighbour matrix from the text file
- `-f/--file` - file from which the matrix will be loaded
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
x - edge length between vertices (in double float number)
```
The user can skip zeros in the right part of matrix during inputting.
