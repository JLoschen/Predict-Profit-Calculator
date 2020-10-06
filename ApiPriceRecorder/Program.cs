using ApiPriceRecorder.Ninject;
using Ninject;

namespace ApiPriceRecorder
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var kernel = new StandardKernel(new PriceRecorderNinjectModule()))
            {
                var recorder = kernel.Get<Recorder>();
                recorder.Run().ConfigureAwait(false).GetAwaiter().GetResult(); 
            }
        }
    }
}
