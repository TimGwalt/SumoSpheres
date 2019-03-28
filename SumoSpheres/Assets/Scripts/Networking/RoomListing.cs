using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_RoomNameText;
    private TextMeshProUGUI RoomNameText
    {
        get { return m_RoomNameText; }
    }
    public string m_RoomName { get; private set; }

    public bool m_Updated { get; set; }

    void Start()
    {
        GameObject lobbyCanvasObject = JoinCanvasManager.m_Instance.LobbyCanvas.gameObject;
        if (lobbyCanvasObject == null)
            return;
        
        LobbyCanvas lobbyCanvas = lobbyCanvasObject.GetComponent<LobbyCanvas>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => lobbyCanvas.OnClickJoinRoom(RoomNameText.text));
    }

    void OnDestroy() {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void SetRoomNameText(string text)
    {
        m_RoomName = text;
        RoomNameText.text = m_RoomName;
    }
}
