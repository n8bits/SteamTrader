using OpenQA.Selenium.Chrome;
using SteamTrader.Extensions;
using SteamTrader.Pages;
using SteamTrader.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTrader
{
    public class Trader
    {
        public enum Strategy
        {
            Undercut = 1,
            QuickSell = 2,
            Mixed = 3
        }

        public static async Task CancelListings(SteamTradeSettings settings, CancellationTokenSource cts)
        {
            String userProfile = @"E:\Code\VisualStudio\SteamTrader\ChromeProfile\";
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=" + userProfile);
            options.AddArgument("--profile-directory=SteamTrader");

            using (ChromeDriver driver = new ChromeDriver(options))
            {
                var marketPage = new MarketPage(driver);
                marketPage.GoToPage();

                var minPrice = 0.16M;

                while (!cts.IsCancellationRequested)
                {
                    try
                    {
                        while (!cts.IsCancellationRequested && marketPage.MarketTable.Listings.Any(x => x.BuyerPrice >= minPrice))
                        {
                            marketPage.MarketTable.Listings.First(x => x.BuyerPrice >= minPrice).RemoveListing();
                        }

                        if(marketPage.MarketTable.OnLastPage)
                        {
                            break;
                        }
                    }
                    catch
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    marketPage.MarketTable.NextPage();
                }
            }
        }


        public static async Task ListItems(SteamTradeSettings Settings, CancellationTokenSource cts)
        {
            String userProfile = @"E:\Code\VisualStudio\SteamTrader\ChromeProfile\";
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=" + userProfile);
            options.AddArgument("--profile-directory=SteamTrader");
            
           // options.AddArgument("--headless");
            Console.WriteLine(options.Arguments.ToString());

            var IDs = new List<String>();

            using (ChromeDriver driver = new ChromeDriver(options))
            {
                var inventoryPage = new InventoryPage(driver);

                inventoryPage.GoToPage();
                //inventoryPage.SkipPages(2);
                var page = 0;
                inventoryPage.SkipPages(page);
                while (true && !cts.IsCancellationRequested)
                {
                    Thread.Sleep(Settings.DelayMilliseconds);
                    var freshItems = inventoryPage.Inventory.Items.Where(x => !IDs.Contains(x.Id)).ToList();

                    if (freshItems.Count == 0)
                    {
                        page++;
                        driver.Navigate().Refresh();
                        inventoryPage.OpenCounterStrikeInventory();
                        inventoryPage.SkipPages(page);
                    }

                    foreach (var inventoryItem in freshItems)
                    {
                        if(cts.IsCancellationRequested)
                        {
                            return;
                        }

                        if (!inventoryPage.CounterStrikeInventoryOpen)
                        {
                            inventoryPage.GoToPage();
                            inventoryPage.OpenCounterStrikeInventory();

                        }
                        IDs.Add(inventoryItem.Id);

                        // Check if we should sell
                        if (
                                inventoryItem.ItemInfoPanel.Sellable
                                && inventoryItem.ItemInfoPanel.StartingPrice <= Settings.MaximumPrice
                                && inventoryItem.ItemInfoPanel.StartingPrice > Settings.MinimumPrice
                                && Settings.IgnoredItems.All(y => !inventoryItem.Name.Contains(y))
                                && Settings.IgnoredItems.All(y => !inventoryItem.Name.ToLower().Contains(y.ToLower()))
                           )
                        {
                            if (inventoryItem == null)
                                return;
                            inventoryItem.Select();
                            if (inventoryPage.ItemInfo.Sellable
                                && inventoryPage.ItemInfo.StartingPrice <= Settings.MaximumPrice
                                && inventoryPage.ItemInfo.StartingPrice > Settings.MinimumPrice)
                            {
                                var price = inventoryItem.ItemInfoPanel.StartingPrice;
                                var name = inventoryItem.Name;

                                
                                IDs.Add(inventoryItem.Id);
                                //price = (price * Settings.SellPriceMultiplier) - Settings.ConstantPriceSubtractor;

                                // Remove this temp pricing for unicorn
                                price = inventoryItem.Name.Contains("Sticker | Unicorn (Holo)") ? (price * Settings.SpecialSellPriceMultiplier) - Settings.ConstantPriceSubtractor : (price * Settings.SellPriceMultiplier) - Settings.ConstantPriceSubtractor;

                                // If we are looking to sell immediately, list at the highest price someone is currently willing to pay
                                if (Settings.Strategy == Trader.Strategy.QuickSell)
                                {
                                    var priceLookup = driver.LookupBuyPrice(inventoryItem.MarketUrl);

                                    if (priceLookup > 0)
                                    {
                                        price = priceLookup;
                                    }
                                }
                                else if (Settings.Strategy == Trader.Strategy.Undercut)
                                {
                                    var priceLookup = driver.LookupLowestSellPrice(inventoryItem.MarketUrl);

                                    if (priceLookup > 0)
                                    {
                                        // Undercut by a penny
                                        price = priceLookup - 0.01m;
                                    }
                                } else if (Settings.Strategy == Strategy.Mixed)
                                {
                                    if(price > 0.5m)
                                    {
                                        var priceLookup = driver.LookupLowestSellPrice(inventoryItem.MarketUrl);

                                        if (priceLookup > 0)
                                        {
                                            // Undercut by a penny
                                            price = priceLookup - 0.01m;
                                        }
                                    }
                                    else
                                    {
                                        var priceLookup = driver.LookupBuyPrice(inventoryItem.MarketUrl);

                                        if (priceLookup > 0)
                                        {
                                            price = priceLookup;
                                        }
                                    }
                                }

                                var sellDialog = inventoryPage.ItemInfo.Sell();
                                sellDialog.Sell(price);
                                Thread.Sleep(Settings.DelayMilliseconds);

                                if (IDs.Count % 8 == 0)
                                {
                                    driver.Navigate().Refresh();
                                    inventoryPage.SkipPages(page);
                                }


                                break;
                            }
                        }
                        else
                        {
                            Thread.Sleep(Settings.DelayMilliseconds);
                        }

                    }
                }
            }
        }


        public static async Task MakeGems(SteamTradeSettings Settings, CancellationTokenSource cts)
        {
            String userProfile = @"E:\Code\VisualStudio\SteamTrader\ChromeProfile\";
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=" + userProfile);
            options.AddArgument("--profile-directory=SteamTrader");
            Console.WriteLine(options.Arguments.ToString());

            var IDs = new List<String>();

            using (ChromeDriver driver = new ChromeDriver(options))
            {
                var inventoryPage = new InventoryPage(driver);

                inventoryPage.GoToPage();
                inventoryPage.OpenInventory(InventoryPage.STEAM_INVENTORY_ID);
                //inventoryPage.SkipPages(2);
                var page = 0;
                inventoryPage.SkipPages(page);
                while (true && !cts.IsCancellationRequested)
                {
                    Thread.Sleep(Settings.DelayMilliseconds);
                    var freshItems = inventoryPage.Inventory.Items.Where(x => !IDs.Contains(x.Id)).ToList();

                    if (freshItems.Count == 0)
                    {
                        page++;
                        driver.Navigate().Refresh();
                        inventoryPage.OpenInventory(InventoryPage.STEAM_INVENTORY_ID);
                        inventoryPage.SkipPages(page);
                    }

                    foreach (var inventoryItem in freshItems)
                    {
                        if (cts.IsCancellationRequested)
                        {
                            return;
                        }

                        IDs.Add(inventoryItem.Id);

                        // Check if we should sell
                        if(true)
                        {
                            inventoryItem.Select();
                            inventoryItem.ItemInfoPanel.TurnIntoGems();
                                Thread.Sleep(Settings.DelayMilliseconds);

                                if (IDs.Count % 8 == 0)
                                {
                                    driver.Navigate().Refresh();
                                    inventoryPage.SkipPages(page);
                                }


                                break;
                            
                        }
                        else
                        {
                            Thread.Sleep(Settings.DelayMilliseconds);
                        }

                    }
                }
            }
        }
    }
}
