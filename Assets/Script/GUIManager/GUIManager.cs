using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    // POWERS OBJ
    [Header("Power obj")]
    [SerializeField] public Sprite[] powersSprites;
    [SerializeField] public GameObject[] powersObjActive;
    [SerializeField] public Text[] powersTextActive;
    [SerializeField] public GameObject powerActiveFather;

    [SerializeField] public GameObject[] powersObjRandomGenerate;
    [SerializeField] public Text[] powersTextRandomGenerate;
    [SerializeField] public GameObject powerObjRandomGenerateFather;

    // DIC POWERS
    List<PowerInfo<IEntity>> powerRandomGenerate;
    Dictionary<GameObject, PowerInfo<IEntity>> dicObjRandomGenerationPowers = new Dictionary<GameObject, PowerInfo<IEntity>>();
    List<PowerInfo<IEntity>> activePowers = new List<PowerInfo<IEntity>>();

    // RECHARGE BULLET
    [Header("Recharge bullet")]
    [SerializeField] public Image bullets;
    int maxBulletStart = 8;
    int maxBulletCounter = 0;
    float timerRechargeBullet = 0;
    int timeRecharge = 2;
    bool recharging = false;
    float fillToRemoveBullet;
    [SerializeField] public Text activeBullet;

    // LIFE
    [Header("Life")]
    [SerializeField] public Image life;

    // CURSOR
    [Header("Cursor")]
    [SerializeField] public Texture2D cursor;

    // END GAME
    [Header("End Game")]
    [SerializeField] public Image endGameWin;
    [SerializeField] public Image endGameLose;
    [SerializeField] public GameObject endGameObj;
    [SerializeField] public Text endGameText;

    // ENEMY
    [Header("Enemy")]
    [SerializeField]  public Sprite[] spritesEnemies;
    [SerializeField] public UnityEngine.UI.Image imageEnemy;
    [SerializeField] public Text numberEnemies;
    int maxEnemy = 0;

    // ENEMY POWERS
    [Header("Enemy Powers")]
    [SerializeField] public UnityEngine.UI.Image[] enemyPowers;
    [SerializeField] public Text[] enemyPowersText;
    Dictionary<int,int> enemyPowerCounter = new Dictionary<int,int>();

    // COINS
    [SerializeField] public Text coins;
    int numCoins = 0;
    void OnEnable()
    {
        Mediator.Instance.RegisterAction(SetRecharge, IEntity.TypeEvents.Recharge);
        Mediator.Instance.consumeBullet += ConsumeBullet;
        Mediator.Instance.RegisterAction(SetDamageGUI, IEntity.TypeEvents.Life);
        Mediator.Instance.RegisterAction(AddLifeGUI, IEntity.TypeEvents.RechargeLife);
        Mediator.Instance.RegisterAction(SetEndGame, IEntity.TypeEvents.EndGame);
        Mediator.Instance.RegisterAction(SetNumberEnemy, IEntity.TypeEvents.SetNumEnemy);
        Mediator.Instance.RegisterAction(SetImagePowersEnemy, IEntity.TypeEvents.SetPowerEnemy);
        Mediator.Instance.RegisterAction(SetImageEnemyCount, IEntity.TypeEvents.ImageEnemyCount);
        Mediator.Instance.RegisterAction(SetCoins, IEntity.TypeEvents.SetCoins);       
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2 cursorHotspot = new Vector2(cursor.width / 2, cursor.height / 2);
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.ForceSoftware);

        ResetImageEnemyPower();
        fillToRemoveBullet = 1f / float.Parse(maxBulletStart.ToString());
        maxBulletCounter = maxBulletStart;

        endGameObj.SetActive(false);
        activeBullet.text = maxBulletCounter + "/" + maxBulletStart;
        coins.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (recharging)
        {
            timerRechargeBullet += Time.deltaTime;
            bullets.fillAmount = timerRechargeBullet * timeRecharge / 4;
            if (bullets.fillAmount >= 1)
            {
                timerRechargeBullet = 0;
                bullets.fillAmount = 1;
                recharging = false;
                Mediator.Instance.SetAction(false, IEntity.TypeEvents.Reload);
                maxBulletCounter = maxBulletStart;
                activeBullet.text = maxBulletCounter + "/" + maxBulletStart;
            }
        }
    }

    public bool ConsumeBullet(bool value)
    {
        if (value && !recharging)
        {
            maxBulletCounter -= 1;
            activeBullet.text = maxBulletCounter + "/" + maxBulletStart;
            bullets.fillAmount -= fillToRemoveBullet;
            if (maxBulletCounter == 0)
            {
                recharging = true;
                Mediator.Instance.SetAction(true, IEntity.TypeEvents.Reload);
                return recharging;
            }
            return recharging;
        }
        return recharging;
    }

    public void SetRandomPowers(List<PowerInfo<IEntity>> powers)
    {
        dicObjRandomGenerationPowers = new Dictionary<GameObject, PowerInfo<IEntity>>();
        powerObjRandomGenerateFather.SetActive(true);
        for (int i = 0; i < powers.Count; i++)
        {
            powersObjRandomGenerate[i].SetActive(true);
            powersTextRandomGenerate[i].text = "NAME:" + powers[i].Entity.NamePower.ToString() + " VALUE: " + powers[i].Entity.EffectValue.ToString();
            dicObjRandomGenerationPowers.Add(powersObjRandomGenerate[i], powers[i]);
        }
    }

    public void SetActivePowers(List<PowerInfo<IEntity>> powers)
    {
        if (!powerActiveFather.active)
        {
            powerActiveFather.SetActive(true);
        }
        for (int i = 0; i < powers.Count; i++)
        {
            powersObjActive[i].SetActive(true);
            powersTextActive[i].text = "NAME:" + powers[i].Entity.NamePower.ToString() + " VALUE: " + powers[i].Entity.EffectValue.ToString();
        }
    }

    // SCELTA DA TASTO INTERFACCIA
    public void ChoicePower(GameObject button)
    {
        if (activePowers.Count < powersObjRandomGenerate.Length)
        {
            Mediator.Instance.SetActivePower(dicObjRandomGenerationPowers[button]);
            activePowers.Add(dicObjRandomGenerationPowers[button]);
            SetActivePowers(activePowers);
        }
        else
        {
            return;
        }

    }

    public void SetRecharge(object value)
    {
        timeRecharge = (int)value;
    }

    public void SetDamageGUI(object value)
    {
        float convertDamageFill = ((float)(int)value/100f);

        life.fillAmount -= convertDamageFill;
    }

    public void AddLifeGUI(object value)
    {
        float convertAddFill = ((float)(int)value/100f);

        life.fillAmount += convertAddFill;
    }

    public void SetCoins(object value)
    {
        numCoins += int.Parse(value.ToString());
        if (numCoins != 0)
        {
            if(!coins.enabled)
                coins.enabled = true;
            coins.text = "X " + numCoins.ToString();
        }else
        coins.enabled=false;
    }

    public void SetEndGame(object value)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        // Se vero richiamato da nemici
        if ((bool)value)
        {
                endGameObj.SetActive(true);
                endGameWin.enabled = true;
                endGameLose.enabled = false;
                endGameText.text = "YOU WIN!!";
                Time.timeScale = 0;
        }
        else
        {
            // Richiamato da player
            endGameObj.SetActive(true);
            endGameWin.enabled = false;
            endGameLose.enabled = true;
            endGameText.text = "YOU LOSE";
            Time.timeScale = 0;
        }
    }

    public void SetImageEnemyCount(object value)
    {
        imageEnemy.sprite = spritesEnemies[(int)value];
    }

    public void SetNumberEnemy(object value)
    {
        maxEnemy += int.Parse(value.ToString());
        if(maxEnemy <= 0)
        {
            numberEnemies.enabled = false;
        }
        else
        {
            numberEnemies.text = maxEnemy.ToString();
        }
    }

    public void SetImagePowersEnemy(object value)
    {
        RegisterEnemyPower((int)value);

        enemyPowers[(int)value].enabled = true;
        enemyPowersText[(int)value].enabled = true;
        enemyPowersText[(int)value].text = "X " + enemyPowerCounter[(int)value].ToString();
    }

    public void RegisterEnemyPower(int value)
    {
        if (enemyPowerCounter.ContainsKey(value))
            enemyPowerCounter[value]++;
        else
            enemyPowerCounter.Add(value, 1);
    }

    public void ResetImageEnemyPower()
    {
        for (int i = 0; i < enemyPowers.Length; i++)
        {
            enemyPowers[i].enabled = false;
            enemyPowersText[i].enabled = false;
        }
    }

}
