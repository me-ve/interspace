namespace interspace
{
	public static class GraphCalculations
	{
		// the class for storing functions operating on graphs
		public static int[,] NeighbourMatrix;
		public static int DirectPathDistance(int vertex1, int vertex2){
			return NeighbourMatrix[vertex1, vertex2];
		}
		// TODO implement those functions to the console
		public static int[,] ExtendShortestPaths(int[,] D, int[,] W){
			int n = D.GetLength(0);
			var dPrim = new int[n,n];
			for(int i=0; i<n; i++){
				for(int j=0; j<n; j++){
					dPrim[i,j] = int.MaxValue;
					for(int k=0; k<n; k++){
						// dPrim[i,j] = min(dPrim[i,j], dPrim[i,k] + W[k,j])
						dPrim[i,j] = dPrim[i,j] < dPrim[i,k] + W[k,j] ?
							dPrim[i,j] :
							dPrim[i,k] + W[k,j];
					}
				}
			}
			return dPrim;
		}
		public static int[,] SlowAllPairsShortestPaths(int[,] W){
			int n = W.GetLength(0);
			var D = (int[,])W.Clone();
			for(int m=1; m < n; m++){
				D = ExtendShortestPaths(D, W);
			}
			return D;
		}
		public static int[,] FastAllPairsShortestPaths(int[,] W){
			//attention - this works for matrices with nonnegative paths
			int n = W.GetLength(0);
			var D = (int[,])W.Clone();
			int m = 0;
			while (n-1 > m){
				D = ExtendShortestPaths(D, D);
				m *= 2;
			}
			return D;
		}
		//TODO ADD SHORTEST PATH ALGORITHM
	}
}
