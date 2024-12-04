using System;

[Serializable]
public class ArchiveLog{
    public int attempt;
    public string state;
    public string archiveText;

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
