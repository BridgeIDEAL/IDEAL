[System.Serializable]
public class RoomArchiveLog
{
    public int ID;
    public int attempt;

    public RoomArchiveLog(int _ID,  int _attempt){
        this.ID = _ID;
        this.attempt = _attempt;
    }

    public int GetID(){
        return this.ID;
    }
    
    public int GetAttempt(){
        return this.attempt;
    }

    public void SetAttempt(int _attempt){
        this.attempt = _attempt;
    }
}
