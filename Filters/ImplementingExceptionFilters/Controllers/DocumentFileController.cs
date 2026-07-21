using ImplementingExceptionFilters.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ImplementingExceptionFilters.Controllers
{
    [Route("/api/files")]
    public class DocumentFileController : ControllerBase 
    {
        [HttpGet("{docNum}")]
        
        public IActionResult GetFile([FromRoute] int docNum)
        {
            var fileName = "fackFile.pdf";

            var filePath = Path.Combine("C:\\savePdf", fileName);

            if (!System.IO.File.Exists(filePath))
                throw new FileNotFoundException("Not Found File" , fileName);

            return PhysicalFile(filePath,"application/pdf", fileName);

        }
    }
}
