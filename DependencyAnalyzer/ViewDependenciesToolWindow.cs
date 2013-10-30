using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using System.Windows.Controls;

namespace KitsuneSoft.DependencyAnalyzer
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    ///
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane, 
    /// usually implemented by the package implementer.
    ///
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its 
    /// implementation of the IVsUIElementPane interface.
    /// </summary>
    [Guid("ed24c3ba-1e06-492c-9c83-40632d21eaf7")]
    public class ViewDependenciesToolWindow : ToolWindowPane
    {
        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public ViewDependenciesToolWindow() :
            base(null)
        {
            try
            {
                // Set the window title reading it from the resources.
                this.Caption = Resources.ToolWindowTitle;
                // Set the image that will appear on the tab of the window frame
                // when docked with an other window
                // The resource ID correspond to the one defined in the resx file
                // while the Index is the offset in the bitmap strip. Each image in
                // the strip being 16x16.
                this.BitmapResourceID = 301;
                this.BitmapIndex = 1;

                base.Content = new ViewDependenciesControl();
            }
            catch (Exception)
            {
                MessageBox.Show("The view dependencies window is currently unavailable");
            }
        }

        public void Refresh()
        {
            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on 
            // the object returned by the Content property.

            var control = base.Content as ViewDependenciesControl;

            if (control != null)
            {
                control.DataContext = new DependencyViewModel();
            }
        }
    }
}
