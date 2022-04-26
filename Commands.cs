using System;
using System.IO;

namespace interspace{
    public static class Commands{
		// all the commands are specified here
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
		//TODO load matrix from file
		public static ErrorCode LoadInputFile(){
			Console.WriteLine("Type the name of matrix file: ");
			string filename = Console.ReadLine();
			try{
				ApplicationData.inputFile = new StreamReader(filename);
			}
			catch (FileNotFoundException e){
				ApplicationData.LogError(e);
				return ErrorCode.FILE_LOADING_ERROR;
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
    }
}