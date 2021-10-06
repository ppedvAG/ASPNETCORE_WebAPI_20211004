using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstRestfulService.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Upload von Dateien
        /// </summary>
        /// <param name="files">multiple Files</param>
        /// <param name="subDirectory">Unterverzeichnis als Upload-Ziel</param>
        void UploadFile(List<IFormFile> files, string subDirectory);

        /// <summary>
        /// Download von Dateien als Zip
        /// </summary>
        /// <param name="subDirectory">Angabe des zu downloadenten Unterverzeichnis</param>
        /// <returns></returns>
        (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory);


        /// <summary>
        /// Größenangabe als String
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        string SizeConverter(long bytes);


    }
}
