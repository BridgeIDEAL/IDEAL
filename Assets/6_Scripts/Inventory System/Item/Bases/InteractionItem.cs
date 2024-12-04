using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionItem : CountableItem, IUsableItem, INotUseInventoryUI
{
    public InteractionItem(InteractionItemData data, int amount = 1) : base(data, amount) { }

    public bool Use(){
        // 개수 감소    Interaction 작동은 호출 부분에서 처리
        Amount--;
        
        return true;
    }

    protected override CountableItem Clone(int amount)
    {
        return new InteractionItem(CountableData as InteractionItemData, amount);
    }
}
