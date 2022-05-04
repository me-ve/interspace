using System;
using System.IO;
using System.Collections.Generic;
using ErrorCode = interspace.Errors.ErrorCode;

namespace interspace{
    public static class Commands{
		// all the commands are specified here
		public delegate Errors.ErrorCode CommandDelegate();
		public static Dictionary<string, CommandDelegate> commands = new Dictionary<string, CommandDelegate>(){
			{"create",			CreateMatrixFromStdin},
			{"draw",			DrawNeighbourMatrix},
			{"edit",			EditMatrixEdge},
			{"editcol",			EditMatrixCol},
			{"editrow",			EditMatrixRow},
			{"export",			ExportMatrices},
			{"help",			Help},
			{"history",			DisplayHistory},
			{"load",			LoadMatrixFromFile},
			{"shortest",		DrawShortestPathsMatrix},
			{"exit",			CloseProgram},
		};

		public static int commandsCount = commands.Count;
		public static List<string> commandsNamesList = new List<string>(commands.Keys);
        public static ErrorCode CloseProgram(){
			ApplicationData.running = false;
			return ErrorCode.NO_ERROR;
		}
        public static ErrorCode DisplayHistory(){
			try{
				Console.WriteLine(ApplicationData.history);
			}
			catch (Exception ex){
				ApplicationData.LogError(ex);
				return ErrorCode.UNSPECIFIED_ERROR;
			}
			return ErrorCode.NO_ERROR;
		}
		public static ErrorCode LoadMatrixFromFile(){
			Console.Write("Type the name of matrix file: ");
			string filename = Console.ReadLine();
			try{
				ApplicationData.inputFile = new StreamReader(filename);
				//process file
				uint n = Convert.ToUInt32(ApplicationData.inputFile.ReadLine()); //get vertices' count
				var matrix = new double[n,n];
				for(int i=0; i<n; i++){
					string[] line = ApplicationData.inputFile.ReadLine().Split(' ');
					if(line.Length > n) throw new FormatException();
					// if the user had provided less than n numbers we assume the rest are zeros
					for(int j=0; j<line.Length; j++){
						matrix[i,j] = Convert.ToDouble(line[j]);
					}
				}
				GraphCalculations.NeighbourMatrix = matrix;
				//close the file after processing
				ApplicationData.inputFile.Close();
			}
			catch (Exception ex){
				ApplicationData.LogError(ex);
				switch(ex){
					case FileNotFoundException:
						return ErrorCode.FILE_LOADING_ERROR;
					case IOException:
						return ErrorCode.FILE_GENERAL_ERROR;
					case FormatException:
						return ErrorCode.MATRIX_FORMAT_ERROR;
					default:
						return ErrorCode.UNSPECIFIED_ERROR;
				}
			}
			return ErrorCode.NO_ERROR;
		}
		public static ErrorCode CreateMatrixFromStdin(){
			try{
				Console.Write("Enter the number of vertices: ");
				uint n = Convert.ToUInt32(Console.ReadLine());
				var matrix = new double[n,n];	// make temporary matrix to save elements from input
				for(int i=0; i<n; i++){
					Console.Write($"Enter elements of row {i} separated by spaces: ");
					string[] line = Console.ReadLine().Split(' ');
					if (line.Length > n) throw new FormatException();
					// if the user had provided less than n numbers we assume that the rest in the row is zero
					for(int j=0; j<line.Length; j++){
						matrix[i,j] = Convert.ToDouble(line[j]);
					}
				}
				// if there is still no error we can safely save all the data to our main matrix
				GraphCalculations.NeighbourMatrix = matrix;
			}
			catch(Exception ex){
				ApplicationData.LogError(ex);
				switch(ex){
					case FormatException:
						return ErrorCode.MATRIX_FORMAT_ERROR;
					default:
						return ErrorCode.NO_ERROR;
				}
			}
			return ErrorCode.NO_ERROR;
		}
		public static ErrorCode EditMatrixCol(){
			try{
				if(GraphCalculations.NeighbourMatrix == null) throw new NullReferenceException();
				Console.WriteLine("Enter edited column: ");
				uint col = Convert.ToUInt32(Console.ReadLine());
				if(col>=GraphCalculations.Vertices) throw new IndexOutOfRangeException();
				var temp = new double[GraphCalculations.Vertices];
				Console.WriteLine("Enter new values: ");
				string[] line = Console.ReadLine().Split(' ');
				int n = line.Length;
				if(n > GraphCalculations.Vertices) throw new FormatException();
				//attention - this changes only n first elements
				for(int i=0; i<n; i++){
					temp[i] = Convert.ToDouble(line[i]);
				}
				for(int i=0; i<n; i++){
					GraphCalculations.NeighbourMatrix[i, col] = temp[i];
				}
			}
			catch(Exception ex){
				ApplicationData.LogError(ex);
				switch(ex){
					case NullReferenceException:
						return ErrorCode.MATRIX_NULL_ERROR;
					case IndexOutOfRangeException:
						return ErrorCode.MATRIX_INDEX_ERROR;
					case FormatException:
						return ErrorCode.MATRIX_FORMAT_ERROR;
					default:
						return ErrorCode.UNSPECIFIED_ERROR;
				}
			}
			return ErrorCode.NO_ERROR;
		}
		public static ErrorCode EditMatrixEdge(){
			try{
				if(GraphCalculations.NeighbourMatrix == null) throw new NullReferenceException();
				Console.WriteLine("Enter edited row: ");
				uint row = Convert.ToUInt32(Console.ReadLine());
				if(row>=GraphCalculations.Vertices) throw new IndexOutOfRangeException();
				uint col = Convert.ToUInt32(Console.ReadLine());
				Console.WriteLine("Enter edited column: ");
				if(row>=GraphCalculations.Vertices) throw new IndexOutOfRangeException();
				Console.WriteLine("Enter new value: ");
				GraphCalculations.NeighbourMatrix[row, col] = Convert.ToDouble(Console.ReadLine());
			}
			catch(Exception ex){
				ApplicationData.LogError(ex);
				switch(ex){
					case NullReferenceException:
						return ErrorCode.MATRIX_NULL_ERROR;
					case IndexOutOfRangeException:
						return ErrorCode.MATRIX_INDEX_ERROR;
					case FormatException:
						return ErrorCode.MATRIX_FORMAT_ERROR;
					default:
						return ErrorCode.UNSPECIFIED_ERROR;
				}
			}
			return ErrorCode.NO_ERROR;
		}
		public static ErrorCode EditMatrixRow(){
			try{
				if(GraphCalculations.NeighbourMatrix == null) throw new NullReferenceException();
				Console.WriteLine("Enter edited row: ");
				uint row = Convert.ToUInt32(Console.ReadLine());
				if(row>=GraphCalculations.Vertices) throw new IndexOutOfRangeException();
				var temp = new double[GraphCalculations.Vertices];
				Console.WriteLine("Enter new values: ");
				string[] line = Console.ReadLine().Split(' ');
				int n = line.Length;
				if(n > GraphCalculations.Vertices) throw new FormatException();
				//attention - this changes only n first elements
				for(int j=0; j<n; j++){
					temp[j] = Convert.ToDouble(line[j]);
				}
				for(int j=0; j<n; j++){
					GraphCalculations.NeighbourMatrix[row, j] = temp[j];
				}
			}
			catch(Exception ex){
				ApplicationData.LogError(ex);
				switch(ex){
					case NullReferenceException:
						return ErrorCode.MATRIX_NULL_ERROR;
					case IndexOutOfRangeException:
						return ErrorCode.MATRIX_INDEX_ERROR;
					case FormatException:
						return ErrorCode.MATRIX_FORMAT_ERROR;
					default:
						return ErrorCode.UNSPECIFIED_ERROR;
				}
			}
			return ErrorCode.NO_ERROR;
		}
		public static ErrorCode DrawNeighbourMatrix(){
            try{
                UserInterface.DrawMatrix(GraphCalculations.NeighbourMatrix);
            }
			catch(Exception ex){
				ApplicationData.LogError(ex);
				switch(ex){
					case NullReferenceException:
						return ErrorCode.MATRIX_NULL_ERROR;
					case IndexOutOfRangeException:
						return ErrorCode.MATRIX_INDEX_ERROR;
					default:
						return ErrorCode.UNSPECIFIED_ERROR;
				}
			}
            return ErrorCode.NO_ERROR;
        }
		public static ErrorCode DrawShortestPathsMatrix(){
			try{
				//TODO if the matrix is not changed it shouldn't be calculated again
				GraphCalculations.CalculateShortestPaths();
				UserInterface.DrawMatrix(GraphCalculations.ShortestPathsMatrix);
			}
			catch(Exception ex){
				ApplicationData.LogError(ex);
				switch(ex){
					case NullReferenceException:
						return ErrorCode.MATRIX_NULL_ERROR;
					case IndexOutOfRangeException:
						return ErrorCode.MATRIX_INDEX_ERROR;
					default:
						return ErrorCode.UNSPECIFIED_ERROR;
				}
			}
			return ErrorCode.NO_ERROR;
		}
		public static ErrorCode ExportMatrices(){
			try{
				Console.WriteLine("Enter the name of export file: ");
				string name = Console.ReadLine();
				var exportFile = new StreamWriter(name);
				uint n = GraphCalculations.Vertices;
				exportFile.WriteLine("Neighbour Matrix:");
				for(uint i=0; i<n; i++){
					for(uint j=0; j<n; j++){
						exportFile.Write($"{GraphCalculations.NeighbourMatrix[i,j]},");
					}
					exportFile.Write("\n");
				}
				GraphCalculations.CalculateShortestPaths();
				exportFile.WriteLine("Shortest Paths Matrix:");
				for(uint i=0; i<n; i++){
					for(uint j=0; j<n; j++){
						exportFile.Write($"{GraphCalculations.ShortestPathsMatrix[i,j]},");
					}
					exportFile.Write("\n");
				}
				exportFile.Close();
			}
			catch(Exception ex){
				ApplicationData.LogError(ex);
				return ErrorCode.UNSPECIFIED_ERROR;
			}
			return ErrorCode.NO_ERROR;
		}
		public static ErrorCode Help(){
			try{
				Console.WriteLine(UserInterface.HelpDialog);
			}
			catch(Exception ex){
				//TODO secure for case when help.txt file is not present
				ApplicationData.LogError(ex);
				return ErrorCode.UNSPECIFIED_ERROR;
			}
			return ErrorCode.NO_ERROR;
		}
    }
}