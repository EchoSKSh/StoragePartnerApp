using Microsoft.AspNetCore.Http;

namespace StoragePartnerApp.Helpers
{
    public class CustomFormFile : IFormFile
    {
        private readonly Stream _stream;
        private readonly string _fileName;
        private readonly string _contentType;

        public CustomFormFile(Stream stream, string fileName, string contentType)
        {
            _stream = stream;
            _fileName = fileName;
            _contentType = contentType;
        }

        public string ContentType => _contentType;

        public string ContentDisposition => $"form-data; name=\"{_fileName}\"; filename=\"{_fileName}\"";

        public IHeaderDictionary Headers => new HeaderDictionary();

        public long Length
        {
            get
            {
                if (_stream == null)
                {
                    throw new InvalidOperationException("Stream is not available.");
                }

                if (!_stream.CanRead)
                {
                    throw new InvalidOperationException("Stream is not readable.");
                }

                // Ensure the stream is at the beginning
                _stream.Seek(0, SeekOrigin.Begin);

                return _stream.Length;
            }
        }


        public string Name => _fileName;

        public string FileName => _fileName;

        public void CopyTo(Stream target)
        {
            _stream.CopyTo(target);
        }

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            await _stream.CopyToAsync(target, cancellationToken);
        }

        public Stream OpenReadStream()
        {
            return _stream;
        }
    }
}
