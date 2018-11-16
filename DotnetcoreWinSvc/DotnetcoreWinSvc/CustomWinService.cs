using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.ServiceProcess;
using System.Threading;

namespace DotnetcoreWinSvc
{
    public class CustomWinService : ServiceBase
    {
        private readonly IHost _host;
        private readonly ILogger _logger;
        private readonly IApplicationLifetime _appLifetime;

        private bool _stopRequestedByWindows;

        public CustomWinService(IHost host) : base()
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));

            _logger = _host.Services.GetService(typeof(ILogger<CustomWinService>)) as ILogger<CustomWinService>;
            _appLifetime = _host.Services.GetService(typeof(IApplicationLifetime)) as IApplicationLifetime;

            _appLifetime.ApplicationStopped.Register(()=> {

                if (!_stopRequestedByWindows)
                    base.Stop();
            });
        }

        protected sealed override void OnStart(string[] args)
        {
            _logger.LogInformation("host.start() host called.");
            _host.Start();
            _logger.LogInformation("host.start() host done.");
        }

        protected sealed override void OnStop()
        {
            _stopRequestedByWindows = true;            
            try
            {
                _logger.LogInformation("host.stopasync() host called.");
                _host.StopAsync(new CancellationToken()).GetAwaiter().GetResult();
                _logger.LogInformation("host.stopasync() host done.");
            }
            finally
            {
                _host.Dispose();                
            }
        }
    }
}
