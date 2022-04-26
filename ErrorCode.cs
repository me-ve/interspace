using System;

namespace interspace{
	public enum ErrorCode{
		// this class contains codes of various events that could ruin user interaction with program
		NO_ERROR,
		FILE_LOADING_ERROR,
		MATRIX_NULL_ERROR,
		MATRIX_SIZE_ERROR,
		MATRIX_FORMAT_ERROR,
		MATRIX_INDEX_ERROR,
		UNKNOWN_COMMAND_ERROR,
		UNSPECIFIED_ERROR
	}
}