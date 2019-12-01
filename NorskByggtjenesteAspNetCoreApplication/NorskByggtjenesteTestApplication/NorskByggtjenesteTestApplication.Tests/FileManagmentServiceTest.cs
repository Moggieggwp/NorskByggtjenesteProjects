using NorskByggtjenesteTestApplication.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace NorskByggtjenesteTestApplication.Tests
{
    public class FileManagmentServiceTest
    {
        [Fact]
        public void FilesShouldBeWrittenSequantially()
        {
            var fileManagmentSerice = new FileManagmentService();
            string path = $"{Directory.GetCurrentDirectory()}";

            var fileContents = new List<string> { "1", "2", "3" }; // List of contents that should be saved in files

            fileManagmentSerice.CreateFilesFromContents(fileContents, path); //Invoke CreateFiles method

            //Read all created files and check content to be sure that its created sequantially
            IEnumerable<bool> allFilesWrittenSequantially = fileContents.Select((content, index) =>
            {
                var fileContent = File.ReadAllText(Path.Combine(path, string.Format("generated_file_{0}.txt", ++index)));
                return fileContent == content && fileContent == index.ToString(); //Condition to check if content of file is equal to initial content and equal to index number
            }).ToList();

            Assert.True(allFilesWrittenSequantially.All(x => x == true)); //True if content of files is equal to its index number
        }

        [Fact]
        public void FilesCanBeWithoutAnyContent()
        {
            var fileManagmentSerice = new FileManagmentService();
            string path = $"{Directory.GetCurrentDirectory()}";

            var fileContents = new List<string> { "1", "", "3" }; // List of contents that should be saved in files

            fileManagmentSerice.CreateFilesFromContents(fileContents, path); //Invoke CreateFiles method

            //Read all created files and check that file without any content has been successfully saved
            IEnumerable<bool> allFilesWrittenSequantially = fileContents.Select((content, index) =>
            {
                var fileContent = File.ReadAllText(Path.Combine(path, string.Format("generated_file_{0}.txt", ++index)));
                return string.IsNullOrEmpty(fileContent); //Condition to check if file has any content
            }).ToList();

            Assert.Contains(allFilesWrittenSequantially, x => x == true); //True when there are at least one file with empty content
        }
    }
}
