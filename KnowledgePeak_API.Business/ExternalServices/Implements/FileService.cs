using KnowledgePeak_API.Business.Constants;
using KnowledgePeak_API.Business.Exceptions.File;
using KnowledgePeak_API.Business.Extensions;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace KnowledgePeak_API.Business.ExternalServices.Implements;

public class FileService : IFileService
{
    readonly IHostEnvironment _env;

    public FileService(IHostEnvironment env)
    {
        _env = env;
    }

    public void Delete(string? path)
    {
        if (String.IsNullOrEmpty(path) || String.IsNullOrWhiteSpace(path))
            throw new FilePathIsNullOrWhiteSpaceException();
        if (!path.StartsWith(RootConstants.Root))
            path = Path.Combine(RootConstants.Root, path);
        if (File.Exists(path))
            File.Delete(path);
    }

    public async Task SaveAsync(IFormFile file, string path)
    {
        using FileStream fs = new FileStream(Path.Combine(RootConstants.Root, path), FileMode.Create);
        await file.CopyToAsync(fs);
    }
    private string _renameFile(IFormFile file)
        => Guid.NewGuid() + Path.GetExtension(file.FileName);

    private void _checkDirectory(string path)
    {

        if (!Directory.Exists(Path.Combine(RootConstants.Root, path)))
        {
            Directory.CreateDirectory(Path.Combine(RootConstants.Root, path));
        }
    }

    public async Task<string> UploadAsync(IFormFile file, string path, string contentType = "image", int mb = 2)
    {
        if (!file.IsSizeValid(mb)) throw new FileSizeInvalidException();
        if (!file.IsTypeValid("image")) throw new FileTypeInvalidExveption();
        string newFileName = _renameFile(file);
        _checkDirectory(path);
        path = Path.Combine(path, newFileName);
        await SaveAsync(file, path);
        return path;
    }
}
