using System;
using System.Linq;
using ErrorCode = interspace.Errors.ErrorCode;
using CommandDelegate = interspace.Commands.CommandDelegate;
namespace interspace
{
	static class UserInterface{
		// there are stored the commands for interacting with the user	
		static ErrorCode DoAction(string[] commandParams){
			string[] args = commandParams.Where((item, index) => index != 0).ToArray();	//get other arguments
			//TODO process those arguments too
			CommandDelegate command;
			if(!Commands.commands.TryGetValue(commandParams[0], out command))
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
		public static void WriteColor(string message, ConsoleColor color){
			Console.ForegroundColor = color;
			Console.Write(message);
			Console.ResetColor();
		}
		public static void OutputErrorToUser(ErrorCode errorCode){
			//assume we won't output anything if there is no error
			if(errorCode == ErrorCode.NO_ERROR) return;
			string communicate;
			if(!Errors.errorCommunicates.TryGetValue(errorCode, out communicate)){
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
