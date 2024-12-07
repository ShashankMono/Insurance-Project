namespace Insurance_final_project.Services
{
    public interface ICloudSettings
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
