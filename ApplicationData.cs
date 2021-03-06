using System;
using System.IO;
using System.Collections.Generic;

namespace interspace
{
	public static class ApplicationData
	{
		// data used by program to operate
		public static bool running = true;										// if this is false the program will be closed
		public static StreamReader inputFile;									// for matrix file
		public static StreamWriter logFile = File.AppendText("logs.log");		// for logging errors
		public static uint linesExecuted = 0;									// how many commends were input
		public static List<string> history = new List<string>();										// to display previous commands
		public static void LogError(Exception e){
			ApplicationData.logFile.WriteLine($"{e.Message}");
			ApplicationData.logFile.WriteLine($"{e.StackTrace}");
			ApplicationData.logFile.Flush();
		}
	}
}
