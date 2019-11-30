using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorskByggtjenesteTestApplication.Services
{
    public interface IFileManagmentService
    {
        Task CreateFilesFromContents(IEnumerable<string> listOfContents);
    }
}
