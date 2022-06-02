using System;
using System.IO;
using System.Linq;
using ErrorCode = interspace.Errors.ErrorCode;
using CommandDelegate = interspace.Commands.CommandDelegate;
namespace interspace
{
	static class UserInterface{
		// there are stored the commands for interacting with the user
		public static ConsoleColor ErrorColor => ConsoleColor.Red;
		public static ConsoleColor SuccessColor => ConsoleColor.Green;
		public static ConsoleColor IndexColor => ConsoleColor.Cyan;
		public static string HelpDialog = File.ReadAllText("help.txt");
		static ErrorCode DoAction(string[] commandParams){
			string[] args = commandParams.Where((item, index) => index != 0).ToArray();	//get other arguments
			CommandDelegate command;
			if(!Commands.commands.TryGetValue(commandParams[0], out command))
				return ErrorCode.UNKNOWN_COMMAND_ERROR;
			else
				return command.Invoke(args);
		}
		static string[] GetCommand(){
			string lineBeginning = $"{ApplicationData.linesExecuted}> ";
			Console.Write(lineBeginning);
			string command = Console.ReadLine();
			ApplicationData.history.Add($"{command}");
			return command.Split();
		}
		public static void WriteColorLine(string message, ConsoleColor color){
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
		}
		public static void WriteColor(string message, ConsoleColor color){
			Console.ForegroundColor = color;
			Console.Write(message);
			Console.ResetColor();
		}
		public static void DrawMatrix(double[,] matrix){
			string colsStr = "";
			int m = matrix.GetLength(0);
			int n = matrix.GetLength(1);
			for(int i=0; i<n; i++){
				colsStr += $"\t{i}";
			}
			WriteColorLine(colsStr, IndexColor);
			string[] rows = matrix.RowsToString();
			   for(uint i=0; i<n; i++){
				WriteColor($"{i}\t", IndexColor);
				Console.WriteLine(rows[i]);
			}
		}
		public static void OutputErrorToUser(ErrorCode errorCode){
			//assume we won't output anything if there is no error
			if(errorCode == ErrorCode.NO_ERROR) return;
			string communicate;
			if(!Errors.errorCommunicates.TryGetValue(errorCode, out communicate)){
				communicate = "Unknown error";
			}
			WriteColorLine($"[!] {communicate}", ErrorColor);
		}
		public static void GetAndParseCommand(){
			string[] command = GetCommand();
			ErrorCode errorCode = DoAction(command);
			if(errorCode != ErrorCode.NO_ERROR)
				OutputErrorToUser(errorCode);
			else{
				switch(command[0]){
					case "history":
					case "help":
					case "exit":
						break;
					default:
						WriteColorLine("OK", SuccessColor);
					break;
				}
				ApplicationData.linesExecuted++;
			}
		}
	}
}
