using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
       coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision) //나갔을때 발생.
    {
        if(!collision.CompareTag("Area")) //타일맵에서 탈주한게 Area라면 아래 코드 실행하는거임.
            return;

        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 tileMapPosition = transform.position; //타일맵의 포지션

        



        switch (transform.tag) //Area태그가 탈주한 오브젝트가 Ground1이라면?
        {
            case "Ground1":
                //플레이어와 타일맵의 거리를 계산
                float diffX = playerPosition.x - tileMapPosition.x; 
                float diffY = playerPosition.y - tileMapPosition.y;
                float dirX = diffX < 0 ? -1 : 1;  // 음수면 타일맵이 플레이어보다 왼쪽, 양수면 오른쪽
                float dirY = diffY < 0 ? -1 : 1;
                
              
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40); //Translate 이동할 양을 넣어주면된다.
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                else
                {
                    transform.Translate(Vector3.right * dirX * 40);
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPosition - tileMapPosition;

                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0); //랜덤 벡터 더하여 퍼져있는 몬스터 재배치
                    transform.Translate(ran + dist * 2);
                }
                break;

        }
    }

}
