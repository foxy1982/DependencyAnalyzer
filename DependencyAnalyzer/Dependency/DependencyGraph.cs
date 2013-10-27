using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitsuneSoft.DependencyAnalyzer
{
        public class DependencyGraph : BidirectionalGraph<DependencyVertex, DependencyEdge>
        {
            public DependencyGraph()
            {
            }

            public DependencyGraph(bool allowParallelEdges)
                : base(allowParallelEdges)
            {
            }

            public DependencyGraph(bool allowParallelEdges, int vertexCapacity)
                : base(allowParallelEdges, vertexCapacity)
            {
            }
    }
}
