using Microsoft.AspNetCore.Mvc;
using NorskByggtjenesteTestApplication.Services;
using System;
using System.Collections.Generic;

namespace NorskByggtjenesteTestApplication.Controllers
{
    /// <summary>
    /// FileManagmentController controller
    /// </summary>
    [Route("api/[controller]")]
    public class FileManagmentController : Controller
    {
        private readonly IFileManagmentService fileManagmentService; //IFileManagmentService field

        /// <summary>
        /// Initializes the controller dependencies
        /// </summary>
        public FileManagmentController(IFileManagmentService fileManagmentService) //IFileManagmentService injection to controller
        { 
            this.fileManagmentService = fileManagmentService; //Assigning injected service to field
        }

        /// <summary>
        /// SaveFiles method that take list of written contents by user and save files to file system
        /// </summary>
        [HttpPost("[action]")]
        public IActionResult SaveFiles([FromBody] IEnumerable<string> filesContents)
        {
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Path to Documents folder where file will be saved
                fileManagmentService.CreateFilesFromContents(filesContents, documentsPath); //Execute CreateFiles method from fileManagmentService
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //Return 400 if somethiung wrong happend
            }

            return Ok(); //Return 200 when all files created
        }
    }
}
