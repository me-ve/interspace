namespace interspace
{
	public static class GraphCalculations
	{
		// the class for storing functions operating on graphs
		public static int[,] NeighbourMatrix;
		public static int DirectPathDistance(int vertex1, int vertex2){
			return NeighbourMatrix[vertex1, vertex2];
		}
		//TODO ADD SHORTEST PATH ALGORITHM
	}
}
