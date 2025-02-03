using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeInteraction : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public Text selectedOptionText;

    private float buttonSpacing = 200f;
    private Button btn;

    public GameObject sphere;
    public GameObject feet;
    
    private bool isShowingFadeOut = false;
    private float fadeOutTime = 2f;

    public IEnumerator movingToFeetCorutine;
    
    private void MyMethod() // 반환도 없고 인자도 없는 메서드
    {
        Debug.Log("버튼 눌림");
    }

    private void OnCollisionEnter(Collision other)
    {
        // 플레이어와 충돌 체크
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Tree"))
        {
            ShowChoices("부딛힌!!",new List<string> {"나무 조사하기", "열매 따기", "그냥 가기"});
        }
        else
        {
            ShowChoices("SIUUUUUUU",new List<string>{"SIUUUUUUUU"});            
        }
    }

    private void TriggerAction()
    {
        if (gameObject.name.Equals("Christiano"))
        {
            ShowChoices("신두형을 만남!!", new List<string> {"신두형에게 패스하기", "그냥 가기"});
        }
        else
        {
            ShowChoices("가까운 나무!!" ,new List<string> {"갈길 가기"});            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // AddListener 는 유니티 버튼에서 사용되는 메서드
        // 버튼 클릭 시, 호출될 메서드를 등록하는 기능
        // 버튼 이벤트가 발생 시, 수행될 메서드를 "바인딩" 한다고 표현
        // 다시 말해 버튼과 메서드가 연결된다.
        //btn.onClick.AddListener(MyMethod);
        
        // 미리 만드는게 아니라, 런타임에 버튼의 기능을 바인딩한다. => 동적 바인딩
        // 유연한 코드 작성 가능하게 한다.
        
        //btn.onClick.RemoveListener();
        movingToFeetCorutine = PassToFeet();
    }

    void ShowChoices(string title, List<string> choices)
    {
        DestroyButtons();
        selectedOptionText.text = title;
        
        for (int i = 0; i < choices.Count; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            newButton.GetComponentInChildren<Text>().text = choices[i];
            newButton.tag = gameObject.tag;
            // 무조건 i로 존재하면 안될듯
            // 방법 1 - i를 temp로 한번 캡쳐해두기
            float executedTime = fadeOutTime;
            // 방법 2 - 어짜피 텍스트 가져와야하니까 텍스트를 캡쳐하기
            string choiceText = "선택! : " + choices[i];
            
            newButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                // 이건 왜 안될까요?!
                // int temp = i;
                OnChoiceSelected(choiceText, executedTime);

                if (choiceText.Equals("선택! : 신두형에게 패스하기"))
                {
                    PassToChristiano();
                }
            });
            
            RectTransform buttonRect = newButton.GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(0, -i * buttonSpacing);
        }
    }
    
    void OnChoiceSelected(string choice, float executedTime)
    {
        selectedOptionText.text = choice;
        isShowingFadeOut = true;
        StartCoroutine(FadeOut());
    }
    
    void DefaultAction()
    {
        Debug.Log("기본 동작");
    }

    void ChangedAction()
    {
        Debug.Log("변경된 동작");
    }    
    
    public void ChangeAction()
    {
        btn.onClick.AddListener(DefaultAction);
        btn.onClick.RemoveListener(ChangedAction);
    }

    public void DestroyButtons()
    {
        foreach (Transform child in buttonContainer)
        {
            if (!gameObject.CompareTag(child.tag))
            {
                break;
            }
            else
            {
                Destroy(child.gameObject);                
            }
        }
    }
    
    // Update is called once per frame
    // triggerFlag가 true 일때만 실행하기
    void FixedUpdate()
    {
        float distance = Vector3.Distance(gameObject.transform.position, sphere.gameObject.transform.position);

        if (isShowingFadeOut)
        {
            return;
        }
        
        // 거리가 5 이내인데, 버튼이 없다.
        if (distance <= 5 && buttonContainer.childCount == 0)
        {
            TriggerAction();
        }
        
        // 거리가 5보다 큰데, 버튼이 있다.
        // 버튼이 2가지라 계속 DestroyButtons가 호출 될거
        if (distance > 5 && buttonContainer.childCount > 0)
        {
            DestroyButtons();
        }
    }
    
    IEnumerator FadeOut()
    {
        DestroyButtons();
        Color c = selectedOptionText.color;
        for (float alpha = 1f; alpha >=0; alpha -= 0.05f)
        {
            c.a = alpha;
            selectedOptionText.color = c;
            yield return new WaitForSeconds(.1f);
        }
        
        isShowingFadeOut = false;
        ResetText();
    }
    
    private void PassToChristiano()
    {
        StartCoroutine(movingToFeetCorutine);
    }

    public IEnumerator PassToFeet()
    {
        Rigidbody rigid = sphere.GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.up * 10);
        for (int i = 0; i < 20; i++)
        {
            rigid.AddForce((feet.transform.position - sphere.transform.position) * 10);            
        }
        
        
        yield return new WaitForSeconds(.1f);
    }
    
    private void ResetText()
    {
        Color c = selectedOptionText.color;
        c.a = 1f;
        
        selectedOptionText.text = "";
        selectedOptionText.color = c;
    }
}