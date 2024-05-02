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

        
        float diffX = Mathf.Abs(playerPosition.x - tileMapPosition.x); //절대값으로 가져옴 Mathf.Abs()
        float diffY = Mathf.Abs(playerPosition.y - tileMapPosition.y);
        //위 코드 : 타일맵의 거리와 플레이어의 거리의 절대값을 저장해줌.
        
        
        Vector3 playerDir = GameManager.instance.player.inputVec;
        //플레이어에 입력된 벡터값을 저장함.

        
        float dirX = playerDir.x < 0 ? -1 : 1; 
        float dirY = playerDir.y < 0 ? -1 : 1;
        //플레이어가 어디로 가는지 확인
        //대각선 방향이면 Normalized에 의해 1보다 작은 값이 될것임.


        switch (transform.tag) //Area태그가 탈주한 오브젝트가 Ground1이라면?
        {
            case "Ground1":
                if(diffX > diffY)
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
            /*case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), 0f));
                    //몬스터 재배치
                    //재배치하고나면 재배치된 몬스터의 체력을 다시 채워줄 필요가 있음.
                }
                break;*/

        }
    }

}
