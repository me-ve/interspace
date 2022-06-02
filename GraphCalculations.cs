namespace interspace
{
	public static class GraphCalculations
	{
		// the class for storing functions operating on graphs
		public static double[,] NeighbourMatrix;
		public static double[,] ShortestPathsMatrix;
		public static string[] RowsToString(this double[,] matrix){
			int m = matrix.GetLength(0);
			int n = matrix.GetLength(1);
			string[] rows = new string[m];
			for(int i=0; i<m; i++){
				rows[i] = "";
				for(int j=0; j<n; j++){
					rows[i] += $"{matrix[i,j]}\t";
				}
			}
			return rows;
		}
		public static uint Vertices => (uint)NeighbourMatrix.GetLength(0);
		public static bool hasNegativeEdges(){
			for(uint i=0; i<Vertices; i++){
				for(uint j=0; j<Vertices; j++){
					if(NeighbourMatrix.Edge(i, j) < 0) return true;
				}
			}
			return false;
		}
		public static double Edge(this double[,] matrix, uint vertex1, uint vertex2){
			return matrix[vertex1, vertex2];
		}
		public static double[,] ExtendShortestPaths(double[,] D, double[,] W){
			int n = D.GetLength(0);
			var dPrim = new double[n,n];
			for(int i=0; i<n; i++){
				for(int j=0; j<n; j++){
					dPrim[i,j] = double.PositiveInfinity;
					for(int k=0; k<n; k++){
						// dPrim[i,j] = min(dPrim[i,j], d[i,k] + W[k,j])
						dPrim[i,j] =
							dPrim[i,j] < D[i,k] + W[k,j] ?
							dPrim[i,j] :
							D[i,k] + W[k,j];
					}
				}
			}
			return dPrim;
		}
		public static double[,] SlowAllPairsShortestPaths(double[,] W){
			//it works for all but is Θ(n^4)
			int n = W.GetLength(0);
			var D = (double[,])W.Clone();
			for(int m = 1; m < n; m++){
				D = ExtendShortestPaths(D, W);
			}
			return D;
		}
		//TODO find negative cycles
		public static double[,] FastAllPairsShortestPaths(double[,] W){
			//attention - this works for matrices with non-negative cycles
			//I think this one is Θ(n^3 log n) which is slightly better
			int n = W.GetLength(0);
			var D = (double[,])W.Clone();
			int m = 1;
			while (n > m){
				D = ExtendShortestPaths(D, D);
				m <<= 1;
			}
			return D;
		}
		public static void CalculateShortestPaths(){
			var EdgeWeightsMatrix = new double[Vertices, Vertices];
			for(int i = 0; i<Vertices; i++){
				for(int j = 0; j<Vertices; j++){
					if(i != j && NeighbourMatrix[i, j] == 0){
						EdgeWeightsMatrix[i, j] = double.PositiveInfinity;
					}
					else{
						EdgeWeightsMatrix[i, j] = NeighbourMatrix[i, j];
					}
				}
			}
			if(hasNegativeEdges()){	// FIXME this will be changed to use slow method only for negative cycles
				ShortestPathsMatrix = SlowAllPairsShortestPaths(EdgeWeightsMatrix);
			}
			else{
				ShortestPathsMatrix = FastAllPairsShortestPaths(EdgeWeightsMatrix);
			}
		}
	}
}
