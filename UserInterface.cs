using System;
using System.Linq;
using System.Collections.Generic;
namespace interspace
{
	static class UserInterface{
		// there are stored the commands for interacting with the user
		delegate ErrorCode Command();
		static Dictionary<ErrorCode, string> errorCommunicates = new Dictionary<ErrorCode, string>(){
			//sent to console during error
			{ErrorCode.NO_ERROR, ""},
			{ErrorCode.FILE_GENERAL_ERROR,		"There was a problem with loading file"},
			{ErrorCode.FILE_LOADING_ERROR,		"The file does not exist"},
			{ErrorCode.MATRIX_NULL_ERROR,		"The matrix is not initialized"},
			{ErrorCode.MATRIX_SIZE_ERROR,		"The matrix size must be positive"},
			{ErrorCode.MATRIX_FORMAT_ERROR,		"The matrix provided in input has wrong format"},
			{ErrorCode.MATRIX_INDEX_ERROR,		"The index was outside of the bounds of matrix"},
			{ErrorCode.UNKNOWN_COMMAND_ERROR,	"Unknown command"},
		};
		static Dictionary<string, Command> commands = new Dictionary<string, Command>(){
			{"create",	Commands.CreateMatrixFromStdin},
			{"draw",	Commands.DrawMatrix},
			{"help",	Commands.Help},
			{"history",	Commands.DisplayHistory},
			{"load",	Commands.LoadMatrixFromFile},
			{"exit",	Commands.CloseProgram}
		};
		static ErrorCode DoAction(string[] commandParams){
			string[] args = commandParams.Where((item, index) => index != 0).ToArray();	//get other arguments
			//TODO process those arguments too
			Command command;
			if(!commands.TryGetValue(commandParams[0], out command))
				return ErrorCode.UNKNOWN_COMMAND_ERROR;
			else
				return command.Invoke();
		}
		static string[] GetCommand(){
			string lineBeginning = $"{ApplicationData.linesExecuted}> ";
			Console.Write(lineBeginning);
			string command = Console.ReadLine();
			ApplicationData.history += $"{ApplicationData.linesExecuted}\t{command}\n";
			return command.Split();
		}
		public static void WriteColorLine(string message, ConsoleColor color){
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
		}
		public static void OutputErrorToUser(ErrorCode errorCode){
			//assume we won't output anything if there is no error
			if(errorCode == ErrorCode.NO_ERROR) return;
			string communicate;
			if(!errorCommunicates.TryGetValue(errorCode, out communicate)){
				communicate = "Unknown error";
			}
			WriteColorLine($"[!] {communicate}", ConsoleColor.Red);
		}
		public static void GetAndParseCommand(){
			ErrorCode errorCode = DoAction(GetCommand());
			if(errorCode != ErrorCode.NO_ERROR)
				OutputErrorToUser(errorCode);
			else ApplicationData.linesExecuted++;
		}
	}
}
