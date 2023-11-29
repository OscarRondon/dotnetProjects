namespace BlazorServerApp.Models
{
    public class DataGridColumnDefinition
    {
        public string DataField { get; set; }
        public string Caption { get; set; }
        public DataTypeEnum DataType { get; set; } = DataTypeEnum.none;
        public string Format { get; set; }
        public AligmentEnum Aligment { get; set; } = AligmentEnum.none;
        

    }
}
