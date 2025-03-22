using System;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

namespace ElfsharpWebpage
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var bootstrapper = Bootstrapper.Factory.CreateWeb(args);

            return await bootstrapper.RunAsync();
        }
    }
}

