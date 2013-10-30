using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VSLangProj;

namespace KitsuneSoft.DependencyAnalyzer
{
    public class DependencyViewModel
    {
        private readonly List<String> _layoutAlgorithmTypes = new List<string>();
        private DependencyGraph _graph;
        private string _layoutAlgorithmType;

        public DependencyViewModel()
        {
            RefreshCommand = new DelegateCommand(x => Refresh());

            InitializeLayoutAlgorithms();

            Refresh();
        }

        private void Refresh()
        {
            var dte = (DTE)Microsoft.VisualStudio.Shell.ServiceProvider.GlobalProvider.GetService(typeof(SDTE));

            if (!dte.Solution.IsOpen)
            {
                MessageBox.Show("No solution open");
            }

            CreateDiagram(dte.Solution);
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

        public ICommand RefreshCommand
        {
            get;
            private set;
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

            var validProjects = GetValidProjects(solution);

            foreach (var project in validProjects)
            {
                graph.AddVertex(new DependencyVertex(project.Project.Name));
            }

            foreach (var project in validProjects)
            {

                    foreach (Reference reference in project.References)
                    {
                        if (graph.Vertices.Any(x => x.ID == reference.Name))
                        {
                            var vertex1 = graph.Vertices.Single(x => x.ID == project.Project.Name);
                            var vertex2 = graph.Vertices.Single(x => x.ID == reference.Name);
                            graph.AddEdge(new DependencyEdge("", vertex1, vertex2));
                        }
                    }
            }

            Graph = graph;
        }

        private static List<VSProject> GetValidProjects(Solution solution)
        {
            var validProjects = new List<VSProject>();

            foreach (Project project in solution.Projects)
            {
                validProjects = GetValidProjects(project, validProjects);
            }
            return validProjects;
        }

        private static List<VSProject> GetValidProjects(Project project, List<VSProject> validProjects)
        {
            VSProject vsProj = project.Object as VSProject;

            if (vsProj != null)
            {
                validProjects.Add(vsProj);
            }

            foreach (ProjectItem innerProject in project.ProjectItems)
            {
                if (innerProject.SubProject != null)
                {
                    validProjects = GetValidProjects(innerProject.SubProject, validProjects);
                }
            }

            return validProjects;
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
