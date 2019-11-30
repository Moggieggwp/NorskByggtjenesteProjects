using Microsoft.AspNetCore.Mvc;
using NorskByggtjenesteTestApplication.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorskByggtjenesteTestApplication.Controllers
{
    [Route("api/[controller]")]
    public class FileManagmentController : Controller
    {
        private readonly IFileManagmentService fileManagmentService;

        public FileManagmentController(IFileManagmentService fileManagmentService)
        {
            this.fileManagmentService = fileManagmentService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveFiles([FromBody] IEnumerable<string> filesContents)
        {
            try
            {
                await fileManagmentService.CreateFilesFromContents(filesContents);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
