using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmproComponent;
    [SerializeField] string[] _dialogue;
    [SerializeField] private float _textSpeed;
    [SerializeField] private PlayerController _player;
    private int currentIndex;
    // Start is called before the first frame update

    private PlayerInputs _inputs;
    private void Awake()
    {
        _inputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        _inputs.Player.Jump.performed += HandleDialoguePress;
        _inputs.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        _inputs.Player.Jump.Disable();
    }

    void Start()
    {
        tmproComponent.text = string.Empty;
    }

    void HandleDialoguePress(InputAction.CallbackContext obj)
    {
        if (tmproComponent.text == _dialogue[currentIndex])
        {
            NextDialogue();
        }
        else
        { // Don't wait and fill all the text
            StopAllCoroutines();
            tmproComponent.text = _dialogue[currentIndex];
        }
    }

    public void StartDialogue()
    {
        gameObject.SetActive(true);
        currentIndex = 0;
        _player.TakeAwayControl();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in _dialogue[currentIndex].ToCharArray())
        {
            tmproComponent.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }

    void NextDialogue()
    {
        if (currentIndex < _dialogue.Length - 1)
        {
            currentIndex++;
            tmproComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            _player.ReturnControl();
            gameObject.SetActive(false);
        }
    }
}
