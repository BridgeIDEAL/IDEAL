public class ArchiveLog{
    private int attempt;
    private string state;
    private string archiveText;

    public ArchiveLog(int _attempt, string _state, string _archiveText){
        this.attempt = _attempt;
        this.state = _state;
        this.archiveText = _archiveText;
    }

    public int GetAttempt(){
        return this.attempt;
    }

    public string GetState(){
        return this.state;
    }

    public string GetArchiveText(){
        return this.archiveText;
    }
}
