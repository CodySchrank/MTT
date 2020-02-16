namespace MTT
{
    public class LineObject
    {
        public string VariableName { get; set; }
        public string Type { get; set; }
        public bool IsArray { get; set; }
        public bool IsOptional { get; set; }
        public bool UserDefined { get; set; }
        public string UserDefinedImport { get; set; }
        public LineObject[] Container { get; set; }
        public bool IsContainer { 
            get {
                return Container != null ? Container.Length > 0 : false;
            }
        } 
    }
}