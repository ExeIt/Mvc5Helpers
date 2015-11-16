namespace Torchlight.Mvc5.Common.Libs.ModelAssets.AlertAssets
{
    public class Alert
    {
        public Alert(string alertClass, string message)
        {
            Message = message;
            AlertClass = alertClass;
        }

        public string AlertClass { get; set; }

        public string Message { get; set; }
    }
}
