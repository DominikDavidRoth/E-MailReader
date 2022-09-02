using System.Net;

public class APIHeandler
{
    private WebClient client =  new WebClient();

    public void send(string ticketTitel, string ticketText, string TicketPriority)
    {
        System.Collections.Specialized.NameValueCollection parameters = new System.Collections.Specialized.NameValueCollection();
        parameters.Add("Key", "");                                                                      // key muss hinzugefügt werden
        parameters.Add("PersonTitle", "");                                                              // can be edited if needed
        parameters.Add("PersonAcademicTitle", "");                                                      // can be edited if needed
        parameters.Add("PersonFirstName", "VM");                                                        // can be edited if needed
        parameters.Add("PersonLastName", "Checker");                                                    // can be edited if needed
        parameters.Add("PersonMail", "");                                                               // can be edited if needed
        parameters.Add("TicketType", "Incident");                                                       // can be edited if needed
        parameters.Add("TicketPriority", TicketPriority);
        parameters.Add("TicketTitle", ticketTitel);
        parameters.Add("TicketText", ticketText);

        try
        {
            client.UploadValues("https://hms.hinrichs-it.de/Connector/Ticket.asmx/New", parameters);    //Sends the ticket
        }
        catch (Exception e)
        {
            Console.WriteLine("Ticket Error: " + e.Message);
        }

    }
}

