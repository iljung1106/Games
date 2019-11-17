using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Xml;

public class GameManager : MonoBehaviour
{
    public int[] havingCharacter = new int[3]{ 1, 0, 0 };
    public int selectedCharacter;
    public int coins;

    public int SelectedMap = 0;

    public int attackLevel = 0;
    public int healthLevel = 0;

    int score = 0;
    int highScore = 0;
    public GameObject canvas;
    public GameObject scoreBoard;
    public Text HighScoreBox;
    public Text ScoreBox;
    public Text GotPointBox;
    public Text PointBox;

    public GameObject Camera;
    public GameObject CharacterCamera;

    float secondChecker = 0;
    bool gameStarted = false;

    public GameObject[] characters;

    //XmlDocument xmlDoc = new XmlDocument();
    //string fileName = "save.xml";

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        /*havingCharacter[0] = PlayerPrefs.GetInt("havingCharacter0", 1);
        havingCharacter[1] = PlayerPrefs.GetInt("havingCharacter1", 0);
        havingCharacter[2] = PlayerPrefs.GetInt("havingCharacter2", 0);
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);
        //selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);
        // 이거 테스트로 주석함
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        coins = PlayerPrefs.GetInt("Coins", 0);

        attackLevel = PlayerPrefs.GetInt("attackLevel", 0);
        healthLevel = PlayerPrefs.GetInt("healthLevel", 0);

        SelectedMap = PlayerPrefs.GetInt("selectedMap", 0);*/
        
