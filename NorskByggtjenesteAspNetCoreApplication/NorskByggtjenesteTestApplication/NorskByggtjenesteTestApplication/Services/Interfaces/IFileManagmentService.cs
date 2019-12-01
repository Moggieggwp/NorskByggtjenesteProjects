using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorskByggtjenesteTestApplication.Services
{
    public interface IFileManagmentService
    {
        void CreateFilesFromContents(IEnumerable<string> listOfContents, string path);
    }
}
