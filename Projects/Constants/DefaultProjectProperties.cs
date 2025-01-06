using Projects.Entities;
using Projects.Enums;

namespace Projects.Constants;

public static class DefaultProjectProperties
{
    public static Property Name = new Property("Name","Name", Datatype.Text);
    public static Property Key = new Property("Key","Key", Datatype.Text);
    public static Property Description = new Property("Description","Description", Datatype.TextArea);
    public static Property Status = new Property("Status","Status", Datatype.Number);
    public static Property Leader = new Property("Leader","Leader", Datatype.Person);
    public static Property Url = new Property("Url","Url", Datatype.Text);
    public static Property StartDate = new Property("StartDate","Start Date", Datatype.DateTime);
    public static Property EndDate = new Property("EndDate","End Date", Datatype.DateTime);
}
