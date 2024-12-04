public class GuideLog{
    private int ID;
    private string guideText;
    private string descText;

    public GuideLog(int _ID, string _guideText, string _descText){
        this.ID = _ID;
        this.guideText = _guideText;
        this.descText = _descText;
    }

    public int GetID(){
        return this.ID;
    }

    public string GetGuideText(){
        return this.guideText;
    }

    public string GetDescText(){
        return this.descText;
    }
}
