namespace Projects.Enums;

public enum Datatype
{
    // Text inputs for string values
    Text, // Simple text (e.g., project name, task title)
    TextArea, // Multiline text (e.g., project description, task details)

    // Numeric inputs for numbers
    Number, // Number values (e.g., task priority, project budget)
    Decimal, // Decimal values (e.g., task cost, estimated effort)

    // Date and time inputs
    DateTime, // Date and time (e.g., project start date, task due date)
    TimeSpan, // Duration (e.g., task duration, time spent on task)

    // Boolean (True/False)
    Boolean, // Checkboxes (e.g., task completed, project active)

    // Choice-based inputs
    RadioButton, // Single choice (e.g., task status, project phase)
    SelectList, // Dropdown select (e.g., task assignee, project category)

    // File upload (e.g., attachments)
    File, // File input (e.g., project documents, task attachments)

    // People picker or multi-select
    MultiSelect, // Multiple selections (e.g., team members, project stakeholders)
    Person // Single person (e.g., task owner, project manager)
}
