게임엔진1 최종 프로젝트
엔터테인먼트컴퓨팅 2015184025 정관영

<게임 설명>
쫒아오는 좀비들을 피해서 수정을 먹는 게임. 수정을 다 먹으면 승리. 수정을 다 먹기전에 좀비와 닿으면 패배.
수정을 먹어 점수가 증가함에 따라 적이 생성된다. 스테미너 시스템이 있어 연속으로 달리면 탈진 상태에 빠져 달릴 수 없다.
달리고 걷는 것을 잘 조절하여 탈진상태에 빠지는 것을 막아야 한다. 탈진 상태에 걸리면 5초동안 달릴 수 없다.
파란 수정은 1점 상승, 황금 수정은 10점 상승, 보라 수정은 10초 동안 탈진 상태 방지, 붉은 수정은 10초 동안 모든 몬스터 정지.
좀비는 유저의 발소리에 민감하게 반응하니 주변에 좀비가 있다면 뛰지 말자!

<단축키>
방향키 : 전진, 후진, 좌, 우 이동
LeftShift 누를 시 : 달리기(스테미너 감소)
Esc : 메뉴

<외부 라이브러리 리스트>
RPG_FPS_game_assets_industrial
-> 공장 분위기의 모델 에셋 모음.
-> 맵의 대부분을 이 에셋으로 만듦.
https://assetstore.unity.com/packages/3d/environments/industrial/rpg-fps-game-assets-for-pc-mobile-industrial-set-v2-0-86679

ZombieAnimationPackFree
-> 좀비 모델 애니메이션 에셋.
-> 몬스터 모델 애니메이션 사용.
https://assetstore.unity.com/packages/3d/animations/zombie-animation-pack-free-150219

Kevin Iglesias
-> 모델 애니메이션 에셋.
-> 플레이어 모델 애니메이션 사용.
https://assetstore.unity.com/packages/3d/animations/basic-motions-free-pack-154271

SineVFX
-> 수정 모델 에셋.
-> 점수나 아이템 모델로 사용.
https://assetstore.unity.com/packages/3d/environments/fantasy/translucent-crystals-106274

footstep
-> 좀비나 플레이어 발소리 모음.

zombie sound
-> 좀비가 내는 소리 모음.

<C# 스크립트 리스트>
FirstPersonController.cs
MonsterController.cs
GameManager.cs
NoExhaustionItem.cs
PauseMenu.cs
ResultBoard.cs
ScoreUI.cs
ScoreUpItem.cs
StartButtonScript.cs
SteminaBar.cs
StopMonsterItem.cs

모든 C# 스크립트는 자체 제작했습니다.

<적용된 기술>
적의 경로탐색 기술(플레이어 발소리와 플레이어 위치를 정보로 이용)
-> 적 모델에 Nav Mesh Agent 컴포넌트를 추가하여 베이크된 경로를 돌아다녀 최선의 경로를 탐색하도록 함.

플레이어 발소리와 적의 충돌처리
-> 수업시간에 배운 Trigger 활용
-> 플레이어에 Sphere Collider 컴포넌트를 추가한 후 is Trigger 체크. 
-> OnTriggerStay 함수를 이용하여 발소리 Collider 안에 적이 있는 동안은 적이 플레이어를 추적.

플레이어와 적의 충돌처리
-> OnControllerColliderHit 함수를 이용하여 적과의 충돌 감지.
-> 충돌 시 게임 종료 선언.

플레이어와 적의 발소리 삽입
-> 사운드클립에 플레이어와 적의 발소리를 삽입.
-> 오디오소스의 Play(), Pause()를 이용해 달릴 때, 걸을 때의 발소리를 다르게 함.

1인칭 카메라 워킹
-> 수업시간에 배운 Input.GetAxis() 활용.
-> Input.GetAxis("Mouse X")와 Input.GetAxis("Mouse Y")를 이용하여 마우스 이동에 따른 시야 변경.
-> 좌우 이동은 플레이어 몸을 회전, 상하 이동은 카메라를 회전.

모든 UI
-> 수업시간에 배운 Canvas 활용.
-> 버튼 삽입, 점수에 따라 스코어보드 값 변경(Text 이용).
-> 초기화면에서 게임 시작 버튼 클릭 시 씬 불러오기(LoadScene 이용).
-> 게임 종료 버튼 클릭 시 게임 종료(Quit 이용).
-> Esc로 메뉴 불러올 시 게임 시간 멈춤, 마우스 포인터 등장
-> Esc를 다시 누르거나 Resume 버튼 클릭 시 게임 재개