using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using UploadLargeFile.Helpers;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using System.Text;
using System.Globalization;
using UploadLargeFile.Attributes;

namespace UploadLargeFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly ILogger<FileUploadController> _logger;

        // Get the default form options so that we can use them to set the default 
        // limits for request body data.
        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        private const long _fileSizeLimit = 10L * 1024L * 1024L * 1024L; // 10GB, adjust to your need
        
        public FileUploadController(ILogger<FileUploadController> logger)
        {
            this._logger = logger;
        }

        [HttpPost(nameof(UploadWithBuffer))]
        [RequestSizeLimit(_fileSizeLimit)]
        public async Task<IActionResult> UploadWithBuffer(List<IFormFile> files)
        {

            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });

        }

        [HttpPost(nameof(UploadWithStream))]
        [RequestSizeLimit(_fileSizeLimit)]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> UploadWithStream()
        {
            var request = HttpContext.Request;

            //https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/mvc/models/file-uploads/samples/2.x/SampleApp/Utilities/MultipartRequestHelper.cs
            // validation of Content-Type
            // 1. first, it must be a form-data request
            // 2. a boundary should be found in the Content-Type
            if (!request.HasFormContentType ||
               !MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeader) ||
               string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value))
            {
                return new UnsupportedMediaTypeResult();
            }

            var reader = new MultipartReader(mediaTypeHeader.Boundary.Value, request.Body);
            var section = await reader.ReadNextSectionAsync();

            // This sample try to get the first file from request and save it
            // Make changes according to your needs in actual use
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition,
                    out var contentDisposition);

                if (hasContentDispositionHeader && contentDisposition != null && contentDisposition.DispositionType.Equals("form-data") &&
                    !string.IsNullOrEmpty(contentDisposition.FileName.Value))
                {
                    // Don't trust any file name, file extension, and file data from the request unless you trust them completely
                    // Otherwise, it is very likely to cause problems such as virus uploading, disk filling, etc
                    // In short, it is necessary to restrict and verify the upload
                    // Here, we just use the temporary folder and a random file name

                    // Get the temporary folder, and combine a random file name with it
                    var fileName = Path.GetRandomFileName();
                    var saveToPath = Path.Combine(@"C:\app\Nguyen Minh Thanh\kafka-docker\", fileName);

                    using (var targetStream = System.IO.File.Create(saveToPath))
                    {
                        await section.Body.CopyToAsync(targetStream);
                    }

                    return Ok();
                }

                section = await reader.ReadNextSectionAsync();
            }

            // If the code runs to this location, it means that no files have been saved
            return BadRequest("No files data in the request.");


            //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0
            //if (!MultipartRequestHelper.IsMultipartContentType(HttpContext.Request.ContentType ?? string.Empty))
            //{
            //    // Log error
            //    return BadRequest("The request couldn't be processed (Error 1).");
            //}

            //// Accumulate the form data key-value pairs in the request (formAccumulator).
            //var formAccumulator = new KeyValueAccumulator();
            //var trustedFileNameForDisplay = string.Empty;
            //var untrustedFileNameForStorage = string.Empty;
            //var streamedFileContent = Array.Empty<byte>();

            //var boundary = MultipartRequestHelper.GetBoundary(
            //    MediaTypeHeaderValue.Parse(Request.ContentType),
            //    _defaultFormOptions.MultipartBoundaryLengthLimit);
            //var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            //var section = await reader.ReadNextSectionAsync();

            //var path = Path.Combine(@"C:\app\Nguyen Minh Thanh\kafka-docker\", Guid.NewGuid().ToString());
            //Directory.CreateDirectory(path);
            //var fileIndex = 1;

            //while (section != null)
            //{
            //    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

            //    if (hasContentDispositionHeader)
            //    {
            //        if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
            //        {
            //            untrustedFileNameForStorage = contentDisposition.FileName.Value;
            //            // Don't trust the file name sent by the client. To display
            //            // the file name, HTML-encode the value.
            //            trustedFileNameForDisplay = WebUtility.HtmlEncode(contentDisposition.FileName.Value);

            //            //streamedFileContent = await FileHelpers.ProcessStreamedFile(section, contentDisposition, _fileSizeLimit);

            //            //streamedFileContent = streamedFileContent.Union(tempBytes).ToArray();

            //            var saveToPath = Path.Combine(path, fileIndex.ToString() + ".tmp");
            //            using (var targetStream = System.IO.File.Create(saveToPath))
            //            {
            //                await section.Body.CopyToAsync(targetStream);
            //            }
            //            fileIndex++;
            //        }
            //        else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
            //        {
            //            // Don't limit the key name length because the 
            //            // multipart headers length limit is already in effect.
            //            var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name).Value;

            //            var encoding = GetEncoding(section);

            //            if (encoding == null)
            //            {
            //                throw new Exception($"The request couldn't be processed (Error 2).");
            //                // Log error
            //            }

            //            using (var streamReader = new StreamReader(
            //                section.Body,
            //                encoding,
            //                detectEncodingFromByteOrderMarks: true,
            //                bufferSize: 1024,
            //                leaveOpen: true))
            //            {
            //                // The value length limit is enforced by 
            //                // MultipartBodyLengthLimit
            //                var value = await streamReader.ReadToEndAsync();

            //                if (string.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
            //                {
            //                    value = string.Empty;
            //                }

            //                formAccumulator.Append(key, value);

            //                if (formAccumulator.ValueCount >
            //                    _defaultFormOptions.ValueCountLimit)
            //                {
            //                    // Form key count limit of 
            //                    // _defaultFormOptions.ValueCountLimit 
            //                    // is exceeded.
            //                    throw new Exception($"The request couldn't be processed (Error 3).");
            //                    // Log error
            //                }
            //            }
            //        }
            //    }

            //    // Drain any remaining section body that hasn't been consumed and
            //    // read the headers for the next section.
            //    section = await reader.ReadNextSectionAsync();
            //}

            // Bind form data to the model
            //var formData = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>());
            //var formValueProvider = new FormValueProvider(
            //    BindingSource.Form,
            //    new FormCollection(formAccumulator.GetResults()),
            //    CultureInfo.CurrentCulture);
            //var bindingSuccessful = await TryUpdateModelAsync(formData, prefix: "",
            //    valueProvider: formValueProvider);

            //if (!bindingSuccessful)
            //{
            //    throw new Exception("The request couldn't be processed (Error 5).");
            //    // Log error
            //}

            // **WARNING!**
            // In the following example, the file is saved without
            // scanning the file's contents. In most production
            // scenarios, an anti-virus/anti-malware scanner API
            // is used on the file before making the file available
            // for download or for use by other systems. 
            // For more information, see the topic that accompanies 
            // this sample app.

            //return Created(nameof(FileUploadController), null);
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            var hasMediaTypeHeader =
                MediaTypeHeaderValue.TryParse(section.ContentType, out var mediaType);

            // UTF-7 is insecure and shouldn't be honored. UTF-8 succeeds in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }

            return mediaType.Encoding;
        }

        //[HttpPost(nameof(UploadWithStream1))]
        //[RequestSizeLimit(_fileSizeLimit)]
        //[RequestFormLimits(MultipartBodyLengthLimit = _fileSizeLimit)]
        //[DisableFormValueModelBinding]
        //public async Task<IActionResult> UploadWithStream1()
        //{
        //    if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
        //        throw new Exception("Not a multipart request");

        //    var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType));
        //    var reader = new MultipartReader(boundary, Request.Body);

        //    // note: this is for a single file, you could also process multiple files
        //    var section = await reader.ReadNextSectionAsync();

        //    if (section == null)
        //        throw new Exception("No sections in multipart defined");

        //    if (!ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition))
        //        throw new BadRequestException("No content disposition in multipart defined");

        //    var fileName = contentDisposition.FileNameStar.ToString();
        //    if (string.IsNullOrEmpty(fileName))
        //    {
        //        fileName = contentDisposition.FileName.ToString();
        //    }

        //    if (string.IsNullOrEmpty(fileName))
        //        throw new BadRequestException("No filename defined.");

        //    using var fileStream = section.Body;
        //}
    }
}
