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
                if (IsLegendary(i)) continue;

                if (ShouldIncreaseQualityOfItem(i))
                {
                    IncreaseQualityOfItem(i);

                    if (IsABackstagePass(i))
                    {
                        if (Items[i].SellIn < 11)
                        {
                            IncreaseQualityOfItem(i);
                        }

                        if (Items[i].SellIn < 6)
                        {
                            IncreaseQualityOfItem(i);
                        }
                    }
                }
                else
                {
                    ReduceQualityOfItem(i);
                }

                DecreaseSellInOfItem(i);

                if (Items[i].SellIn >= 0)
                {
                    continue;
                }

                if (Items[i].Name == "Aged Brie")
                {
                    IncreaseQualityOfItem(i);
                    continue;
                }

                if (IsABackstagePass(i))
                {
                    Items[i].Quality = Items[i].Quality - Items[i].Quality;
                    continue;
                }

                ReduceQualityOfItem(i);
            }
        }

        private bool IsConjured(int i) => Items[i].Name == "Conjured Mana Cake";

        private bool IsABackstagePass(int i) => Items[i].Name == "Backstage passes to a TAFKAL80ETC concert";

        private bool ShouldIncreaseQualityOfItem(int i) => Items[i].Name == "Aged Brie" || Items[i].Name == "Backstage passes to a TAFKAL80ETC concert";

        private bool IsLegendary(int i) => Items[i].Name == "Sulfuras, Hand of Ragnaros";

        private void IncreaseQualityOfItem(int i)
        {
            if (Items[i].Quality < 50)
            {
                Items[i].Quality = Items[i].Quality + 1;
            }
        }

        private void DecreaseSellInOfItem(int i)
        {
            if (!IsLegendary(i))
            {
                Items[i].SellIn = Items[i].SellIn - 1;
            }
        }

        private void ReduceQualityOfItem(int i)
        {
            if (IsLegendary(i))
                return;

            Items[i].Quality = Items[i].Quality - 1;
            
            if (IsConjured(i))
                Items[i].Quality = Items[i].Quality - 1;

            if (Items[i].Quality < 0) Items[i].Quality = 0;
        }
    }
}
