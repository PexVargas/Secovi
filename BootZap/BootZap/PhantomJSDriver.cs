using OpenQA.Selenium;

namespace BootZap
{
    internal class PhantomJSDriver : IWebDriver
    {
        private string v;
        private PhantomJSOptions options;

        public PhantomJSDriver(string v, PhantomJSOptions options)
        {
            this.v = v;
            this.options = options;
        }
    }
}