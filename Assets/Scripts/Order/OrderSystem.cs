using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderSystem : MonoBehaviour
{
    private int[] orders = {0, 0, 0, 0};
    private int[] currentOrders = {0, 0, 0, 0};
    private int selectedOrders = 0;

    private int totalOnOff;
    
    private int ordersCompleted;
    [SerializeField] private int maxAmountOfOrdersDaily;
    
    [SerializeField] private int maxOrder;
    [SerializeField] private int minOrder;
    
    [SerializeField] private int maxSelectedOrder;
    [SerializeField] private int minSelectedOrder;
    
    [SerializeField] private int orderOn;
    [SerializeField] private int orderOff;
    
    private bool orderRandomized;
    private bool orderSelected;
    
    [SerializeField] private List<TMP_Text> orderTexts;

    [SerializeField] private string[] ingredientsTags;

    // Update is called once per frame

    void Update()
    {
        if (!GameObject.FindWithTag("Canvas").GetComponent<MainMenu>().HasClicked) return;
        SelectedOrder();

        var ordersDone = 0;
        for (int i = 0; i < currentOrders.Length; i++)
        {
            ordersDone += currentOrders[i];
        }
        
        if (ordersDone == 0)
        {
            OrderCompleted();
        }

        if (ordersCompleted < maxAmountOfOrdersDaily && GameObject.FindWithTag("Canvas").GetComponent<dayNightCycle>().GetTime == 0)
        {
            Time.timeScale = 0;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        CheckIngredient(other);
    }

    private void OrderCompleted()
    {
        ordersCompleted++;
        
        orderSelected = false;
        orderRandomized = false;
        
        SelectedOrder();
    }

    private void SelectedOrder()
    {
        if (orderSelected) return;
        selectedOrders = Random.Range(minSelectedOrder, maxSelectedOrder);
        orderSelected = true;
        
        SwitchOrder(selectedOrders);
        RandomizeOrder();
    }

    private void SwitchOrder(int newOrder)
    {
        for (int s = 0; s < orderTexts.Count; s++)
        {
            orderTexts[s].text = Random.Range(orderOff, orderOn).ToString();
            totalOnOff += int.Parse(orderTexts[s].text);
        }

        if (totalOnOff == 0)
        {
            orderTexts[1].text = "1";
        }
        
        for (int s = 0; s < orderTexts.Count; s++)
        {
            if (orderTexts[s].text == orderOff.ToString())
            {
                orderTexts[s].enabled = false;
            }
            else
            {
                orderTexts[s].enabled = true;
            }
        }
    }

    private void RandomizeOrder()
    {
        if (orderRandomized) return;
        for (var i = 0; i < orders.Length; i++)
        {
            if (orderTexts[i].text != orderOff.ToString())
            {
                currentOrders[i] = orders[i] = Random.Range(minOrder, maxOrder);
                orderRandomized = true;
            }
        }

        for (int i = 0; i < orderTexts.Count; i++)
        {
            orderTexts[i].text = currentOrders[i].ToString(": " + currentOrders[i]);
        }
    }

    private void CheckIngredient(Collision other)
    {
        for (int i = 0; i < ingredientsTags.Length; i++)
        {
            if (other.gameObject.CompareTag(ingredientsTags[i]))
            {
                if (currentOrders[i] == 0)
                {
                    Destroy(other.gameObject);
                }
                else
                {
                    currentOrders[i]--;
                    Destroy(other.gameObject);
                
                    orderTexts[i].text = currentOrders[i].ToString(": " + currentOrders[i]);
                }
            }
            else if (!other.gameObject.CompareTag(ingredientsTags[i]))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
