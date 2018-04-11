namespace Template.Infrastructure
{
    public class ApiSettings
    {
        public string ApiVersion { get; set; }
        

        /// <summary>
        /// Also known as WrkID
        /// </summary>
        public string WorkStationId { get; set; }

        /// <summary>
        /// AppID
        /// </summary>
        public string AppId { get; set; } = string.Empty;
    }
}
