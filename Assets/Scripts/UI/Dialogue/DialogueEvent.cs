using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    #region Get & Use Item
    public void GetItem(List<string> _parameterList)
    {
        Debug.Log(_parameterList[0]);
        if (!FabManager.Instance.IsInItemDataDictionary(_parameterList[0]))
        {
            Debug.LogError("�����ϴ�");
            return;
        }
        InteractionItemData _itemData = FabManager.Instance.LoadInteractionItemData(_parameterList[0]);
        if (_itemData == null)
            return;
        Inventory.Instance.Add(_itemData, 1);
    }

    public void UseItem(List<string> _parameterList)
    {
        int _itemCode = int.Parse(_parameterList[0]);
        Inventory.Instance.UseItemWithItemCode(_itemCode);
    }

    public void SpawnItem(List<string> _parameterList)
    {
        GameObject _spawnItem = FabManager.Instance.LoadPrefab(_parameterList[0]);
        Instantiate(_spawnItem);
    }
    #endregion

    #region Dialogue Index
    public void UnableCommunicate(List<string> _parameterList)
    {
        // DialogueManager.Instance.CurrentTalkEntity�� ���� Interaction ��ũ��Ʈ ���� ��, �� �̻� ��ȭ���� ���ϵ��� ���� 
        InteractionConditionConversation conversation = DialogueManager.Instance.CurrentTalkEntity.GetComponent<InteractionConditionConversation>();
        conversation.ChangeIndex(-1);
    }

    public void DialogueIndexChange(List<string> _parameterList)
    {
        int nextIndex = int.Parse(_parameterList[0]);
        InteractionConditionConversation conversation = DialogueManager.Instance.CurrentTalkEntity.GetComponent<InteractionConditionConversation>();
        conversation.ChangeIndex(nextIndex);
    }
    #endregion

    #region Hurt & Heal Method
    public void Damaged(List<string> _parameterList)
    {
        int _damage = int.Parse(_parameterList[1]);
        switch (_parameterList[0])
        {
            case "LeftArm":
                Hurt(IdealBodyPart.LeftArm, _damage);
                break;
            case "RightArm":
                Hurt(IdealBodyPart.RightArm, _damage);
                break;
            case "LeftLeg":
                Hurt(IdealBodyPart.LeftLeg, _damage);
                break;
            case "RightLeg":
                Hurt(IdealBodyPart.RightLeg, _damage);
                break;
            // Later
            case "RightHand":
                Hurt(IdealBodyPart.RightArm, _damage);
                break;
        }
    }

    public void Hurt(IdealBodyPart _bodyPart, int _damage)
    {
        if (HealthPointManager.Instance.GetHealthPoint(_bodyPart) > HealthPointManager.minHP)
            HealthPointManager.Instance.Hurt(_bodyPart, _damage);
        else
            HealthPointManager.Instance.Hurt(_bodyPart, _damage);
    }

    public void Heal(IdealBodyPart _bodyPart, int _damage)
    {
        if (HealthPointManager.Instance.GetHealthPoint(_bodyPart) < HealthPointManager.maxHP)
            HealthPointManager.Instance.Heal(_bodyPart, _damage);
        else
            HealthPointManager.Instance.Heal(_bodyPart, _damage);
    }
    #endregion

    #region Entity Event
    public void SpawnEntity(List<string> _parameterList)
    {
        string _name = _parameterList[0];
        EntityDataManager.Instance.Controller.ActiveEntity(_name);
    }

    public void EntityAnimationTrigger(List<string> _parameterList)
    {
        BaseEntity _baseEntity = EntityDataManager.Instance.Controller.GetEntity(_parameterList[0]);
        _baseEntity.EntityAnimationTrigger(_parameterList[1]);
    }
    #endregion
}
