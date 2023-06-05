using System.Collections.Generic;

[System.Serializable]
public class EnviromentData
{
    public List<string> pickedUpItems;
    public List<string> npcDespawned;
    public EnviromentData(List<string> _pickedUpItems, List<string> _npcDespawned) 
    {
        pickedUpItems= _pickedUpItems;
        npcDespawned = _npcDespawned;
    }
}