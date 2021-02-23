using System;
using System.Configuration;

namespace Winservice.Configuration
{
    public class AppSettings
    {
        public int DelayInMinutes
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["serviceRecoveryRestartAfterWaitingDelayPeriod"]);
            }
        }

        public string ScheduleType
        {
            get
            {
                return ConfigurationManager.AppSettings["scheduleType"];
            }
        }

        public string CronScheduleJobExpressionKey
        {
            get
            {
                return ConfigurationManager.AppSettings["Environment"].ToLower().Equals("production") ?
                 "CronScheduleJobProd" : "CronScheduleJobDev";
            }
        }

        public string CronScheduleJobExpression
        {
            get => ConfigurationManager.AppSettings[CronScheduleJobExpressionKey];
        }

        public string ServiceName { get => ConfigurationManager.AppSettings["serviceName"]; }

        public string ServiceNameFull { get => ConfigurationManager.AppSettings["serviceName"] + ConfigurationManager.AppSettings["Environment"].ToUpper(); }

        public string DisplayName { get => ConfigurationManager.AppSettings["displayName"]; }

        public string Description { get => ConfigurationManager.AppSettings["description"]; }

        public int ServiceRecoveryRestartAfterWaitingDelayPeriod { get => int.Parse(ConfigurationManager.AppSettings["serviceRecoveryRestartAfterWaitingDelayPeriod"]); }

        public int ProcessQuantityRunningAtATime { get => int.Parse(ConfigurationManager.AppSettings["processQuantityRunningAtATime"]); }

    }
}