        if (PlayerPrefs.GetInt("Played", 0) == 1)
        {
            LoadXml();
        }
        else
        {
            PlayerPrefs.SetInt("Played", 1);
            CreateXml();
        }

        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(9, 9);
        Physics2D.IgnoreLayerCollision(9, 11);
        Physics2D.IgnoreLayerCollision(11, 11);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted)
        {
            secondChecker += Time.deltaTime;
            if (secondChecker > 1)
            {
                secondChecker = 0;
                score += 10;
            }
        }
        else
        {
            PointBox.text = coins.ToString("D7");
        }
    }

    public void LoadMenu()
    {
        Camera.SetActive(true);
        CharacterCamera.SetActive(true);
        gameStarted = false;
        SceneManager.LoadScene("Scenes/MenuScene");
        canvas.SetActive(true);
        if(score > 0)
        {
            if(score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
            }
            coins += score / 10;
            PlayerPrefs.SetInt("Coins", coins);
            scoreBoard.SetActive(true);
            HighScoreBox.text = "최고 점수 : " + highScore.ToString();
            ScoreBox.text = "점수 : " + score.ToString();
            GotPointBox.text = "얻은 D : " + (score / 10).ToString();
            PointBox.text = coins.ToString();
        }
        SaveOverlapXml();
    }

    public void LoadGame()
    {
        Camera.SetActive(false);
        CharacterCamera.SetActive(false);
        SceneManager.LoadScene("Scenes/SaladDodgeScene" + SelectedMap);
        Camera.SetActive(false);
        secondChecker = 0;
        score = 0;
        gameStarted = true;
        DontDestroyOnLoad(Instantiate(characters[selectedCharacter]));
    }

    public int GetScore()
    {
        return score;
    }
    public void AddScore(int s)
    {
        score += s;
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt("Coins", coins);
    }

    private void LoadXml()
    {
        //TextAsset textAsset = (TextAsset)Resources.Load("Player");
        //Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        //xmlDoc.LoadXml(textAsset.text);
        xmlDoc.Load(Application.persistentDataPath + "/Player.xml");

        XmlNodeList nodes = xmlDoc.SelectNodes("PlayerInfo/Stats");

        foreach (XmlNode node in nodes)
        {
            coins = int.Parse(node.SelectSingleNode("Coin").InnerText);
            healthLevel = int.Parse(node.SelectSingleNode("HealthLv").InnerText);
            attackLevel = int.Parse(node.SelectSingleNode("AttackLv").InnerText);
            SelectedMap = int.Parse(node.SelectSingleNode("SelectMap").InnerText);
            selectedCharacter = int.Parse(node.SelectSingleNode("SelectChar").InnerText);
        }

        nodes = xmlDoc.SelectNodes("PlayerInfo/Characters");

        foreach (XmlNode node in nodes)
        {
            havingCharacter[0] = int.Parse(node.SelectSingleNode("Character0").InnerText);
            havingCharacter[1] = int.Parse(node.SelectSingleNode("Character1").InnerText);
            havingCharacter[2] = int.Parse(node.SelectSingleNode("Character2").InnerText);
        }
    }

    public void SaveOverlapXml()
    {
        //TextAsset textAsset = (TextAsset)Resources.Load("Player");
        //Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        //xmlDoc.LoadXml(textAsset.text);
        xmlDoc.Load(Application.persistentDataPath + "/Player.xml");

        XmlNodeList nodes = xmlDoc.SelectNodes("PlayerInfo/Stats");

        foreach (XmlNode node in nodes)
        {
            node.SelectSingleNode("Coin").InnerText = coins.ToString();
            node.SelectSingleNode("HealthLv").InnerText = healthLevel.ToString();
            node.SelectSingleNode("AttackLv").InnerText = attackLevel.ToString();
            node.SelectSingleNode("SelectMap").InnerText = SelectedMap.ToString();
            node.SelectSingleNode("SelectChar").InnerText = selectedCharacter.ToString();
        }

        nodes = xmlDoc.SelectNodes("PlayerInfo/Characters");

        foreach (XmlNode node in nodes)
        {
            node.SelectSingleNode("Character0").InnerText = havingCharacter[0].ToString();
            node.SelectSingleNode("Character1").InnerText = havingCharacter[1].ToString();
            node.SelectSingleNode("Character2").InnerText = havingCharacter[2].ToString();
        }

        xmlDoc.Save(Application.persistentDataPath + "/Player.xml");
    }
    
    private void CreateXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        // 루트 노드 생성
        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "PlayerInfo", string.Empty);
        xmlDoc.AppendChild(root);

        // 자식 노드 생성
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Stats", string.Empty);
        root.AppendChild(child);

        // 자식 노드에 들어갈 속성 생성
        XmlElement Coin = xmlDoc.CreateElement("Coin");
        Coin.InnerText = coins.ToString();
        child.AppendChild(Coin);

        XmlElement HealthLv = xmlDoc.CreateElement("HealthLv");
        HealthLv.InnerText = healthLevel.ToString();
        child.AppendChild(HealthLv);

        XmlElement AttackLv = xmlDoc.CreateElement("AttackLv");
        AttackLv.InnerText = attackLevel.ToString();
        child.AppendChild(AttackLv);

        XmlElement SelectMap = xmlDoc.CreateElement("SelectMap");
        SelectMap.InnerText = SelectedMap.ToString();
        child.AppendChild(SelectMap);

        XmlElement SelectChar = xmlDoc.CreateElement("SelectChar");
        SelectChar.InnerText = selectedCharacter.ToString();
        child.AppendChild(SelectChar);


        // 자식 노드 생성
        XmlNode characterChild = xmlDoc.CreateNode(XmlNodeType.Element, "Characters", string.Empty);
        root.AppendChild(characterChild);

        XmlElement Character0 = xmlDoc.CreateElement("Character0");
        Character0.InnerText = havingCharacter[0].ToString();
        characterChild.AppendChild(Character0);

        XmlElement Character1 = xmlDoc.CreateElement("Character1");
        Character1.InnerText = havingCharacter[1].ToString();
        characterChild.AppendChild(Character1);

        XmlElement Character2 = xmlDoc.CreateElement("Character2");
        Character2.InnerText = havingCharacter[2].ToString();
        characterChild.AppendChild(Character2);

        xmlDoc.Save(Application.persistentDataPath + "/Player.xml");
        Debug.Log(Application.persistentDataPath);
    }
}
