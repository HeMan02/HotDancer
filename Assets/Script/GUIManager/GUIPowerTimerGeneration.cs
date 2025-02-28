using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GUIPowerTimerGeneration : MonoBehaviour
{
    public float timerGeneration;
    public Image timerCard;
    public GameObject[] powersObj;
    public int indexPowerEnable;
    List<PowerInfo<IEntity>> powerRandomGenerate;
    PowerInfo<IEntity> activePower;
    public GameObject powerFather;
    float timerGenerationPowerPlayer;
    public GameData gameData;
    Dictionary<IEntity,GameObject> dicListPowerEnable = new Dictionary<IEntity, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if(gameData)
        timerGenerationPowerPlayer = gameData.timerGenerationPowerPlayer;
        timerGeneration = 0;
        indexPowerEnable = -1;
        ChangePower();
    }

    // Update is called once per frame
    void Update()
    {
        
        timerGeneration += Time.deltaTime/ timerGenerationPowerPlayer;
        timerCard.fillAmount -= timerGeneration;
        if (timerCard.fillAmount <= 0)
        {
            timerCard.fillAmount = 1;
            timerGeneration = 0;
            ChangePower();
            Mediator.Instance.SetAction(true, IEntity.TypeEvents.ChangePower);
        }
    }

    public void ChangePower()
    { 
        powerRandomGenerate = Mediator.Instance.GetListPowersMediator();
        indexPowerEnable = Random.Range(0, powerRandomGenerate.Count);

        Mediator.Instance.SetActivePower(powerRandomGenerate[indexPowerEnable]);
        activePower = powerRandomGenerate[indexPowerEnable];



        for (int i = 0; i < powersObj.Length; i++)
        {
            if (powersObj[i].name.Equals(powerRandomGenerate[indexPowerEnable].Entity.NamePower))
            {
                powersObj[i].SetActive(true);
                timerCard = powersObj[i].transform.GetChild(1).GetComponent<Image>();

                if (!dicListPowerEnable.ContainsKey(powerRandomGenerate[indexPowerEnable].Entity))
                {
                    GameObject powerActiveObjPrefab = Resources.Load("Prefab/Power" + powerRandomGenerate[indexPowerEnable].Entity.NamePower) as GameObject;
                    GameObject powerGui = Instantiate(powerActiveObjPrefab, powerActiveObjPrefab.transform.position, powerActiveObjPrefab.transform.rotation);
                    dicListPowerEnable.Add(powerRandomGenerate[indexPowerEnable].Entity, powerGui);
                    Text powerText = powerGui.transform.GetChild(2).GetComponent<Text>();
                    powerText.text = "1";                   
                    powerGui.transform.parent = powerFather.transform;
                }
                else
                {              
                    Text powerText = dicListPowerEnable[powerRandomGenerate[indexPowerEnable].Entity].transform.GetChild(2).GetComponent<Text>();
                    int value = int.Parse(powerText.text.ToString());
                    value += 1;
                    GameObject a = dicListPowerEnable[powerRandomGenerate[indexPowerEnable].Entity];
                    a.transform.GetChild(2).GetComponent<Text>().text = value.ToString();
                    dicListPowerEnable[powerRandomGenerate[indexPowerEnable].Entity] = a;
                }          
            }
            else
            {
                powersObj[i].SetActive(false);
            }
        }
    }
}
