using System;
using System.IO;
using System.Collections.Generic;
using ErrorCode = interspace.Errors.ErrorCode;

namespace interspace{
    public static class Commands{
		// all the commands are specified here
		public delegate Errors.ErrorCode CommandDelegate();
		public static Dictionary<string, CommandDelegate> commands = new Dictionary<string, CommandDelegate>(){
			{"create",	CreateMatrixFromStdin},
			{"draw",	DrawMatrix},
			{"help",	Help},
			{"history",	DisplayHistory},
			{"load",	LoadMatrixFromFile},
			{"exit",	CloseProgram}
		};
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
				int n = Convert.ToInt32(Console.ReadLine());
				if (n <= 0) return ErrorCode.MATRIX_SIZE_ERROR; // we cannot have notpositive size matrix
				var matrix = new int[n,n];	// make temporary matrix to save elements from input
				for(int i=0; i<n; i++){
					Console.Write($"Enter elements of row {i} separated by spaces: ");
					string[] line = Console.ReadLine().Split(' ');
					if (line.Length > n) return ErrorCode.MATRIX_FORMAT_ERROR;
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

		public static ErrorCode DrawMatrix(){
            try{
                string matrixStr = "";
                for(int i=0; i<GraphCalculations.NeighbourMatrix.GetLength(0); i++){
                    for(int j=0; j<GraphCalculations.NeighbourMatrix.GetLength(1); j++){
                        matrixStr += $"{GraphCalculations.NeighbourMatrix[i, j]}\t";
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
			Console.WriteLine("create\tdraw\texit\thistory\tload\thelp");
			return ErrorCode.NO_ERROR;
		}
    }
}