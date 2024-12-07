
using UnityEngine;
using UnityEngine.UI;

public class UITitle : MonoBehaviour
{
    [SerializeField] private Image titleImage;
    [SerializeField] private Color redColor;
    [SerializeField] private GameObject timeCapsuleObject;
    [SerializeField] private GameObject closebookObject;
    private bool canShowTimeCapsule = false;

    void Awake(){
        if(IdealSceneManager.Instance !=null && IdealSceneManager.Instance.isClearLobby){
            titleImage.color = redColor;
            canShowTimeCapsule = true;
        }
    }

    public void ShowTimeCapsule(){
        if(canShowTimeCapsule){
            timeCapsuleObject.SetActive(true);
            timeCapsuleObject.GetComponent<UITimeCapsule>().ShowTimeDesk();
            closebookObject.SetActive(true);
        }
        
    }
}
