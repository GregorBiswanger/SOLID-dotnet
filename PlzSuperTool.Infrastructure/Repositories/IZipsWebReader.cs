using PlzSuperTool.Infrastructure.Entities;

namespace PlzSuperTool.Infrastructure.Repositories;

public interface IZipsWebReader
{
    PlzResponse? LoadZipsFrom(string cityName);
}