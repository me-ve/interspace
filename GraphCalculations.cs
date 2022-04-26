namespace interspace
{
	public static class GraphCalculations
	{
		public static int[,] NeighbourMatrix = {
			// example matrix that could be used
			{1, 2, 3, 4, 4},
			{2, 2, 3, 1, 2},
			{3, 3, -1, 4, 1}, 
			{4, 1, 1, 5, 3},
			{4, 2, 1, 3, 3}
		};
		public static int DirectPathDistance(int vertex1, int vertex2){
			return NeighbourMatrix[vertex1, vertex2];
		}
		//TODO ADD SHORTEST PATH ALGORITHM
	}
}
