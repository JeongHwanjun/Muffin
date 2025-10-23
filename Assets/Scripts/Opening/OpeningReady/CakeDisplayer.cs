using System.Collections.Generic;
using UnityEngine;

public class CakeDisplayer : MonoBehaviour
{
    public GameObject cakeDisplayPrefab;
    void Start()
    {
        // 1. 메타데이터를 읽어옴
        List<CakeMetaData> metaDatas = MetadataReader.ReadMetaData();

        // 2. 메타데이터를 순회하며 해당 정보를 기반으로 드래그 가능한 아이템을 생성함
        foreach(CakeMetaData metaData in metaDatas)
        {
            GameObject newCakeDisplay = Instantiate(cakeDisplayPrefab, transform);
            // 3. 생성한 아이템을 초기화함
            newCakeDisplay.GetComponent<CakeCard>().Initialize(metaData);
        }
        
    }
}
