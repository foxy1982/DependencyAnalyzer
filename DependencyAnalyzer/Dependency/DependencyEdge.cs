using QuickGraph;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitsuneSoft.DependencyAnalyzer
{
        /// <summary>
        ///     A simple identifiable edge.
        /// </summary>
        [DebuggerDisplay("{Source.ID} -> {Target.ID}")]
        public class DependencyEdge : Edge<DependencyVertex>
        {
            public DependencyEdge(string id, DependencyVertex source, DependencyVertex target)
                : base(source, target)
            {
                ID = id;
            }

            public string ID { get; private set; }
        }
}
