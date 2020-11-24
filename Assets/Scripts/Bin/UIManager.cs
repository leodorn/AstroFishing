//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class UIManager : MonoBehaviour
//{
//    public static UIManager instance;
//    [SerializeField]
//    GameObject buttonGoInBoat, buttonOutOfBoat, panelShop, buttonSellAllFish,moneyTextShop;
//    [SerializeField]
//    TextMeshProUGUI moneyText;
//    [SerializeField]
//    GameObject buttonShop;
//    public List<int> multiplyForEachLevelList;
//    [SerializeField]
//    GameObject menuShip;


//    private void Awake()
//    {
//        if(instance == null)
//        {
//            instance = this;
//        }
//    }

//    private void Update()
//    {
        
//    }

//    public void PushButtonSellAllFish()
//    {
//        if(Player.instance.inBoat && Player.instance.boat != null)
//        {
//            SellAllFish(Player.instance.boat.GetComponent<Boat>().fishCaughtList, Player.instance.boat.GetComponent<Boat>().fishStockPlaceTransform);
//        }
//        else if(!Player.instance.inBoat)
//        {
//            SellAllFish(Player.instance.fishCaughtList, GameObject.Find("PointToStockFishNoBoat").transform);
//        }
//    }
//    public void SellAllFish(List<Fish> fishCaughtList,Transform pointToStock)
//    {       
//        if(fishCaughtList.Count == 0)
//        {
//            Debug.Log("Vous n'avez pas de poisson sur ce lieu !" );
//        }
//        else
//        {
//            int amount = 0;
//            foreach(Fish fish in fishCaughtList)
//            { 
//                if(fish.fishData.levelOfFish < multiplyForEachLevelList.Count)
//                {
//                    amount += fish.getPriceDependOnWeight() * multiplyForEachLevelList[fish.fishData.levelOfFish];
//                }
//                else
//                {
//                    amount += fish.getPriceDependOnWeight();
//                }
               
//            }                
//            for (int i = 0; i < pointToStock.transform.childCount; i++)
//            {
//                Destroy(pointToStock.transform.GetChild(i).gameObject);
//            }
//            Debug.Log("Vous avez gagné " + amount + " argent");
//            Player.instance.playerData.money += amount;
//            SetTextTotalMoney(Player.instance.playerData.money);
//            fishCaughtList.Clear();
//        } 
//    }

//    public void SetTextTotalMoney(int money)
//    {
//        moneyText.text = "Money : " + money.ToString();
//    }

//    public void SetActiveButtonGoInBoat(bool active)
//    {
//        buttonGoInBoat.SetActive(active);
//    }

//    public void PushButtonGoInBoat()
//    {
//        if(!Player.instance.isFishing)
//        {
//            Player.instance.GoInBoat();
//            SetActiveButtonGoInBoat(false);
//        }

//    }

//    public void PushButtonGoOutOfBoat()
//    {
//        if (!Player.instance.isFishing)
//        {
//            //Player.instance.GoOutOfBoat();
//            SetActiveButtonGoOutOfBoat(false);
//        }
//    }

//    public void SetActiveButtonGoOutOfBoat(bool active)
//    {
//        buttonOutOfBoat.SetActive(active);
//    }

//    public void OpenShop()
//    {
//        Player.instance.SetState(new PlayerInShop(Player.instance));
//        panelShop.SetActive(true);
//        buttonOutOfBoat.SetActive(false);
//        buttonGoInBoat.SetActive(false);
//        buttonShop.SetActive(false);
//        buttonSellAllFish.SetActive(false);
//        moneyText.gameObject.SetActive(false);
//        moneyTextShop.SetActive(true);
//        moneyTextShop.GetComponent<TextMeshProUGUI>().text = moneyText.text;


//    }

//    public void OpenOrCloseMenuShip(bool active)
//    {
//        menuShip.SetActive(active);
//    }

//    public void CloseShop()
//    {
//        Player.instance.SetState(new PlayerOnGround(Player.instance));
//        panelShop.SetActive(false);
//        buttonShop.SetActive(true);
//        moneyText.gameObject.SetActive(true);
//        buttonSellAllFish.SetActive(true);
//        moneyTextShop.SetActive(false);
//    }
//}
