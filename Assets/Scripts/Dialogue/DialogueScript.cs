using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI dialogueOptionOneText;
    [SerializeField] private TextMeshProUGUI dialogueOptionTwoText;

    [SerializeField] private float typeWriterDelay = 0.1f;
    [SerializeField] private string dialogueOptionOne;
    [SerializeField] private string dialogueOptionTwo;
    [SerializeField] private TPSCharacter player;
    [SerializeField] private Transform focus;

    [SerializeField] private bool rebindOldFocus;
    [SerializeField] private Button optionOneButton;
    [SerializeField] private Button optionTwoButton;
    
    private string currentText;
    private static readonly int _STalking = Animator.StringToHash("Talking");
    private DialogueOption _dialogueOption;

    public void NextDialogue(GameObject dialogueOption)
    {
        dialogueBox.SetActive(true);
        _dialogueOption = dialogueOption.GetComponent<DialogueOption>();

        HandleFocus();

        if (player)
        {
            player.SetControlInactive();
            player.GetComponent<Animator>().Rebind();
        }

        dialogueText.text = "";
        dialogueOptionOneText.text = "";
        dialogueOptionTwoText.text = "";

        _dialogueOption.GetComponent<DialogueOption>();
        dialogueOptionOne = _dialogueOption.firstOption;
        dialogueOptionTwo = _dialogueOption.secondOption;
        currentText = _dialogueOption.text;
        ShowDialogue(currentText);

        if (player)
        {
            var focusDirection = (focus.transform.position - player.transform.position).normalized;
            var targetFocusDirection = Quaternion.LookRotation(new Vector3(focusDirection.x, 0, focusDirection.z));
            player.transform.rotation = targetFocusDirection;
        }

        optionOneButton.onClick.RemoveAllListeners();
        optionOneButton.onClick.AddListener(() => { _dialogueOption.firstOptionEvent.Invoke(); });

        optionTwoButton.onClick.RemoveAllListeners();
        optionTwoButton.onClick.AddListener(() => { _dialogueOption.secondOptionEvent.Invoke(); });
        if (player) player.SetControlInactive();
    }

    private void HandleFocus()
    {
        if (focus)
        {
            var oldFocus = focus.GetComponentInParent<Animator>();
            if (oldFocus)
            {
                oldFocus.SetBool(_STalking, false);
                if (rebindOldFocus) oldFocus.Rebind();
            }
        }

        if (_dialogueOption.focus) focus = _dialogueOption.focus;
        if (focus)
        {
            var focusAnim = focus.GetComponentInParent<Animator>();
            if (focusAnim) focusAnim.SetBool(_STalking, true);
        }
    }

    public void ShowDialogue(string text)
    {
        dialogueBox.SetActive(true);
        currentText = text;
        StartCoroutine(TypeWriteText());
    }

    private IEnumerator TypeWriteText()
    {
        while (dialogueText.text != currentText)
        {
            dialogueText.text += currentText[dialogueText.text.Length];
            yield return new WaitForSeconds(typeWriterDelay);
        }

        dialogueOptionOneText.text = dialogueOptionOne;
        dialogueOptionTwoText.text = dialogueOptionTwo;
    }
}