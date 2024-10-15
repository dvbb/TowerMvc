using UnityEngine;

public class ResourcesLoadTool : Singleton<ResourcesLoadTool>
{
    public T ResourceLoadObject<T>(object obj) where T : Object
    {
        string currentName = obj.GetType().Name;
        string filePath = string.Empty;
        switch (currentName)
        {
            case "MusicType_Main":
                filePath = "Music/Main/" + obj.ToString();
                break;
            case "MusicType_Items":
                filePath = "Music/Items/" + obj.ToString();
                break;
            case "Monster":
                filePath = "Prefabs/Monster/" + obj.ToString();
                break;
            case "Bullet":
                filePath = "Prefabs/Bullet/" + obj.ToString();
                break;
            default:
                break;
        }

        return Resources.Load(filePath) as T;
    }
}
