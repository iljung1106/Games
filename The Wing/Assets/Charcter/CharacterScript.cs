using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    StandaloneInputModule inputModule;
    Rigidbody rigidbody;
    CapsuleCollider capsule;
    AudioSource audio;

    float JumpRegenCool = 0;

    bool canFirstJump = true;
    bool canSecondJump = true;

    bool isOnGround = false;

    Vector3 TP;
    //public으로 선언하면 다른 클래스에서 가져갈 수 있고, 유니티 인스펙터에서 수정가능하다
    public GameObject touchParticle;

    public RectTransform JumpGauge;
    public RectTransform JumpGaugeLine;
    public Image GaugeSprite;
    public Image LineSprite;



    public Skybox skybox;

    public Transform SunTransform;

    public float JumpPower = 1200;

    // Start is called before the first frame update
    void Start()
    {
        inputModule = gameObject.GetComponent<StandaloneInputModule>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        capsule = gameObject.GetComponent<CapsuleCollider>();
        audio = gameObject.GetComponent<AudioSource>();

        Input.simulateMouseWithTouches = false;
    }

    // Update is called once per frame
    void Update()
    {
        //태양이 시간에 따라 돌아간다
        SunTransform.Rotate(Time.deltaTime, 0, 0);
        //점프 게이지의 크기를 쿨타임에 맞춘다
        JumpGauge.localScale = new Vector2(JumpRegenCool / 2, JumpRegenCool / 2);
        
        //canFirstJump변수와 canSecondJump 변수 값 할당
        RegenJump(isOnGround);


        if(canFirstJump) //화면에 표시되는 게이지의 색을 흰색으로, 게이지의 선을 푸른색으로
        {
            LineSprite.color = new Color(0.3f, 0.3f, 1f, 0.6f);
            GaugeSprite.color = new Color(1f, 1f, 1f, 0.6f);
        }
        else if(canSecondJump) //게이지의 선을 초록색으로
        {
            LineSprite.color = new Color(0.3f, 1f, 0.3f, 0.6f);
        }
        else //화면에 표시되는 게이지와 선을 붉은색으로
        {
            LineSprite.color = new Color(1f, 0.3f, 0.3f, 0.6f);
            GaugeSprite.color = new Color(1f, 0.6f, 0.6f, 0.6f);
        }
        
        if (JumpRegenCool <= 2.0f)  // 점프리젠쿨이 2초보다 작다면
        {
            JumpRegenCool += Time.deltaTime; // 프레임동안 지나간 시간만큼 점프리젠쿨어 더한다
        }
        

        //여기부터 마우스용
        if (!Input.touchSupported&& Input.mousePresent)
        {
            if (Input.GetMouseButton(0))    //마우스 좌클릭이 눌려 있다면
            {
                TP = Input.mousePosition;   //벡터 변수 TP를 마우스의 위치값으로
                TP.z = 5f;      //z축의 값을 캐릭터와 카메라에 맞도록 한다
                Vector3 TouchPos = Camera.main.ScreenToWorldPoint(TP);  // 벡터변수 TouchPos를 선언하고 TP를 월드좌표계에 맞춘 값으로 초기화
                TouchPos.z = 0f;    //z축의 값을 캐릭터와 카메라에 맞도록 한다
                touchParticle.transform.position = TouchPos;    //터치한 위치에 파티클을 옮긴다
            }
            if (Input.GetMouseButtonUp(0))      //마우스 좌클릭을 떼면
            {
                Vector3 TouchPos = Camera.main.ScreenToWorldPoint(TP);
                TouchPos.z = 0f;
                //마우스 왼쪽 버튼 눌렀을때와 같다

                Vector3 JumpDirection = (TouchPos - gameObject.transform.position).normalized;  //캐릭터가 점프할 방향을 (터치한 위치 - 캐릭터 위치) 의 방향으로 정한다
                JumpDirection.y = JumpDirection.y * 1.4f;       //화면의 비율을 고려해 y축의 이동값을 늘린다

                Jump(JumpDirection, JumpRegenCool);
            }
        }
        //여기까지 마우스용


        if (Input.touchCount > 0)   //터치가 되었다면
        {
            Touch t = Input.GetTouch(0);        // 터치 변수 t를 선언하고 t의 값을 0번째 터치로 정한다

            Vector3 TP = Input.GetTouch(0).position;    //여기부터 마우스일때 실행되는 코드와 거의 같다
            TP.z = 5f;
            Vector3 TouchPos = Camera.main.ScreenToWorldPoint(TP);
            TouchPos.z = 0f;
            touchParticle.transform.position = TouchPos;
            if (t.phase == TouchPhase.Ended)
            {
                touchParticle.transform.localScale = new Vector3(0, 0, 0);
                Vector3 JumpDirection = (TouchPos - gameObject.transform.position).normalized;
                JumpDirection.y = JumpDirection.y * 1.4f;
                Jump(JumpDirection, JumpRegenCool);
            }
            //여기까지 마우스일때 실행되는 코드와 거의 같다
        }

    }

    private void Jump(Vector3 direction, float charging)
    {
        audio.Play();//뛰는 효과음 재생                                     
        JumpRegenCool = 0;//점프 쿨타임 0으로 초기화
        if (canFirstJump)
        {
            canFirstJump = false;
            rigidbody.AddForce(direction * JumpPower * charging, ForceMode.Impulse);//캐릭터의 rigidbody에 JumpDirection 방향으로 게이지가 채워져 있을수록 강한 힘을 가한다. 
        }
        else if(canSecondJump)//만약 2단 점프라면
        {
            canSecondJump = false;
            rigidbody.AddForce(direction * JumpPower * charging, ForceMode.Impulse);//캐릭터의 rigidbody에 JumpDirection 방향으로 게이지가 채워져 있을수록 강한 힘을 가한다. 
        }
    }
    
    // 땅에 닿아있다면 canFirstJump, canSecondJump 변수 true
    void RegenJump (bool stayingGround)
    {
        if(isOnGround && JumpRegenCool >= 2f)
        {
            canFirstJump = true;
            canSecondJump = true;
        }
    }

    // 땅에 닿아있다면 isOnGround를 true
    public void OnCollisionStay(Collision collision)
    {
        isOnGround = true;
    }

    // 땅에 닿으면 isOnGround를 true
    public void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }

    // 땅에 닿아있지 않다면 isOnGround를 false
    private void OnCollisionExit(Collision collision)
    {

        isOnGround = false;
    }
}
