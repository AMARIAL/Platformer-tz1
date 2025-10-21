using System.Collections.Generic;
using UnityEngine;

public class Containers : MonoBehaviour
{
    public static Containers ST {get; private set;}
    
    public Dictionary<GameObject, Health> healthContainer = new Dictionary<GameObject, Health>();
    public Dictionary<GameObject, Healer> healerContainer = new Dictionary<GameObject, Healer>();
    public Dictionary<GameObject, CheckPoint> checkPointContainer = new Dictionary<GameObject, CheckPoint>();
    public Dictionary<GameObject, Chest> chestContainer = new Dictionary<GameObject, Chest>();
    private void Awake()
    {
        ST = this;
    }
}
