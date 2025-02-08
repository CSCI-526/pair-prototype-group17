using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDebugDraw : MonoBehaviour
{
    public Player player;
    [SerializeField] private TMP_Text textElement;
    public string prefix;
    // Start is called before the first frame update
    void Start()
    {
        textElement.text = prefix;
    }

    // Update is called once per frame
    void Update()
    {
        textElement.text = prefix + ":" + player.stateMachine.currentState.animBoolName;
    }
}
