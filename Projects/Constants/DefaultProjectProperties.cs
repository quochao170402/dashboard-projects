using Projects.Entities;
using Projects.Enums;

namespace Projects.Constants;

public static class DefaultProjectProperties
{
    public static Property Name = new Property("Name", Datatype.Text);
    public static Property Key = new Property("Key", Datatype.Text);
    public static Property Description = new Property("Description", Datatype.TextArea);
    public static Property Status = new Property("Status", Datatype.Number);
    public static Property LeaderId = new Property("LeaderId", Datatype.Person);
    public static Property Url = new Property("Url", Datatype.Text);
    public static Property StartDate = new Property("StartDate", Datatype.DateTime);
    public static Property EndDate = new Property("EndDate", Datatype.DateTime);
}
