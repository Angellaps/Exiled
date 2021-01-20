public interface IItemContainer
{

    //bool ContainsItem(ItemSO item);
    bool RemoveItem(ItemSO item);
    bool AddItem(ItemSO item, int amount);
    //bool isFull();
    int ItemCount(ItemSO item);



}
