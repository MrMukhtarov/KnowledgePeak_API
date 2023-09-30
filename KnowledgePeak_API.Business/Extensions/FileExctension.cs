using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Extensions;

public static class FileExctension
{
    public static bool IsSizeValid(this IFormFile file, int mb)
    {
        return file.Length <= mb * 1024 * 1024;
    }
    public static bool IsTypeValid(this IFormFile file, string contentType)
    {
        return file.ContentType.StartsWith(contentType);
    }
}
