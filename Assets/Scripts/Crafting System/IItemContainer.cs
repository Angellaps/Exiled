public interface IItemContainer {

    bool ContainsItem(Item item);
    bool RemoveItem(Item item);
    bool AddItem(Item item);
    bool isFull();
    int ItemCount(Item item);
    
}
