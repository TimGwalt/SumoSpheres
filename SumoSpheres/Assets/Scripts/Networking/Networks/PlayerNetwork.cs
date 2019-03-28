using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork m_Instance;
    public string m_Name {get; private set;}

    void Awake()
    {
        m_Instance = this;

        m_Name = "Player#" + Random.Range(1000, 9999);
    }
}
