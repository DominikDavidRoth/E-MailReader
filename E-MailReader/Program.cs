using MailKit.Net.Imap;
using MailKit;
using System.Text.RegularExpressions;

class Program
{
    public void Mailreader(List<EMail> e)
    {
        using (var client = new ImapClient())
        {
            using (var cancel = new CancellationTokenSource())
            {
                //Conect to Email Client
                client.Connect("imap.gmail.com", 993, true, cancel.Token);          //Change to provider (Addrase,port,ssl necessary?,Token)

                client.Authenticate("zieliepavon@gmail.com", "ognpbwcohhsatqcl");   //Change to Acount (Username, Password)

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadWrite, cancel.Token);

                //Get the nessesery Data from the Emails
                int c = inbox.Count;
                for (int i = 0; i < c; i++)
                {
                    var message = inbox.GetMessage(i, cancel.Token);

                    String Sender = message.From.ToString();
                    String Date = message.Date.ToString();

                    EMail eMail = new EMail(Sender, message.Subject, Date);

                    e.Add(eMail);
                }

                var folder = client.GetFolder("test");                              //Defines in wich Folder the Emails are send after reding them

                for (int i = 0; i < c; i++)
                {
                    if (c > 0)
                    {
                        inbox.MoveTo(0, folder);                                    //Move Emails to another folder
                    }
                }

                client.Disconnect(true, cancel.Token);
            }
        }
    }


    public async void SearchForVMProplems(List<EMail> EList,List<Device> DList)
    {
        APIHeandler api = new APIHeandler();

        for (int i = 0; i < EList.Count; i++)
        {
            String tobeSearched = EList[i].getSubject();
            int splitpoint = 10;
            int startpoint = 1;
            string vmname = "";

            Regex rx = new Regex("C[0-9]");
            foreach (Match match in rx.Matches(tobeSearched))
            {
                startpoint = match.Index;
            }

            //Finding Warnings and Failings and create a Ticket from them
            if (tobeSearched.Contains("[Warning]"))
            {
                splitpoint = tobeSearched.IndexOf("[Warning]");
                vmname = tobeSearched.Substring(startpoint, splitpoint - startpoint);
                vmname = vmname.Replace(" ", "");
                vmname = vmname.Replace("-", "");
                api.send("VM Warning","VM " + vmname + " has send a Warning", "Medium");
            }
            if (tobeSearched.Contains("[Failed]"))
            {
                splitpoint = tobeSearched.IndexOf("[Failed]");
                vmname = tobeSearched.Substring(startpoint, splitpoint - startpoint);
                vmname = vmname.Replace(" ", "");
                vmname = vmname.Replace("-", "");
                api.send("VM Failed", "VM " + vmname + " has Failed", "Medium");
            }
            if (tobeSearched.Contains("[Success]"))
            {
                splitpoint = tobeSearched.IndexOf("[Success]");
                vmname = tobeSearched.Substring(startpoint, splitpoint - startpoint);
                vmname = vmname.Replace(" ", "");
                vmname = vmname.Replace("-", "");
            }


            //Finding missing Devices by removing known
            Device toBeRemovedDevice = new Device("test");

            foreach (Device d in DList)
            {
                string device = d.getName();
                device = device.Replace(" ", "");
                device = device.Replace("-", "");
                if (device.Equals(vmname))
                {
                    toBeRemovedDevice = d;
                }
            }
            DList.Remove(toBeRemovedDevice);

        }

        //create ticket for missing Devices
        foreach (Device d in DList)
        {
            string device = d.getName();
            device = device.Replace(" ", "");
            device = device.Replace("-", "");
            api.send("VM didnt Respond", "VM " + device + " has not Responded within 24h", "Medium");
        }

    }


    public void SearchMeneger()
    {
        List<EMail> eMailsList = new List<EMail>();
        List<Device> eDevicesList = new List<Device>();
        DatabaseConection databaseConection = new DatabaseConection();

        Mailreader(eMailsList);
        databaseConection.getDivices(eDevicesList);

        SearchForVMProplems(eMailsList, eDevicesList);

    }

}