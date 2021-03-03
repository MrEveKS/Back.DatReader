namespace Back.DatReader.Models
{
    public class Header
    {
        public int Version { get; set; }
        public string Name { get; set; }
        public ulong Timestamp { get; set; }
        public int Records { get; set; }
        public uint OffsetRanges { get; set; }
        public uint OffsetCities { get; set; }
        public uint OffsetLocations { get; set; }
    }
}
