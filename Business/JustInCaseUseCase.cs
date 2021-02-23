using System;
using Core.Business;
using Core.Business.Base;
using Serilog;

namespace Business
{
    public class JustInCaseUseCase : IJustInCase
    {
        private ILogger logger = Log.ForContext<JustInCaseUseCase>();

        public JustInCaseUseCase()
        {
        }

        public void Execute()
        {
            logger.Information("Executing JUST IN CASE");
        }
    }
}
