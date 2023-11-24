using System.Collections.Generic;

public class EntityDataBase 
{
    static readonly EntityDataBase instance = new EntityDataBase();
    public static EntityDataBase Instance => instance;
    private Dictionary<int, BaseEntity> entityDictionary;
    public void SetUp()
    {
        entityDictionary = new Dictionary<int, BaseEntity>();
    }

    public void RegisterEntity(BaseEntity entity)
    {
        entityDictionary.Add(entity.ID, entity);
    }

    public void RemoveEntity(int id)
    {
        entityDictionary.Remove(id);
    }

    public Dictionary<int, BaseEntity> GetDictionary()
    {
        return entityDictionary;
    }
    
    public BaseEntity GetEntity(int id)
    {
        foreach(KeyValuePair<int,BaseEntity> entity in entityDictionary)
        {
            if (entity.Key == id)
            {
                return entity.Value;
            }
        }
        return null;
    }
}
