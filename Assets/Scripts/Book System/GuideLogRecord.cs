using System;

public class GuideLogRecord : IComparable<GuideLogRecord>
{
    private int guideLogID;
    private int attempt;

    public GuideLogRecord(int _guideLogID, int _attempt){
        this.guideLogID = _guideLogID;
        this.attempt = _attempt;
    }

    public int GetGuideLogID(){
        return this.guideLogID;
    }

    public int GetAttempt(){
        return this.attempt;
    }

    // IComparable<T> 인터페이스를 구현하여 정렬 기준 제공
    public int CompareTo(GuideLogRecord other){
        // guideLogID를 먼저 비교
        int compareGuideLogID = this.guideLogID.CompareTo(other.guideLogID);

        if (compareGuideLogID != 0) {
            return compareGuideLogID;
        }

        // guideLogID가 같으면 attempt를 비교
        return this.attempt.CompareTo(other.attempt);
    }
}
