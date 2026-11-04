using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MESINA_11042026.Services
{
    public record FileProcessingRecord(string FileName, long FileSize, string ProcessingTime, string Status);

    public interface IFileTrackingService
    {
        void TrackFile(string fileName, long fileSize, string processingTime);

        int GetTotalProcessedCount();
        
        IEnumerable<FileProcessingRecord> GetProcessedFiles();
    }

    public class FileTrackingService : IFileTrackingService
    {

        private readonly ConcurrentBag<FileProcessingRecord> _processedFiles = new();

        public void TrackFile(string fileName, long fileSize, string processingTime)
        {
            _processedFiles.Add(new FileProcessingRecord(fileName, fileSize, processingTime, "Success"));
        }

        public int GetTotalProcessedCount() => _processedFiles.Count;

        public IEnumerable<FileProcessingRecord> GetProcessedFiles() => _processedFiles;
    }
}