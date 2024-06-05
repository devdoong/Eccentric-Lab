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

    private void OnTriggerExit2D(Collider2D collision) //�������� �߻�.
    {
        if(!collision.CompareTag("Area")) //Ÿ�ϸʿ��� Ż���Ѱ� Area��� �Ʒ� �ڵ� �����ϴ°���.
            return;

        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 tileMapPosition = transform.position; //Ÿ�ϸ��� ������

        



        switch (transform.tag) //Area�±װ� Ż���� ������Ʈ�� Ground1�̶��?
        {
            case "Ground1":
                //�÷��̾�� Ÿ�ϸ��� �Ÿ��� ���
                float diffX = playerPosition.x - tileMapPosition.x; 
                float diffY = playerPosition.y - tileMapPosition.y;
                float dirX = diffX < 0 ? -1 : 1;  // ������ Ÿ�ϸ��� �÷��̾�� ����, ����� ������
                float dirY = diffY < 0 ? -1 : 1;
                
              
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40); //Translate �̵��� ���� �־��ָ�ȴ�.
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

                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0); //���� ���� ���Ͽ� �����ִ� ���� ���ġ
                    transform.Translate(ran + dist * 2);
                }
                break;

        }
    }

}
