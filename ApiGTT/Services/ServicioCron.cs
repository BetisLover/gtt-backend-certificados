using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApiGTT.Services
{
    internal class ServicioCron : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;

        public ServicioCron(ILogger<ServicioCron> logger){
            _logger = logger;
        }

       


        //interfaz idisposable
        public void Dispose()
        {
            _timer?.Dispose();
        }


        //interfaz ihosted
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("arrancando el servicio");
            _timer = new Timer(DoWork,null,TimeSpan.Zero, TimeSpan.FromHours(12));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("parando el servicio");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        


        public void DoWork(object state)
        {
            _logger.LogInformation("ejecutando tarea");
        }
    }
}
