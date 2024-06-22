using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabManager 
{
    #region Rooms
    public Dictionary<string, Classroom> classDic = new Dictionary<string, Classroom>();
    //public Dictionary<string, Teacherroom> teacherroomDic = new Dictionary<string, Teacherroom>();
    #endregion

    #region Items
    public Dictionary<string, DecisionSpawnItem> itemDic =new Dictionary<string, DecisionSpawnItem>(); 
    #endregion
}
