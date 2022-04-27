using System;
using System.Linq;

namespace interspace
{
	static class UserInterface{
		// there are stored the commands for interacting with the user
		static ErrorCode DoAction(string[] command){
			string[] args = command.Where((item, index) => index != 0).ToArray();	//get other arguments
			//TODO process those arguments too
			switch(command[0]){
				case "create":
				return Commands.CreateMatrixFromStdin();
				case "draw":
				return Commands.DrawMatrix();
				case "help":
				return Commands.Help();
				case "history":
				return Commands.DisplayHistory();
				case "load":
				return Commands.LoadMatrixFromFile();
				case "exit":
				return Commands.CloseProgram();
			}
			return ErrorCode.UNKNOWN_COMMAND_ERROR;
		}
		static string[] GetCommand(){
			string lineBeginning = $"{ApplicationData.linesExecuted}> ";
			Console.Write(lineBeginning);
			string command = Console.ReadLine();
			ApplicationData.history += $"{ApplicationData.linesExecuted}\t{command}\n";
			return command.Split();
		}
		public static void GetAndParseCommand(){
			ErrorCode errorCode = DoAction(GetCommand());
			if(errorCode != ErrorCode.NO_ERROR){
				Console.ForegroundColor = ConsoleColor.Red;
				switch(errorCode){
					case ErrorCode.FILE_LOADING_ERROR:
						Console.WriteLine("[!] File loading error");
						break;
					case ErrorCode.MATRIX_NULL_ERROR:
						Console.WriteLine("[!] The matrix is not initialized");
						break;
					case ErrorCode.MATRIX_SIZE_ERROR:
						Console.WriteLine("[!] The matrix size must be positive.");
						break;
					case ErrorCode.MATRIX_FORMAT_ERROR:
						Console.WriteLine("[!] The matrix in input file has wrong format");
						break;
					case ErrorCode.MATRIX_INDEX_ERROR:
						Console.WriteLine("[!] The index was outside of the bounds of matrix");
						break;
					case ErrorCode.UNKNOWN_COMMAND_ERROR:
						Console.WriteLine("[!] Unknown command");
						break;
					default:
						Console.WriteLine("[!] Unknown error");
						break;
				}
				Console.ResetColor();
			}else{
				ApplicationData.linesExecuted++;
			}
		}
	}
}
