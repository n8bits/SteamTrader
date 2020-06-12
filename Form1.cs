using OpenQA.Selenium.Chrome;
using SteamTrader.Extensions;
using SteamTrader.Pages;
using SteamTrader.Pages.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamTrader
{
    public partial class Form1 : Form
    {
        public SteamTradeSettings Settings;

        CancellationTokenSource cts = new CancellationTokenSource();

        public Form1()
        {
            
            InitializeComponent();
            Settings = new SteamTradeSettings()
            {
                Strategy = Trader.Strategy.Undercut,
                MinimumPrice = .04M,
                MaximumPrice = 60.0M,
                ConstantPriceSubtractor = 0.01M,
                SellPriceMultiplier = 0.93M,
                SpecialSellPriceMultiplier = 0.95m,
                DelayMilliseconds = 1000,
                IgnoredItems = new List<string>()
                {
                    "Lurker",
                    "Pulse",
                    "MP5-SD",
                    "Black Tie",
                    "Light Rail",
                    "Bloodsport",
                    "High Seas",
                    "Orion",
                    //"The Doctor",
                    "Stymphalian",
                    "Muertos",
                    "Chantico's",
                    "Pathfinder",
                    //"Foil",
                    //"The Doctor",
                    "Wildfire",
                    "Ultraviolet",
                    "Pyre",
                    //"Chainmail",
                    "Scorched",
                    "Red Stone",
                    //"Borre",
                    "Tornado",
                    //"Filigree",
                    "Dragon Lore",
                    //"Fire in the Hole",
                    "Elite Build",
                    "Community Sticker",
                    "Capsule",
                    "Bioleak",
                    "Heat"
                    //"CS20 Classic",
                    //"Water Gun",
                    // "Cheongsam",
                    //"Rage",
                    //"Longevity (Foil)"
                }
            };
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            GoButton.Enabled = false;
            CancelListingsButton.Enabled = false;
            StopButton.Enabled = true;
            cts = new CancellationTokenSource();
            Task.Run(new Action(() => Trader.ListItems(Settings, cts)));
        }


        private void button2_Click(object sender, EventArgs e)
        {
            GoButton.Enabled = false;
            CancelListingsButton.Enabled = false;
            StopButton.Enabled = true;

            cts = new CancellationTokenSource();
            Task.Run(new Action(() => Trader.CancelListings(Settings, cts)));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            this.cts.Cancel();
            GoButton.Enabled = true;
            this.CancelListingsButton.Enabled = true;
            StopButton.Enabled = false;
        }

        private void MakeGemsButton_Click(object sender, EventArgs e)
        {
            GoButton.Enabled = false;
            CancelListingsButton.Enabled = false;
            StopButton.Enabled = true;
            cts = new CancellationTokenSource();
            Task.Run(new Action(() => Trader.MakeGems(Settings, cts)));
        }

        private void CaseSimulatorButton_Click(object sender, EventArgs e)
        {
            Task.Run(new Action(() => CaseSimulator.OpenCases("Sticker 2")));
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Strategy = Trader.Strategy.QuickSell;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Strategy = Trader.Strategy.Undercut;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Strategy = Trader.Strategy.Mixed;
        }
    }
}
