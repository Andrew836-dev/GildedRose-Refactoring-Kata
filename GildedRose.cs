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
            foreach (var item in Items)
            {
                if (IsLegendary(item)) 
                    continue;

                DecreaseSellInOfItem(item);

                if (IsABackstagePass(item))
                {
                    if (item.SellIn < 0)
                    {
                        item.Quality = 0;
                        continue;
                    }

                    IncreaseQualityOfItem(item);

                    if (item.SellIn < 10)
                        IncreaseQualityOfItem(item);

                    if (item.SellIn < 5)
                        IncreaseQualityOfItem(item);

                    continue;
                }

                if (IsAgedBrie(item))
                {
                    IncreaseQualityOfItem(item);

                    if (item.SellIn >= 0)
                        continue;

                    IncreaseQualityOfItem(item);

                    continue;
                }
                
                ReduceQualityOfItem(item);

                if (item.SellIn >= 0)
                    continue;

                ReduceQualityOfItem(item);
            }
        }

        private bool IsConjured(Item item) => item.Name.StartsWith("Conjured");

        private bool IsABackstagePass(Item item) => item.Name.StartsWith("Backstage passes");

        private bool IsAgedBrie(Item item) => item.Name == "Aged Brie";

        private bool IsLegendary(Item item) => item.Name == "Sulfuras, Hand of Ragnaros";

        private void IncreaseQualityOfItem(Item item)
        {
            item.Quality += 1;

            if (item.Quality > 50)
                item.Quality = 50;
        }

        private void DecreaseSellInOfItem(Item item)
        {
            item.SellIn -= 1;
        }

        private void ReduceQualityOfItem(Item item)
        {
            item.Quality -= 1;
            
            if (IsConjured(item))
                item.Quality -= 1;

            if (item.Quality < 0) 
                item.Quality = 0;
        }
    }
}
