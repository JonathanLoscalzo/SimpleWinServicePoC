using Castle.DynamicProxy;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WinservicePoC.Interceptors
{
    class JobInterceptor : Attribute, IInterceptor
    {

        public JobInterceptor()
        {
        }

        public void Intercept(IInvocation invocation)
        {
            // TODO add atribute
            // https://stackoverflow.com/questions/27473012/how-can-i-write-interceptor-aop-with-castle-core-or-other-libraries-just-free
            var type = invocation.TargetType.GetType();
            var Log = Serilog.Log.ForContext(type);
            var completeName = $"{invocation.TargetType.Name}.{invocation.Method.Name}()";
            var methodReference = Guid.NewGuid();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                Log.Information($"Invocando servicio {completeName} (ref: {methodReference})");
                invocation?.Proceed();
                invocation.ReturnValue = WatchAsync((Task)invocation.ReturnValue);
            }
            catch (Exception ex)
            {
                Log.Error($"Error en Servicio {completeName} (ref: {methodReference}) - Exception: {ex.ToString()}");
            }
            finally
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);

                Log.Information($"Finaliza servicio {completeName} (ref: {methodReference}) - Runtime: {elapsedTime}");
            }
        }

        private static async Task WatchAsync(Task methodExecution)
        {
            await methodExecution.ConfigureAwait(false);
        }
    }
}
