using MESINA_11042026.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MESINA_11042026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly IFileTrackingService _trackingService;

        public FileUploadController(IFileProcessor fileProcessor, IFileTrackingService trackingService)
        {
            _fileProcessor = fileProcessor;
            _trackingService = trackingService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded or file is empty.");
            }

            var stopwatche = Stopwatch.StartNew();

            try
            {
                using var stream = file.OpenReadStream();
                var result = await _fileProcessor.ProcessFileAsync(stream);

                stopwatche.Stop();
                _trackingService.TrackFile(file.FileName, file.Length, $"{stopwatche.ElapsedMilliseconds} ms");

                return Ok(new
                {
                    Message = "File processed successfully",
                    FileName = file.FileName,
                    ProcessingTimeMs = stopwatche.ElapsedMilliseconds,
                    Result = result
                });

            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Error = "Error processing file", Details = ex.Message });
            }
        }

        [HttpGet("reports")]
        public IActionResult GetReport()
        {
            return Ok(new
            {
                TotalFilesProcessed = _trackingService.GetTotalProcessedCount(),
                ProcessedFilesHistory = _trackingService.GetProcessedFiles()
            });
        }
    }
}