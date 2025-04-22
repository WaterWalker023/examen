using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderSystem : MonoBehaviour
{
    [SerializeField] private int order;

    [SerializeField] private int[] orders = {0, 0, 0, 0};
    [SerializeField] private int[] currentOrders = {0, 0, 0, 0};
    [SerializeField] private int[] selectedOrders = {0};
    
    [SerializeField] private int maxOrder;
    [SerializeField] private int minOrder;
    
    [SerializeField] private int maxSelectedOrder;
    [SerializeField] private int minSelectedOrder;
    
    [SerializeField] private bool orderRandomized;
    [SerializeField] private bool orderSelected;
    
    [SerializeField] private TMP_Text orderText1;
    [SerializeField] private TMP_Text orderText2;
    [SerializeField] private TMP_Text orderText3;
    [SerializeField] private TMP_Text orderText4;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        if (!GameObject.FindWithTag("Canvas").GetComponent<MainMenu>().HasClicked) return;
        SelectedOrder();
        
        if (currentOrders[0] == 0)
        {
            orderSelected = false;
            orderRandomized = false;
            OrderCompleted();
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        CheckOrder(other);
    }

    private void OrderCompleted()
    {
        SelectedOrder();
    }

    private void SelectedOrder()
    {
        if (orderSelected) return;
        for (int s = 0; s < selectedOrders.Length; s++)
        {
            selectedOrders[s] = Random.Range(minSelectedOrder, maxSelectedOrder);
            orderSelected = true;
            
            RandomizeOrder();
            SwitchOrder(selectedOrders[s]);
        }
    }

    private void SwitchOrder(int newOrder)
    {
        switch (newOrder)
        {
            default:
                orderText1.enabled = false;
                orderText2.enabled = false;
                orderText3.enabled = false;
                orderText4.enabled = false;
                break;
            
            case 0:
                orderText1.enabled = true;
                orderText2.enabled = false;
                orderText3.enabled = false;
                orderText4.enabled = false;
                break;
            
            case 1:
                orderText1.enabled = false;
                orderText2.enabled = true;
                orderText3.enabled = false;
                orderText4.enabled = false;
                break;
            
            case 2:
                orderText1.enabled = false;
                orderText2.enabled = false;
                orderText3.enabled = true;
                orderText4.enabled = false;
                break;
            
            case 3:
                orderText1.enabled = false;
                orderText2.enabled = false;
                orderText3.enabled = false;
                orderText4.enabled = true;
                break;
            
            case 4:
                orderText1.enabled = true;
                orderText2.enabled = true;
                orderText3.enabled = false;
                orderText4.enabled = false;
                break;
            
            case 5:
                orderText1.enabled = true;
                orderText2.enabled = false;
                orderText3.enabled = true;
                orderText4.enabled = false;
                break;
            
            case 6:
                orderText1.enabled = true;
                orderText2.enabled = false;
                orderText3.enabled = false;
                orderText4.enabled = true;
                break;
            
            case 7:
                orderText1.enabled = false;
                orderText2.enabled = true;
                orderText3.enabled = true;
                orderText4.enabled = false;
                break;
            
            case 8:
                orderText1.enabled = false;
                orderText2.enabled = true;
                orderText3.enabled = false;
                orderText4.enabled = true;
                break;
            
            case 9:
                orderText1.enabled = false;
                orderText2.enabled = false;
                orderText3.enabled = true;
                orderText4.enabled = true;
                break;
        }
    }

    private void RandomizeOrder()
    {
        if (orderRandomized) return;
        for (var i = 0; i < orders.Length; i++)
        {
            currentOrders[i] = orders[i] = Random.Range(minOrder, maxOrder);
            orderRandomized = true;
        }
        
        orderText1.text = currentOrders[0].ToString(": " + currentOrders[0]);
        orderText2.text = currentOrders[1].ToString(": " + currentOrders[1]);
        orderText3.text = currentOrders[2].ToString(": " + currentOrders[2]);
        orderText4.text = currentOrders[3].ToString(": " + currentOrders[3]);
    }

    private void CheckOrder(Collision other)
    {
        if (other.gameObject.CompareTag("Carrot"))
        {
            if (currentOrders[0] == 0)
            {
                Destroy(other.gameObject);
            }
            else
            {
                currentOrders[0]--;
                Destroy(other.gameObject);
            
                orderText1.text = currentOrders[0].ToString(": " + currentOrders[0]);
            }
        }
        if (other.gameObject.CompareTag("Mushroom"))
        {
            if (currentOrders[1] == 0)
            {
                Destroy(other.gameObject);
            }
            else
            {
                currentOrders[1]--;
                Destroy(other.gameObject);
            
                orderText2.text = currentOrders[1].ToString(": " + currentOrders[1]);
            }
        }
        if (other.gameObject.CompareTag("Pumpkin"))
        {
            if (currentOrders[2] == 0)
            {
                Destroy(other.gameObject);
            }
            else
            {
                currentOrders[2]--;
                Destroy(other.gameObject);
            
                orderText3.text = currentOrders[2].ToString(": " + currentOrders[2]);
            }
        }
        if (other.gameObject.CompareTag("Lettuce"))
        {
            if (currentOrders[3] == 0)
            {
                Destroy(other.gameObject);
            }
            else
            {
                currentOrders[3]--;
                Destroy(other.gameObject);
            
                orderText4.text = currentOrders[3].ToString(": " + currentOrders[3]);
            }
        }
        else if (!other.gameObject.CompareTag("Ground"))
        {
            Destroy(other.gameObject);
        }
    }
}
