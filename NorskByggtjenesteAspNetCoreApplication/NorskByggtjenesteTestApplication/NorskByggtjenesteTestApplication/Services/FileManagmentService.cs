using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NorskByggtjenesteTestApplication.Services
{
    /// <summary>
    /// FileManagmentService service
    /// </summary>
    public class FileManagmentService : IFileManagmentService
    {
        private string fileNameTemplate = "generated_file_{0}.txt"; //Template of file name where {0} number of action in queue

        /// <summary>
        /// Take list of contents and manages to write each of contents to file created by path
        /// </summary>
        public void CreateFilesFromContents(IEnumerable<string> listOfContents, string path)
        {
            //Process each generated content
            listOfContents.Select((content, index) => //index used in file name
            {
                //Standart .net procudere of writing file to path
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, string.Format(fileNameTemplate, ++index))))
                {
                    outputFile.WriteAsync(content);
                }
                return content;
            }).ToList(); //Because .Select() returns IEnumerable and it's causes LazyLoading and never executes
        }
    }
}
