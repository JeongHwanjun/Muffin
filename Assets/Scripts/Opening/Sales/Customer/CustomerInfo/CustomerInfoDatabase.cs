using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "CustomerDatabase", menuName = "Scriptable Objects/Sales/CustomerDatabase")]
public class CustomerDatabase : ScriptableObject
{
    [SerializeField]
    private List<CustomerInfo> customers = new();

    public IReadOnlyList<CustomerInfo> Customers => _sortedCustomers ??= SortCustomers();

    private List<CustomerInfo> _sortedCustomers;

    private List<CustomerInfo> SortCustomers()
    {
        _sortedCustomers = customers.OrderBy(c => c.id).ToList();
        return _sortedCustomers;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        SortCustomers();
    }
#endif

    public CustomerInfo GetById(int id)
    {
        return Customers.FirstOrDefault(c => c.id == id);
    }
}
