using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateTest : MonoBehaviour
{
    private int myVal = 12;
    private Func<int> getVal;

    public delegate void MyDel(); // 델리게이트 설계도, 인자, 반환값 없음
    public MyDel OnRunning; // 그거 가지고 델리게이트 객체 생성

    private List<Action> actions = new List<Action>();
    
    // Start is called before the first frame update
    void Start()
    {
        getVal = () => myVal; // 람다식에서 외부 범위의 변수를 사용하는 것
        
        // 클로져 : 캡쳐된 변수와 해당 변수를 사용하는 함수를 묶어서 저장하는 방식

        int a = 0; // 지역변수 였던것

        OnRunning = () =>
        {
            a++;
            Debug.Log(a);
        };

        for (int i = 0; i < 3; i++)
        {
            actions.Add(() => Debug.Log(i));
        }        

        // 출력결과? -> 333
        foreach (var action in actions)
        {
            action();
        }
    }
    
    

    // Update is called once per frame
    void Update()
    {
        // 외부 변수 캡쳐
        // Debug.Log(getVal());

        // a 가 없어도 계속 출력된다.
        // OnRunning?.Invoke();
    }
}
