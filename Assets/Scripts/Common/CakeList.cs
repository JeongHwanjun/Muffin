using System;
using System.Collections.Generic;

// Cake를 직렬화시켜 json으로 보관하기 위한 클래스
[Serializable]
class CakeList {
  public List<Cake> cakes;
}