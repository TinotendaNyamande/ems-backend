using EMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AttachmentsController(
        IEmailAttachmentDownloadService _download,
        IFileStorageRepository _storage) : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Content(
            Guid id,
            [FromQuery] bool download = false,
            CancellationToken ct = default)
        {
            var meta = await _download.GetDownloadAsync(id, ct);

            var stream = await _storage.OpenReadAsync(meta.FilePath, ct);

            var disposition = download ? "attachment" : "inline";

            var cd = new Microsoft.Net.Http.Headers.ContentDispositionHeaderValue(disposition);
            cd.SetHttpFileName(meta.FileName);  
            Response.Headers.ContentDisposition = cd.ToString();

            return File(
                stream,
                contentType: meta.FileType,
                enableRangeProcessing: true);
        }

        private static bool IsUnsafeForInline(string contentType)
        {
            // These can execute scripts in the user's origin if served inline
            return contentType is
                "text/html" or
                "application/xhtml+xml" or
                "image/svg+xml" or
                "application/javascript" or
                "text/javascript";
        }
    }
}