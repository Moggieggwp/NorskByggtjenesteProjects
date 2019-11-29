using NorskByggtjenesteTestApplication.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace NorskByggtjenesteTestApplication
{
    public partial class MainWindow : Window
    {
        private List<string> fileContents = new List<string>(); //List with file's content
        private string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Path to Documents folder where file will be saved
        private string fileNameTemplate = "generated_file_{0}.txt"; //Template of file name where {0} number of action in queue

        public MainWindow()
        {
            InitializeComponent();
            this.CountOfContentsLabel.Content = "0"; //Default value of label
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var actionExecutor = new ActionExecutor(); //Initialization of ActionExecutor class

            //Select each created content
            fileContents.Select((content, index) =>
            {
                //Add action with writing content to file to list of actions
                actionExecutor.AddActionToQueue(() =>
                {
                    Thread.Sleep(2000); //Just to make action longer and be sure that UI is not blocked
                    //Standart .net procudere of writing file to path
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(documentsPath, string.Format(fileNameTemplate, ++index))))
                    {
                        outputFile.Write(content);
                    }
                });
                return content;
            }).ToList(); //Because .Select() returns IEnumerable and it's causes LazyLoading

            actionExecutor.Execute(); //Execute actions in background sequentially

            this.ContentTextBox.Clear(); //Refresh TextBox
            this.CountOfContentsLabel.Content = "0"; //Default value of label
        }

        private void AddNewContent_Click(object sender, RoutedEventArgs e)
        {
            fileContents.Add(this.ContentTextBox.Text); //Add content from TextBox to list of contents

            this.CountOfContentsLabel.Content = fileContents.Count; //Show label with count of contents
            this.ContentTextBox.Clear(); //Refresh TextBox
        }
    }
}
