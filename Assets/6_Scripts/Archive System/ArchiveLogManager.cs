using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[SerializeField]
public class PlayerArchiveData{
    public List<ArchiveLog> archiveLogRecordList = new List<ArchiveLog>();
}

public class ArchiveLogManager : MonoBehaviour
{
    private static ArchiveLogManager instance = null;
    public static ArchiveLogManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    private string archiveDataPath;

    public PlayerArchiveData playerArchiveData;

    // UI 선언 필요

    private string[] archiveTexts = {
        "Missing person $attempts was wandering the halls, \nran into the principal and lost contact. \n\nNo trace  was found afterward, except for a stack of \nblood-soaked books in the corner of a study hall.",
        "Missing person $attempts repeated, \n“Where did he come from ... \nThere was definitely no one in the Student Center...” \nand then lost signal. \n\nCondition is still unknown.",
        "Missing person $attempts lost signal after an encounter with \na girl whose neck was bent backwards. \n\nA headless, spine-bent body was later found, \nbelieved to be the missing person.",
        "Missing Person $attempts lost signal right after picking up \nkeys from the Student Center.  \n\nThe barely reconstructed recordings reveal \nno trace of the missing person, \nexcept for the screams of an unknown female.",
        "Missing person $attempts lost signal while trying to enter the \nComputer Room by ignoring the computer lab's ■■, \nthen suddenly said he wanted to watch a movie.\n\nNo trace of the missing person found afterward, \nother than one more computer turned on.",
        "Missing person $attempts lost signal after entering \nthe classroom and repeating that there was \nsomething on the board. \n\nThe missing person's name was written on \nthe board from an afterward record.",
        "Missing Person $attempts suddenly reported \nsound of someone running toward them \nand the signal was cut off.\n\nLater investigation reveals the missing \nperson's body parts, which appeared to have been \nhit by something like a fast-moving truck.",
        "Missing person $attempts was complaining of \nexcruciating pain in his eye, \nand the signal was finally cut off \nwhen he gouged out his own eye.",
        "Missing person $attempts took the pill given at \nthe beginning of the entry and then \nthe signal was cut off with a sound \nas if someone was talking to the missing person.",
        "Missing person $attempts escaped Jain High School \nsuccessfully through a suddenly created main exit.\n\nFurther investigation is needed into the main exit \nand the environment inside Jain High School.",
        "Missing person $attempts has been lost \nsince the bell started ringing. \n\nFurther investigation into the bell ringing and \nchanges in the school's internal environment is needed."
    };

    private string[] archiveStates = {
        "Death",
        "Missing",
        "Death",
        "Missing",
        "Missing",
        "Missing",
        "Death",
        "Missing",
        "Death",
        "Survival",
        "Missing",
    };

    private void Awake(){
        if(Instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }

        archiveDataPath = Path.Combine(Application.persistentDataPath, "PlayerArchiveLogData.json");
    }

    private void Start(){
        if (File.Exists(archiveDataPath)){
            LoadArchiveData();
        }
        else{
            playerArchiveData = new PlayerArchiveData();
            SaveArchiveData();
        }

    }

    // 테스트 코드
    private int cnt = 1;
    private void Update(){
        // if(Input.GetKeyDown(KeyCode.P)){
        //     ArchiveLog arc = new ArchiveLog(cnt, archiveStates[cnt-1], archiveTexts[cnt-1]);
        //     AddArchiveLog(arc);
        //     cnt++;
        // }
    }



    private void LoadArchiveData(){
        string loadJson = File.ReadAllText(archiveDataPath);
        playerArchiveData = JsonUtility.FromJson<PlayerArchiveData>(loadJson);
    }

    public void SaveArchiveData(){
        string json = JsonUtility.ToJson(playerArchiveData, true);
        File.WriteAllText(archiveDataPath, json);
    }

    public string GetArchiveText(int index){
        return archiveTexts[index];
    }

    public string GetArchiveState(int index){
        return archiveStates[index];
    }

    public void AddArchiveLog(ArchiveLog archiveLog){
        playerArchiveData.archiveLogRecordList.Add(archiveLog);
        SaveArchiveData();
    }
}
