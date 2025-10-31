using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class CakeCardPlaceholder : MonoBehaviour
{
    public GameObject cakeCardPrefab;
    private GameObject cakeCard;
    private CakeMetaData cakeMetaData;

    private Sprite sprite;
    private string id;
    private string displayName;
    private string path;
    public Image imageUI;
    public TextMeshProUGUI displayNameUI;
    private OpeningReadyEventManager openingReadyEventManager;

    public void Initialize(CakeMetaData data)
    {
        // 상위 객체에서 메타데이터를 받아 초기화함.
        cakeMetaData = data;
    }
    void Start()
    {
        cakeCard = null;

        // 이름 지정
        displayName = cakeMetaData.displayName;
        // 이미지 로드
        byte[] imageByte = File.ReadAllBytes(cakeMetaData.imagePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageByte);

        sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));

        // 케이크 경로 지정
        path = cakeMetaData.cakePath;
        // id획득
        id = cakeMetaData.id;

        // 이벤트 구독
        openingReadyEventManager = OpeningReadyEventManager.Instance;
        openingReadyEventManager.OnCloneNewCard += CloneNewCard;

        // 자신의 이미지 초기화
        imageUI.sprite = sprite;
        displayNameUI.text = displayName;

        // 시작시 카드 생성
        CloneNewCard(id);
    }

    void CloneNewCard(string id)
    {
        if (cakeCard == null && string.Equals(this.id, id))
        {
            cakeCard = Instantiate(cakeCardPrefab, transform);
            cakeCard.GetComponent<CakeCard>().Initialize(id, displayName, sprite, path, this);
        }
    }

    public void UnchainCard()
    {
        cakeCard = null;
    }
}
