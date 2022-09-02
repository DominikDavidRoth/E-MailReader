
class EMail
{
    private string Sender;
    private string Subject;
    private string Date;

    public EMail(string Sender, string Subject, string Date)
    {
        this.Sender = Sender;
        this.Subject = Subject;
        this.Date = Date;
    }


    public string getSender()
    {
        return Sender;
    }

    public string getSubject()
    {
        return Subject;
    }

    public string getDate()
    {
        return Date;
    }

}