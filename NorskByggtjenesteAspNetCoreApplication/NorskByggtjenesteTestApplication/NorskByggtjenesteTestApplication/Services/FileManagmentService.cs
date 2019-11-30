using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NorskByggtjenesteTestApplication.Services
{
    public class FileManagmentService : IFileManagmentService
    {
        private string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Path to Documents folder where file will be saved
        private string fileNameTemplate = "generated_file_{0}.txt"; //Template of file name where {0} number of action in queue

        public async Task CreateFilesFromContents(IEnumerable<string> listOfContents)
        {
            var actionExecutor = new ActionExecutor(); //Initialization of ActionExecutor class

            //Select each created content
            listOfContents.Select((content, index) =>
            {
                //Add action with writing content to file to list of actions
                actionExecutor.AddActionToQueue(() =>
                {
                    //Standart .net procudere of writing file to path
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(documentsPath, string.Format(fileNameTemplate, ++index))))
                    {
                        outputFile.Write(content);
                    }
                });
                return content;
            }).ToList(); //Because .Select() returns IEnumerable and it's causes LazyLoading

            await actionExecutor.Execute();
        }
    }
}
