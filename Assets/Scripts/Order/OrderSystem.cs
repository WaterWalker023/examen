using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderSystem : MonoBehaviour
{
    [SerializeField] private int order;

    [SerializeField] private int[] orders = {0, 0, 0, 0};
    [SerializeField] private int[] currentOrders = {0, 0, 0, 0};
    
    [SerializeField] private int maxOrder;
    [SerializeField] private int minOrder;
    
    [SerializeField] private bool orderRandomized;
    
    [SerializeField] private TMP_Text orderText1;
    [SerializeField] private TMP_Text orderText2;
    [SerializeField] private TMP_Text orderText3;
    [SerializeField] private TMP_Text orderText4;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        print("Hello world");
    }

    // Update is called once per frame

    void Update()
    {
        if (!GameObject.FindWithTag("Canvas").GetComponent<MainMenu>().HasClicked) return;
        RandomizeOrder();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        CheckOrder(other);
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
