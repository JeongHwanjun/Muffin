using System;
using System.Collections.Generic;
using UnityEngine;

public class ManufactureAdmin : MonoBehaviour
{
    public OpeningTimeManager openingTimeManager;

    public static event Action<ManufactureAdmin> OnManufactureAdminReady;

    public void Initialize(OpeningTimeManager _openingTimeManager){
        // 상위 객체(OpeningTimeManager)에서 데이터를 받아와 이에 맞게 화면 초기화
        //openingTimeManager = _openingTimeManager;

        // ScreenSwapper에 준비됐다고 알림
        OnManufactureAdminReady?.Invoke(this);
    }
}
