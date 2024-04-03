using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Reposition : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision) //�������� �߻�.
    {
        Debug.Log("����");
        if(!collision.CompareTag("Area")) //Ÿ�ϸʿ��� Ż���Ѱ� Area��� �Ʒ� �ڵ� �����ϴ°���.
            return;

        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 tileMapPosition = transform.position; //Ÿ�ϸ��� ������

        
        float diffX = Mathf.Abs(playerPosition.x - tileMapPosition.x); //���밪���� ������ Mathf.Abs()
        float diffY = Mathf.Abs(playerPosition.y - tileMapPosition.y);
        //�� �ڵ� : Ÿ�ϸ��� �Ÿ��� �÷��̾��� �Ÿ��� ���밪�� ��������.
        
        
        Vector3 playerDir = GameManager.instance.player.inputVec;
        //�÷��̾ �Էµ� ���Ͱ��� ������.

        
        float dirX = playerDir.x < 0 ? -1 : 1; 
        float dirY = playerDir.y < 0 ? -1 : 1;
        //�÷��̾ ���� ������ Ȯ��
        //�밢�� �����̸� Normalized�� ���� 1���� ���� ���� �ɰ���.


        switch (transform.tag) //Area�±װ� Ż���� ������Ʈ�� Ground1�̶��?
        {
            case "Ground1":
                if(diffX > diffY)
                {
                    Debug.Log("�׶��� ȣ��");
                    transform.Translate(Vector3.right * dirX * 40); //Translate �̵��� ���� �־��ָ�ȴ�.
                }
                else if (diffX < diffY)
                {
                    Debug.Log("�׶��� ȣ��");
                    transform.Translate(Vector3.up * dirY * 40);
                }
                else
                {
                    transform.Translate(Vector3.right * dirX * 40);
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                break;

        }
    }

}
