namespace Template.Infrastructure
{
    public class EntaSettings
    {
        public string EntaVersion { get; set; }
        

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
