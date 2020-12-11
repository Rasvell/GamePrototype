using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class GameManager : MonoBehaviour
{

    public DecorationGenerator decorationGenerator;
    public Transform door;
    public Text EndResult;
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public Levels[] levels;

    private Save save = new Save();
    private string path;
    private Ball ball;
    private int currentLevel = 0;
    private GameObject level;
    public int obst;

    public int obstacles
    {
        get
        {
            return obst;
        }
        set
        {
            obst += value;
            if(obst <= 0)
            {
                ball.canMove = true;
                ball.shootController.canCreate = false;
            }
        }
    }

    private void Start()
    {
        path = Application.persistentDataPath + "/save.gamesave";
        if (File.Exists(path))
        {
            Load();
        }

        ball = FindObjectOfType<Ball>().GetComponent<Ball>();

        CreateLevel();
    }
    public void SaveGame()
    {
        try
        {
            save.currentLevel = currentLevel;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Create);

            bf.Serialize(fs, save);

            fs.Close();
        }
        catch
        {

        }
    }
    public void Load()
    {

        try
        {
            if (!File.Exists(path))
            {

                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            save = (Save)bf.Deserialize(fs);
            currentLevel = save.currentLevel;
        }
        catch
        {

        }
    }

    public void Lose()
    {
        EndResult.text = "You lose!";
        EndResult.color = Color.red;
        ball.canMove = false;
        Invoke("CreateLevel", 3f);
    }

    public void Win()
    {
        if (currentLevel < levels.Length-1) currentLevel++;
        else currentLevel = 0;
        SaveGame();
        EndResult.text = "You win!";
        EndResult.color = Color.green;
        Invoke("CreateLevel", 3f);
      
    }

    public void CreateLevel()
    {
        if (level != null) Destroy(level);
        level = Instantiate(levels[currentLevel].levelPrefab);

        ball.SetStartScale(levels[currentLevel].ballScale);
        ball.transform.position = StartPosition;
        ball.canMove = false;
        ball.shootController.road.transform.localScale =
        new Vector3(ball.transform.localScale.x * 0.1f, ball.shootController.road.transform.localScale.y, ball.shootController.road.transform.localScale.z);

        FindObjectOfType<ShootController>().GetComponent<ShootController>().canCreate = true;

        door.position = new Vector3(0, 1, levels[currentLevel].endPosZ);
        door.gameObject.GetComponent<Door>().animator.enabled = true;
        door.gameObject.GetComponent<Door>().animator.SetBool("NeedOpen", false);

        EndResult.text = "";
        obst = levels[currentLevel].ObstaclesCount;

        decorationGenerator.Clear();
        decorationGenerator.Decorate();
    }


    [System.Serializable]
    public class Levels
    {
        public GameObject levelPrefab;
        public float endPosZ;
        public int ObstaclesCount;

        [Range(2, 6)]
        public float ballScale = 2;
    }

    [System.Serializable]
    public class Save
    {
        public int currentLevel;
    }
}
