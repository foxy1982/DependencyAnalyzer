using EnvDTE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSLangProj;

namespace KitsuneSoft.DependencyAnalyzer
{
    public class DependencyViewModel
    {
        private readonly List<String> _layoutAlgorithmTypes = new List<string>();
        private DependencyGraph _graph;
        private string _layoutAlgorithmType;

        public DependencyViewModel(Solution solution)
        {
            InitializeLayoutAlgorithms();

            CreateDiagram(solution);
        }

        public List<String> LayoutAlgorithmTypes
        {
            get { return _layoutAlgorithmTypes; }
        }

        public string LayoutAlgorithmType
        {
            get { return _layoutAlgorithmType; }
            set
            {
                _layoutAlgorithmType = value;
                NotifyPropertyChanged("LayoutAlgorithmType");
            }
        }

        public DependencyGraph Graph
        {
            get { return _graph; }
            set
            {
                _graph = value;
                NotifyPropertyChanged("Graph");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InitializeLayoutAlgorithms()
        {
            //Add Layout Algorithm Types
            _layoutAlgorithmTypes.Add("BoundedFR");
            _layoutAlgorithmTypes.Add("Circular");
            _layoutAlgorithmTypes.Add("CompoundFDP");
            _layoutAlgorithmTypes.Add("EfficientSugiyama");
            _layoutAlgorithmTypes.Add("FR");
            _layoutAlgorithmTypes.Add("ISOM");
            _layoutAlgorithmTypes.Add("KK");
            _layoutAlgorithmTypes.Add("LinLog");
            _layoutAlgorithmTypes.Add("Tree");

            //Pick a default Layout Algorithm Type
            LayoutAlgorithmType = "BoundedFR";
        }

        private void CreateDiagram(Solution solution)
        {
            if (!solution.IsOpen)
            {
                return;
            }

            var vertices = new List<DependencyVertex>();
            var graph = new DependencyGraph(true);

            foreach (Project project in solution.Projects)
            {
                VSProject vsProj = project.Object as VSProject;

                if (vsProj != null)
                {
                    graph.AddVertex(new DependencyVertex(project.Name));
                }
            }

            foreach (Project project in solution.Projects)
            {
                VSProject vsProj = project.Object as VSProject;

                if (vsProj != null)
                {
                    foreach (Reference reference in vsProj.References)
                    {
                        if (graph.Vertices.Any(x => x.ID == reference.Name))
                        {
                            var vertex1 = graph.Vertices.Single(x => x.ID == project.Name);
                            var vertex2 = graph.Vertices.Single(x => x.ID == reference.Name);
                            graph.AddEdge(new DependencyEdge("", vertex1, vertex2));
                        }
                    }
                }
            }

            Graph = graph;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

}
