using System.Collections.Generic;
using UnityEngine;

public class CakeCardContainer : MonoBehaviour
{
    public GameObject newCakeCardPlaceholderPrefab;
    void Start()
    {
        // 1. 메타데이터를 읽어옴
        List<CakeMetaData> metaDatas = MetadataReader.ReadMetaData();

        // 2. 메타데이터를 순회하며 해당 정보를 기반으로 카드를 생성함
        foreach(CakeMetaData metaData in metaDatas)
        {
            GameObject newCakeCard = Instantiate(newCakeCardPlaceholderPrefab, transform);
            // 3. 생성한 아이템을 초기화함
            newCakeCard.GetComponent<CakeCardPlaceholder>().Initialize(metaData);
        }
        
    }
}
