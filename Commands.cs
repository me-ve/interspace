using System;
using System.IO;
using System.Collections.Generic;
using ErrorCode = interspace.Errors.ErrorCode;

namespace interspace{
    public static class Commands{
		// all the commands are specified here
		public delegate Errors.ErrorCode CommandDelegate();
		public static Dictionary<string, CommandDelegate> commands = new Dictionary<string, CommandDelegate>(){
			{"create",		CreateMatrixFromStdin},
			{"draw",		DrawMatrix},
			{"editcol",		EditMatrixCol},
			{"editedge",	EditMatrixEdge},
			{"editrow",		EditMatrixRow},
			{"help",		Help},
			{"history",		DisplayHistory},
			{"load",		LoadMatrixFromFile},
			{"exit",		CloseProgram}
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
			catch (Exception e){
				ApplicationData.LogError(e);
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
				int n = Convert.ToInt32(ApplicationData.inputFile.ReadLine());	//get vertices' count
				if (n <= 0) return ErrorCode.MATRIX_SIZE_ERROR;
				var matrix = new int[n,n];
				for(int i=0; i<n; i++){
					string[] line = ApplicationData.inputFile.ReadLine().Split(' ');
					if(line.Length > n) return ErrorCode.MATRIX_FORMAT_ERROR;
					// if the user had provided less than n numbers we assume the rest are zeros
					for(int j=0; j<line.Length; j++){
						matrix[i,j] = Convert.ToInt32(line[j]);
					}
				}
				GraphCalculations.NeighbourMatrix = matrix;
				//close the file after processing
				ApplicationData.inputFile.Close();
			}
			catch (FileNotFoundException e){
				ApplicationData.LogError(e);
				return ErrorCode.FILE_LOADING_ERROR;
			}
			catch (IOException e){
				ApplicationData.LogError(e);
				return ErrorCode.FILE_GENERAL_ERROR;
			}
			catch (FormatException e){
				ApplicationData.LogError(e);
				return ErrorCode.MATRIX_FORMAT_ERROR;
			}
			catch (Exception e){
				ApplicationData.LogError(e);
				return ErrorCode.UNSPECIFIED_ERROR;
			}
			return ErrorCode.NO_ERROR;
		}
		public static ErrorCode CreateMatrixFromStdin(){
			try{
				Console.Write("Enter the number of vertices: ");
				uint n = Convert.ToUInt32(Console.ReadLine());
				var matrix = new int[n,n];	// make temporary matrix to save elements from input
				for(int i=0; i<n; i++){
					Console.Write($"Enter elements of row {i} separated by spaces: ");
					string[] line = Console.ReadLine().Split(' ');
					if (line.Length > n) throw new FormatException();
					// if the user had provided less than n numbers we assume that the rest in the row is zero
					for(int j=0; j<line.Length; j++){
						matrix[i,j] = Convert.ToInt32(line[j]);
					}
				}
				// if there is still no error we can safely save all the data to our main matrix
				GraphCalculations.NeighbourMatrix = matrix;
			}
			catch(FormatException e){	// for example if the user will input letter instead of digit
				ApplicationData.LogError(e);
				return ErrorCode.MATRIX_FORMAT_ERROR;
			}
			catch(Exception e){
				ApplicationData.LogError(e);
                return ErrorCode.UNSPECIFIED_ERROR;
			}
			return ErrorCode.NO_ERROR;
		}
		public static ErrorCode EditMatrixCol(){
			try{
				if(GraphCalculations.NeighbourMatrix == null) throw new NullReferenceException();
				Console.WriteLine("Enter edited column: ");
				uint col = Convert.ToUInt32(Console.ReadLine());
				if(col>=GraphCalculations.Vertices) throw new IndexOutOfRangeException();
				int[] temp = new int[GraphCalculations.Vertices];
				Console.WriteLine("Enter new values: ");
				string[] line = Console.ReadLine().Split(' ');
				if(line.Length > GraphCalculations.Vertices) throw new FormatException();
				for(int i=0; i<line.Length; i++){
					temp[i] = Convert.ToInt32(line[i]);
				}
				for(int i=0; i<line.Length; i++){
					GraphCalculations.NeighbourMatrix[i, col] = temp[i];
				}
			}
			catch(NullReferenceException e){
                ApplicationData.LogError(e);
                return ErrorCode.MATRIX_NULL_ERROR;
            }
			catch(IndexOutOfRangeException e){
				ApplicationData.LogError(e);
				return ErrorCode.MATRIX_INDEX_ERROR;
			}
			catch(FormatException e){
				ApplicationData.LogError(e);
				return ErrorCode.MATRIX_FORMAT_ERROR;
			}
			catch(Exception e){
				ApplicationData.LogError(e);
				return ErrorCode.UNSPECIFIED_ERROR;
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
				GraphCalculations.NeighbourMatrix[row, col] = Convert.ToInt32(Console.ReadLine());
			}
			catch(NullReferenceException e){
                ApplicationData.LogError(e);
                return ErrorCode.MATRIX_NULL_ERROR;
            }
			catch(IndexOutOfRangeException e){
				ApplicationData.LogError(e);
				return ErrorCode.MATRIX_INDEX_ERROR;
			}
			catch(FormatException e){
				ApplicationData.LogError(e);
				return ErrorCode.MATRIX_FORMAT_ERROR;
			}
			catch(Exception e){
				ApplicationData.LogError(e);
				return ErrorCode.UNSPECIFIED_ERROR;
			}
			return ErrorCode.NO_ERROR;
		}
		public static ErrorCode EditMatrixRow(){
			try{
				if(GraphCalculations.NeighbourMatrix == null) throw new NullReferenceException();
				Console.WriteLine("Enter edited row: ");
				uint row = Convert.ToUInt32(Console.ReadLine());
				if(row>=GraphCalculations.Vertices) throw new IndexOutOfRangeException();
				int[] temp = new int[GraphCalculations.Vertices];
				Console.WriteLine("Enter new values: ");
				string[] line = Console.ReadLine().Split(' ');
				if(line.Length > GraphCalculations.Vertices) throw new FormatException();
				for(int j=0; j<line.Length; j++){
					temp[j] = Convert.ToInt32(line[j]);
				}
				for(int j=0; j<line.Length; j++){
					GraphCalculations.NeighbourMatrix[row, j] = temp[j];
				}
			}
			catch(NullReferenceException e){
                ApplicationData.LogError(e);
                return ErrorCode.MATRIX_NULL_ERROR;
            }
			catch(IndexOutOfRangeException e){
				ApplicationData.LogError(e);
				return ErrorCode.MATRIX_INDEX_ERROR;
			}
			catch(FormatException e){
				ApplicationData.LogError(e);
				return ErrorCode.MATRIX_FORMAT_ERROR;
			}
			catch(Exception e){
				ApplicationData.LogError(e);
				return ErrorCode.UNSPECIFIED_ERROR;
			}
			return ErrorCode.NO_ERROR;
		}
		public static ErrorCode DrawMatrix(){
            try{
                string matrixStr = "";
                for(uint i=0; i<GraphCalculations.Vertices; i++){
                    for(uint j=0; j<GraphCalculations.Vertices; j++){
                        matrixStr += $"{GraphCalculations.Edge(i, j)}\t";
                    }
                    matrixStr += "\n";
                }
                Console.WriteLine(matrixStr);
            }
			catch(IndexOutOfRangeException e){
				ApplicationData.LogError(e);
                return ErrorCode.MATRIX_INDEX_ERROR;
			}
            catch(NullReferenceException e){
                ApplicationData.LogError(e);
                return ErrorCode.MATRIX_NULL_ERROR;
            }
            catch (Exception e){
				ApplicationData.LogError(e);
                return ErrorCode.UNSPECIFIED_ERROR;
            }
            return ErrorCode.NO_ERROR;
        }
		
		public static ErrorCode Help(){
			Console.WriteLine("Commands:");
			string str = "Commands:\n";
			for(int i=0; i<commandsCount; i++){
				str += $"{commandsNamesList[i]}";
				if(i<commandsCount-1) str += "\t";
			}
			Console.WriteLine(str);
			return ErrorCode.NO_ERROR;
		}
    }
}