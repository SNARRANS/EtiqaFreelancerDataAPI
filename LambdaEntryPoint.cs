using Amazon.Lambda.AspNetCoreServer;
using Microsoft.AspNetCore.Hosting;
namespace EtiqaFreelancerDataAPI
{
    public class LambdaEntryPoint : APIGatewayHttpApiV2ProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            base.Init(builder);
            builder.UseStartup<StartupBase>();
        }
    }
}
