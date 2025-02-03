using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

/* Merge Sort 분할 정복 알고리즘 */ 
public class MergeSort : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<int> numbers = new List<int> { 5, 3, 8, 6, 2, 7, 4, 1 };
        numbers = MergeSortAl(numbers);

        string strNumbers = "";
        foreach (int a in numbers)
        {
            strNumbers += a + ",";
        }
        Debug.Log(strNumbers);
    }
    
    List<int> MergeSortAl(List<int> list)
    {
        // 리스트 크기가 1 이하라면 이미 정렬된 상태이므로 반환한다.
        if (list.Count <= 1)
        {
            return list;
        }

        int mid = list.Count / 2;
            
        // 왼쪽
        List<int> left = list.GetRange(0, mid);
        // 오른쪽
        List<int> right = list.GetRange(mid, list.Count - mid);

        left = MergeSortAl(left);
        right = MergeSortAl(right);

        return Merge(left, right);
    }

    List<int> Merge(List<int> left, List<int> right)
    {
        List<int> result = new List<int>(); // 병합된 결과를 기억할 리스트
        int i = 0;
        int j = 0;
        
        // 두 리스트를 비교하여 작은 값을 결과 리스트에 추가한다.
        while (i < left.Count && j < right.Count)
        {
            if (left[i] < right[j])
            {
                result.Add(left[i++]); // 왼쪽 리스트에 값 추가 후 인덱스 증가
            }
            else
            {
                result.Add(right[j++]); // 오른쪽 리스트 값 추가 후 인덱스 증가
            }
        }

        while (i < left.Count)
        {
            result.Add(left[i++]);
        }

        while (j < right.Count)
        {
            result.Add(right[j++]);
        }

        return result;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
