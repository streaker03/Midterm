using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Tower Defense/Enemy")]
public class Enemy : ScriptableObject {
    public string title;
    public GameObject model;
    public Mesh mesh;
    public float speed;
    public float maxHealth;
    public int cost;
    public int points;
    public int money;
}
