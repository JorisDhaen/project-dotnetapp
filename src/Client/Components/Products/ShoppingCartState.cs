using Domain;

namespace Client.Components.Products
{
    public class ShoppingCartState
    {
        private ISet<OrderItem> _items;
        public ShoppingCartState()
        {
            _items = new HashSet<OrderItem>();
        }
        public ISet<OrderItem> Items { get => _items; }

        public event Action OnChange;
        public event Action OnAdded;

        public void AddItem(OrderItem item)
        {
            OrderItem? i = _items.FirstOrDefault(i => i.ItemId == item.ItemId);
            if (i == null)
            {
                _items.Add(item);
            } else
            {
                PlusOne(i);
            }
            OnAdded?.Invoke();
        }
        public void RemoveItem(OrderItem item)
        {
            _items.Remove(item);
            NotifyStateChanged();
        }

        public void MinusOne(OrderItem item)
        {
            if(item.Quantity > 0)
            {
                item.Quantity--;
            }
            NotifyStateChanged();
        }

        public void PlusOne(OrderItem item)
        {
            if(item.Product.LeftInStock > item.Quantity)
            {
                item.Quantity++;
                NotifyStateChanged();
            }
        }

        public void ClearItems() { 
            _items.Clear();
            NotifyStateChanged();
        }

        public void setItems(ShoppingCartState a)
        {
            if (a.Items.Count > 0)
            {
                _items.Clear();
                foreach (var item in a.Items)
                {
                    _items.Add(item);
                }
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
