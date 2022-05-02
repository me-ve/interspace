namespace interspace
{
	public static class GraphCalculations
	{
		// the class for storing functions operating on graphs
		public static int[,] NeighbourMatrix;
		public static int[,] ShortestPathsMatrix;
		public static uint Vertices => (uint)NeighbourMatrix.GetLength(0);
		public static bool hasNegativeEdges(){
			for(uint i=0; i<Vertices; i++){
				for(uint j=0; j<Vertices; j++){
					if(Edge(i, j) < 0) return true;
				}
			}
			return false;
		}
		public static int Edge(uint vertex1, uint vertex2){
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
		public static int[,] SlowAllPairsShortestPaths(){
			//it works for all but is Θ(n^4) ;-(
			int n = NeighbourMatrix.GetLength(0);
			var D = (int[,])NeighbourMatrix.Clone();
			for(int m = 1; m < n; m++){
				D = ExtendShortestPaths(D, NeighbourMatrix);
			}
			return D;
		}
		//TODO find negative cycles
		public static int[,] FastAllPairsShortestPaths(){
			//attention - this works for matrices with non-negative cycles
			//I think this one is Θ(n^3 log n) which is slightly better
			int n = NeighbourMatrix.GetLength(0);
			var D = (int[,])NeighbourMatrix.Clone();
			int m = 0;
			while (n-1 > m){
				D = ExtendShortestPaths(D, D);
				m <<= 1;
			}
			return D;
		}
		public static void CalculateShortestPaths(){
			if(hasNegativeEdges()){	//this will be changed to use slow method only for negative cycles
				ShortestPathsMatrix = SlowAllPairsShortestPaths();
			}
			else{
				ShortestPathsMatrix = FastAllPairsShortestPaths();
			}
		}
	}
}
