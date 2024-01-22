# 모코코씨앗을 찾아서

유니티 부트캠프에서 진행한 2인 개발 프로젝트입니다.
'로스트아크'게임의 마스코트인 모코코를 주인공으로 설정한 간단한 2D 횡스크롤 게임입니다.
플레이어는 총3개의 목숨을 지니며 맵 끝에 배치된 모코코씨앗을 얻으면 게임에서 승리할 수 있습니다.

## 목차
- [개요](#개요)  
- [게임설명](#게임설명)
- [게임플레이](#게임플레이)  

## 개요
- 프로젝트 이름 : 모코코씨앗을 찾아서
- 프로젝트 개발기간 : 2021.11 - 2021.11
- 개발 엔진 및 언어 : Unity & C#
- 기타 프로그램 : Spine
- 맴버 : 남기산 , 조지환

## 게임설명
|![image](https://github.com/Gongju-Unity-Bootcamp/Noname/assets/148856359/d1e03f5f-179c-40f1-afe0-fdb6f6116211)|
|---|
|<p align="center">게임화면|  

1달의 기간동안 개발한 간단한 2D횡스크롤 게임입니다.  
플레이어는 눈앞의 적들을 물리치며 앞으로 나아가야 하며 골인 지점에 도착하면 게임이 종료됩니다.  

### 게임 규칙
|![KakaoTalk_20240122_224753238](https://github.com/Gongju-Unity-Bootcamp/Noname/assets/148856359/4cd30b81-e995-467d-a36f-1f5cd690fe67)|![KakaoTalk_20240122_224813093](https://github.com/Gongju-Unity-Bootcamp/Noname/assets/148856359/42046d37-5b5e-4a8a-9a5f-4f759c1e21c9)|![KakaoTalk_20240122_224832967](https://github.com/Gongju-Unity-Bootcamp/Noname/assets/148856359/2051129c-eb83-4366-a52a-35e2f45f24a2)|![KakaoTalk_20240122_224851628](https://github.com/Gongju-Unity-Bootcamp/Noname/assets/148856359/5c2f9ed0-71b8-492e-955e-9cac07eb2a08)|
|---|---|---|---|
|<p align="center">플레이어 조작|<p align="center">게임클리어|<p align="center">게임오버조건1|<p align="center">게임오버조건2|  

- **플레이어 조작**
1. 플레이어는 좌/우 화살표, A/D 키보드 버튼을 이용하여 x축으로 이동할 수 있다.  
2. 플레이어는 스페이스바를 눌러 공중으로 점프할 수 있다.  
   2-1. 플레이어는 공중에서 공격을 할 수 있다.  
3. 플레이어는 Z 키보드 버튼을 통해 현재 바라보고 있는 방향으로 근접공격을 할 수 있다.

- **게임클리어**
1. 플레이어는 맵 끝에 존재하는 모코코씨앗 오브젝트를 획득하면 게임에서 승리한다. 

- **게임오버조건1**
1. 플레이어가 맵에 존재하는 낭떠러지에 추락한다.  
2. 플레이어가 화면 밖까지 추락하면 플레이어의 체력을 1 감소시킨다.
3. 만약 플레이어의 체력이 0이면 게임을 종료 시킨다.  

- **게임오버조건2**
1. 플레이어가 적이나 데미지가 존재하는 장애물에 충돌한다.  
2. 플레이어의 체력을 1 감소시키고 약간의 무적시간을 부여한다.  
3. 만약 플레이어의 체력이 0이면 게임을 종료 시킨다.  

## 게임플레이
- **플레이어 조작**
  
|이동방향|좌(LEFT)|우(RIGHT)|공격(ATTACK)|
|---|---|---|---|
|<p align="center">키보드|<p align="center">A|<p align="center">D|<p align="center">Z|

- **플레이 영상**
  
https://github.com/Gongju-Unity-Bootcamp/Noname/assets/148856359/480dc8a3-3ce8-4d42-8aeb-32ae52d24ec4





  
