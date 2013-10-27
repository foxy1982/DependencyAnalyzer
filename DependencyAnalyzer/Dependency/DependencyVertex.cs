using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitsuneSoft.DependencyAnalyzer
{
        /// <summary>
        ///     A simple identifiable vertex.
        /// </summary>
        [DebuggerDisplay("{ID}")]
        public class DependencyVertex
        {
            public DependencyVertex(string id)
            {
                ID = id;
            }

            public string ID { get; private set; }

            public override string ToString()
            {
                return ID;
            }
    }
}
