using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP12.Filters
{
    public class UserLoggerFilter : Attribute, IAsyncResourceFilter
    {
        private readonly FileLogger logger;
        public UserLoggerFilter()
        {
            this.logger = new FileLogger(@"userlog.txt");
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            bool isListed = false;
            string[] ips = System.IO.File.ReadAllLines(@"userlog.txt");
            foreach (string ip in ips)
                if (ip.Equals(GetIP()))
                    isListed = true;
            if (!isListed)
                logger.LogInformation($"{GetIP()}");
            await next();
        }
        private string GetIP() => System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
    }
}

}
