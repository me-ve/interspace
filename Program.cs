using System;

namespace interspace
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Interspace");
			Console.WriteLine("----------------------------");
			while(ApplicationData.running){
				UserInterface.GetAndParseCommand();
			}
			ApplicationData.logFile.Close();
		}
	}
}
