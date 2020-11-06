using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Tower Defense/Turret")]
public class Turret : ScriptableObject {
   public string title;
   public int cost;
   public GameObject prefab;
}
