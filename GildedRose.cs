using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (IsLegendary(i)) 
                    continue;

                DecreaseSellInOfItem(i);

                if (IsABackstagePass(i))
                {
                    if (Items[i].SellIn < 0)
                    {
                        Items[i].Quality = 0;
                        continue;
                    }

                    IncreaseQualityOfItem(i);

                    if (Items[i].SellIn < 10)
                        IncreaseQualityOfItem(i);

                    if (Items[i].SellIn < 5)
                        IncreaseQualityOfItem(i);

                    continue;
                }

                if (IsAgedBrie(i))
                {
                    IncreaseQualityOfItem(i);

                    if (Items[i].SellIn >= 0)
                        continue;

                    IncreaseQualityOfItem(i);

                    continue;
                }
                
                ReduceQualityOfItem(i);

                if (Items[i].SellIn >= 0)
                    continue;

                ReduceQualityOfItem(i);
            }
        }

        private bool IsConjured(int i) => Items[i].Name.StartsWith("Conjured");

        private bool IsABackstagePass(int i) => Items[i].Name.StartsWith("Backstage passes");

        private bool IsAgedBrie(int i) => Items[i].Name == "Aged Brie";

        private bool IsLegendary(int i) => Items[i].Name == "Sulfuras, Hand of Ragnaros";

        private void IncreaseQualityOfItem(int i)
        {
            Items[i].Quality = Items[i].Quality + 1;

            if (Items[i].Quality > 50)
                Items[i].Quality = 50;
        }

        private void DecreaseSellInOfItem(int i)
        {
            Items[i].SellIn = Items[i].SellIn - 1;
        }

        private void ReduceQualityOfItem(int i)
        {
            Items[i].Quality = Items[i].Quality - 1;
            
            if (IsConjured(i))
                Items[i].Quality = Items[i].Quality - 1;

            if (Items[i].Quality < 0) Items[i].Quality = 0;
        }
    }
}
