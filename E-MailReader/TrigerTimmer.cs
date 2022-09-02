using System.Timers;

public class TrigerTimmer
{
    System.Timers.Timer aTimer = new System.Timers.Timer();
    double waitingtime;
    public void startTimer()
    {
        setTimer();                                                     //set timer to triger at midnigth
        aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);        //trigers the search programm
        aTimer.Interval = waitingtime;
        aTimer.Enabled = true;
        Console.WriteLine("Press \'q\' to quit the sample.");
        while (Console.Read() != 'q') ;
    }

    private void setTimer()
    {
        //DateTime today = DateTime.Now;
        //DateTime tomorrow = DateTime.Today.AddDays(1);
        //waitingtime = (tomorrow - today).TotalMilliseconds;
        waitingtime = 15000;
    }
    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        Program p = new Program();
        p.SearchMeneger();
        this.startTimer();
    }
}

