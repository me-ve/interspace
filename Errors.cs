using System.Collections.Generic;

namespace interspace{
	public class Errors{
		public enum ErrorCode{
			// this class contains codes of various events that could ruin user interaction with program
			NO_ERROR,
			FILE_GENERAL_ERROR,
			FILE_LOADING_ERROR,
			MATRIX_NULL_ERROR,
			MATRIX_SIZE_ERROR,
			MATRIX_FORMAT_ERROR,
			MATRIX_INDEX_ERROR,
			UNKNOWN_COMMAND_ERROR,
			UNSPECIFIED_ERROR
		}
		public static Dictionary<ErrorCode, string> errorCommunicates = new Dictionary<ErrorCode, string>(){
			//sent to console during error
			{ErrorCode.NO_ERROR, "OK"},
			{ErrorCode.FILE_GENERAL_ERROR,		"There was a problem with loading file"},
			{ErrorCode.FILE_LOADING_ERROR,		"The file does not exist"},
			{ErrorCode.MATRIX_NULL_ERROR,		"The matrix is not initialized"},
			{ErrorCode.MATRIX_SIZE_ERROR,		"The matrix size must be positive"},
			{ErrorCode.MATRIX_FORMAT_ERROR,		"The matrix provided in input has wrong format"},
			{ErrorCode.MATRIX_INDEX_ERROR,		"The index was outside of the bounds of matrix"},
			{ErrorCode.UNKNOWN_COMMAND_ERROR,	"Unknown command"},
		};
	}
}