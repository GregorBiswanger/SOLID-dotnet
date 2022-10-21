using System;
using PlzSuperTool.Infrastructure.Features.MainWindow;
using PlzSuperTool.Infrastructure.Services;

namespace PlzSuperTool
{
    public class ZipRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ZipRepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IZipRepository Create()
        {
            if (Network.IsOnline())
            {
                return _serviceProvider.GetService(typeof(ZipWebRepository)) as IZipRepository;
            }

            return _serviceProvider.GetService(typeof(ZipFileRepository)) as IZipRepository;
        }
    }
}
